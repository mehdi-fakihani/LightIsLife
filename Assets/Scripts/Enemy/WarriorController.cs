using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace LIL
{
    public class WarriorController : MonoBehaviour
    {
        public float minSpeed = 3.5f;
        public float maxSpeed = 7f;
        public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
        public int baseAttackDamage = 10;               // The amount of health taken away per attack.
        public AudioClip hurtSound;
        public AudioClip deathSound;

        private EnemyManager em;
        private AudioSource source;
        private Transform player;               // Reference to the player's position.
        private Transform player2;
        private NavMeshAgent nav;               // Reference to the nav mesh agent.
        private Animator animator;
        private LightFuel torchLight;
        private bool moveCancelled = false;
        private bool multiplayer = false;
        private float distance;
        private float distance1;
        private HealthManager playerHealth;
        private float timer;                                // Timer for counting up to the next attack.
        private bool playerInRange;
        private float currentAttackDamage;


        void Start()
        {
            // Set up the references.
            source = GetComponent<AudioSource>();
            player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            torchLight = GameObject.FindGameObjectWithTag("Spirit").GetComponent<LightFuel>();
            animator = GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();

            playerHealth = player.GetComponent<HealthManager>();
            currentAttackDamage = baseAttackDamage;
            playerInRange = false;
            timer = timeBetweenAttacks;

            GameObject emObject = GameObject.FindGameObjectWithTag("EnemyManager");
            if (emObject != null)
            {
                em = emObject.GetComponent<EnemyManager>();
            }
            
            // Set hurt and death reactions
            var health = GetComponent<HealthManager>();
            health.setHurtCallback(() =>
            {
                animator.SetTrigger("hurt");
                if(hurtSound != null)
                {
                    source.PlayOneShot(hurtSound);
                }
            });
            health.setDeathCallback(() =>
            {
                animator.SetTrigger("death");
                if (deathSound != null)
                {
                    source.PlayOneShot(deathSound);
                }
                if(em != null)
                {
                    em.CountDeath();
                }
                Destroy(gameObject, 1.5f);
            });
            if (SceneManager.getMulti())
            {
                multiplayer = true;
                player2 = GameObject.FindGameObjectsWithTag("Player")[1].transform;

            }
        }

        void OnTriggerEnter(Collider other)
        {
            // If the entering collider is the player...
            if (other.gameObject.CompareTag("Player"))
            {
                // ... the player is in range.
                playerInRange = true;
            }
        }


        void OnTriggerExit(Collider other)
        {
            // If the exiting collider is the player...
            if (other.gameObject.CompareTag("Player"))
            {
                // ... the player is no longer in range.
                playerInRange = false;
            }
        }


        void Attack()
        {
            // Reset the timer
            timer = 0f;
            playerHealth.harm(currentAttackDamage);
            animator.SetTrigger("attack");
        }

        public void increaseDamage(float ratio)
        {
            currentAttackDamage += baseAttackDamage * ratio;
        }

        void Update()
        {
            timer += Time.deltaTime;

            //check if not dead
            if (!GetComponent<HealthManager>().isAlive()) return;

            if (multiplayer)
            {
                distance1 = Vector3.Distance(player.position, transform.position);
                float distance2 = Vector3.Distance(player2.position, transform.position);

                distance = Mathf.Min(distance1, distance2);
            }

            else
            {
                distance = Vector3.Distance(player.position, transform.position);
            }
            // set speed depending on player's light 

            nav.speed = minSpeed + (maxSpeed - minSpeed)
                                    * Mathf.Clamp(distance / torchLight.GetLightRange(), 0, 1);
                
            nav.speed *= GetComponent<MovementManager>().getSpeedRatio();
            if (GetComponent<MovementManager>().isImmobilized()) nav.speed = 0f;

            // update target position and walk animation
            if (playerInRange)
            {
                if (multiplayer)
                {
                    if (distance == distance1)
                    {
                        nav.SetDestination(player.position);
                    }
                    else
                    {
                        nav.SetDestination(player2.position);
                    }
                }
                else
                {
                    nav.SetDestination(player.position);
                }
                moveCancelled = false;
                //Debug.Log("in range");
                transform.LookAt(player.position);
                nav.isStopped = true;
                nav.velocity = Vector3.zero;
                animator.SetBool("walk", false);
                if(timer > timeBetweenAttacks)
                {
                    Attack();
                }
                
            }
            else
            {
                if (nav.isStopped)
                {
                    transform.LookAt(player.position);
                    nav.isStopped = false;
                }
                animator.SetBool("walk", true);
                nav.SetDestination(player.position);
            }
        }
    }
}
