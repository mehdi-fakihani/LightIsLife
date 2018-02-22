using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace LIL
{
    [RequireComponent(typeof(SkillManager))]
    public class MageMovement : MonoBehaviour
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
        private Animator anim;
        private LightFuel torchLight;
        private bool moveCancelled = false;
        private int currentDamage;

        void Start()
        {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag("Player").transform;
            torchLight = player.GetComponent<LightFuel>();
            anim = GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();
            moveCancelled = false;

            fireball = GetComponent<SkillManager>().getSkill(SkillsID.Fireball);
            currentDamage = (fireball.model as FireballSkill).damageAttack;
        }


        void Update()
        {
            //check if not dead
            if (GetComponent<HealthEnemy>().getCurrentHealth() > 0)
            {
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
                    anim.SetBool("walk", false);
                    if (!moveCancelled)
                    {
                        nav.SetDestination(transform.position);
                        moveCancelled = true;
                    }

                    //turn toward player
                    transform.rotation = Quaternion.LookRotation(movement);

                    //start shooting
                    fireball.tryCast();
                }
                else
                {
                    anim.SetBool("walk", true);
                    moveCancelled = false;
                    nav.SetDestination(player.position);
                }
                
            }
        }

        public void increaseDamage(float ratio)
        {
            FireballSkill fs = (fireball.model as FireballSkill);
            fs.damageAttack += (int) Mathf.Round(fs.damageAttack * ratio);
        }
    }
}
