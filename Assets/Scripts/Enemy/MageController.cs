using System;
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.AI;

namespace LIL
{
    [RequireComponent(typeof(SkillManager))]
    public class MageController : MonoBehaviour
    {
        public float minSpeed = 3.5f;
        public float maxSpeed = 7f;
        public float range = 15f;
        public float rangeFactor = 0.75f;
        public float spellRadius = 0.5f;
        // don't throw spell at max range but maxRange * rangeFactor
        // so the spell will be harder to dodge

        Skill fireball;

        private Transform player;               // Reference to the player's position.
        private NavMeshAgent nav;               // Reference to the nav mesh agent.
        private Animator animator;
        private LightFuel torchLight;
        private FireballSkill skillModel;
        private bool moveCancelled = false;
        private float currentDamage;

        void Start()
        {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag("Player").transform;
            torchLight = player.GetComponent<LightFuel>();
            animator = GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();
            moveCancelled = false;

            fireball = GetComponent<SkillManager>().getSkill(SkillsID.Fireball);
            skillModel = fireball.model as FireballSkill;
            currentDamage = skillModel.damageAttack;

            // Set hurt and death reactions
            var health = GetComponent<HealthManager>();
            health.setHurtCallback(() =>
            {
                animator.SetTrigger("hurt");
            });
            health.setDeathCallback(() =>
            {
                animator.SetTrigger("death");
                Destroy(gameObject, 1.5f);
            });
        }

        void Update()
        {
            //check if not dead
            if (!GetComponent<HealthManager>().isAlive()) return;
            
            float distance = Vector3.Distance(player.position, transform.position);

            nav.speed = minSpeed + (maxSpeed - minSpeed)
                                    * Mathf.Clamp(distance / torchLight.GetLightRange(), 0, 1);

            nav.speed *= GetComponent<MovementManager>().getSpeedRatio();

            //check if in fire range
            bool inRange = false;
            Vector3 movement = player.position - transform.position;
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, spellRadius, movement, out hit, range))
            {
                GameObject hitObject = hit.transform.gameObject;
                inRange = hitObject.CompareTag("Player") && distance < range * rangeFactor;
            }

            //if in range
            if (inRange)
            {
                //stop walking
                animator.SetBool("walk", false);
                if (!moveCancelled)
                {
                    nav.SetDestination(transform.position);
                    moveCancelled = true;
                }

                //turn toward player
                transform.rotation = Quaternion.LookRotation(movement);

                //start shooting
                if (!fireball.tryCast()) return;

                // Added by Sidney : Get the new fireball and buff it
                var hitColliders = Physics.OverlapSphere(transform.position, 0f);
                bool found = false;
                foreach (var hitCollider in hitColliders)
                {
                    var fbScript = hitCollider.GetComponent<Fireball>();
                    if (!fbScript) return;
                    if (fbScript.caster != gameObject) return;
                    fbScript.damages = currentDamage;
                    found = true;
                }
                //Assert.IsTrue(found);
            }
            else
            {
                animator.SetBool("walk", true);
                moveCancelled = false;
                nav.SetDestination(player.position);
            }
                
        }

        public void increaseDamage(float ratio)
        {
            currentDamage += skillModel.damageAttack * ratio;
        }
    }
}
