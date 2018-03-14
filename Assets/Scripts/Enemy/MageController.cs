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
        public float retreatRadius = 7.5f;
        public float castingTime = 0.5f;
        public float range = 15f;
        public float rangeFactor = 0.75f;
        public float spellRadius = 0.5f;
        public float shiftDistance = 7f;

        // don't throw spell at max range but maxRange * rangeFactor
        // so the spell will be harder to dodge

        Skill fireball;

        private Transform player;               // Reference to the player's position.
        private NavMeshAgent nav;               // Reference to the nav mesh agent.
        private Rigidbody body;
        private Animator animator;
        private LightFuel torchLight;
        private FireballSkill skillModel;
        private float currentDamage;

        private delegate void StateAction();
        private StateAction currentAction;
        private int shift;                  // choose to shift left or right to shoot at player
        private float currentShiftDistance;
        private bool isObstacle;
        private bool isInRange;                 // try to fire
        private bool isRetreating;              // setup retreat

        void Start()
        {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag("Player").transform;
            torchLight = player.GetComponent<LightFuel>();
            animator = GetComponent<Animator>();
            body = GetComponent<Rigidbody>();
            nav = GetComponent<NavMeshAgent>();

            fireball = GetComponent<SkillManager>().getSkill(SkillsID.Fireball);
            skillModel = fireball.model as FireballSkill;
            currentDamage = skillModel.damageAttack;
            skillModel.castTime = castingTime;

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

            currentAction = this.TryToFire;
            isObstacle = false;
            isInRange = false;
            isRetreating = false;
            body.isKinematic = true; // start as immobile

            shift = UnityEngine.Random.Range(0, 2);
            if (shift == 0) shift = -1;
            currentShiftDistance = 0;

        }

        void Update()
        {
            //check if not dead
            if (!GetComponent<HealthManager>().isAlive()) return;
            // set current agent speed
            SetSpeed();
            // do something
            currentAction();
            Debug.Log(currentAction.Method.Name);
        }

        private void GetCloser()
        {
            MoveToward(player.position);

            // check for transitions
            float distance = Vector3.Distance(player.position, transform.position);
            isInRange = distance < range * rangeFactor;
            if (isInRange)
            {
                currentAction = this.TryToFire;
            }
        }

        private void TurnOverPlayer()
        {
            // move  with 90 angle and try to shoot again
            Vector3 direction = player.position - transform.position;
            MoveToward(transform.position + Quaternion.Euler(0, shift * 100, 0) * direction);
            currentShiftDistance += nav.speed * Time.deltaTime;
            if(currentShiftDistance > shiftDistance)
            {
                currentAction = this.TryToFire;
                currentShiftDistance = 0;
            }
        }

        private void TryToFire()
        {
            float distance = Vector3.Distance(player.position, transform.position);
            isRetreating = distance < retreatRadius;

            // don't check range and obstacle if it has to retreat
            if (isRetreating)
            {
                currentAction = this.Retreat;
                return;
            }
            
            //check if in range
            UpdateRangeAndObstacle(transform.position);

            if (isInRange)
            {
                if (isObstacle)
                {
                    currentAction = this.TurnOverPlayer;
                }
                else
                {
                    currentAction = this.Fire;
                }
            }
            else
            {
                currentAction = this.GetCloser;
            }

        }

        private void UpdateRangeAndObstacle(Vector3 position)
        {
            float distance = Vector3.Distance(player.position, position);
            isInRange = distance < range * rangeFactor;

            if (isInRange)
            {
                isObstacle = false;
                Vector3 movement = player.position - position;
                RaycastHit hit;
                if (Physics.SphereCast(position, spellRadius, movement, out hit, range))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (!hitObject.CompareTag("Player"))
                    {
                        isObstacle = true;
                    }
                }
            }
        }

        private void Wait() { }

        private void Fire()
        {
            StopMovement();
            FacePosition(player.position);

            //start shooting
            if (!fireball.tryCast())
            {
                // impossible to shoot
                choosePostFireAction();
                return;
            }

            // Added by Sidney : Get the new fireball and buff it
            var hitColliders = Physics.OverlapSphere(transform.position, 0f);
            //bool found = false;
            foreach (var hitCollider in hitColliders)
            {
                var fbScript = hitCollider.GetComponent<Fireball>();
                if (!fbScript) return;
                if (fbScript.caster != gameObject) return;
                fbScript.damages = currentDamage;
                //found = true;
            }
            //Assert.IsTrue(found);

            // wait casting time and make transition to TryToFire
            StartCoroutine(WaitCastingTime());
        }

        IEnumerator WaitCastingTime()
        {
            currentAction = this.Wait;
            yield return new WaitForSeconds(castingTime);
            choosePostFireAction();
        }

        private void choosePostFireAction()
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance < retreatRadius)
            {
                currentAction = this.Retreat;
            }
            else
            {
                currentAction = this.TryToFire;
            }
        }

        private void Retreat()
        {
            FleeFrom(player.position);

            // check for transitions
            float distance = Vector3.Distance(player.position, transform.position);
            isRetreating = distance < range * rangeFactor;
            if (!isRetreating)
            {
                currentAction = this.TryToFire;
            }
        }

        public void increaseDamage(float ratio)
        {
            currentDamage += skillModel.damageAttack * ratio;
        }

        private void FleeFrom(Vector3 position)
        {
            MoveToward(2 * transform.position - position);
        }

        private void MoveToward(Vector3 position)
        {
            
            if (!animator.GetBool("walk"))
            {
                animator.SetBool("walk", true);
            }

            FacePosition(position);
            Vector3 direction = position - transform.position;
            nav.Move(direction.normalized * Time.deltaTime * nav.speed);
        }

        private void StopMovement()
        {
            // check if movement is not already stopped
            if (animator.GetBool("walk"))
            {
                animator.SetBool("walk", false);
            }
        }

        private void FacePosition(Vector3 position)
        {
            //transform.rotation = Quaternion.LookRotation(position);
            transform.LookAt(position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, Vector3.Angle(transform.position, position), 0), Time.deltaTime);
        }

        private void SetSpeed()
        {
            float distance = Vector3.Distance(player.position, transform.position);

            nav.speed = minSpeed + (maxSpeed - minSpeed)
                                 * Mathf.Clamp(distance / torchLight.GetLightRange(), 0, 1);

            nav.speed *= GetComponent<MovementManager>().getSpeedRatio();
            if (GetComponent<MovementManager>().isImmobilized()) nav.speed = 0f;
        }

    }
}
