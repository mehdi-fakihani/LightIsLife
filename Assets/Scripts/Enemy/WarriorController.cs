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
        private float distance2;
        private HealthManager playerHealth;
        private HealthManager playerHealth2;
        private float timer;                                // Timer for counting up to the next attack.
        private bool playerInRange;
        private bool player2InRange;
        private float currentAttackDamage;

        public static GameObject Create(EnemyManager em, GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject w = Instantiate(prefab, position, rotation);
            w.GetComponent<WarriorController>().SetManager(em);
            return w;
        }

        public void SetManager(EnemyManager manager)
        {
            em = manager;
        }

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
            if (GeneralData.multiplayer)
            {
                multiplayer = true;
                player2 = GameObject.FindGameObjectsWithTag("Player")[1].transform;
                playerHealth2 = player2.GetComponent<HealthManager>();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            // If the entering collider is the player...
            if (other.gameObject.Equals(player.gameObject))
            {
                // ... the player is in range.
                playerInRange = true;
            }
            else if (multiplayer && other.gameObject.Equals(player2.gameObject))
            {
                player2InRange = true;
            }
        }


        void OnTriggerExit(Collider other)
        {
            // If the exiting collider is the player...
            if (other.gameObject.Equals(player.gameObject))
            {
                // ... the player is no longer in range.
                playerInRange = false;
            }
            if (multiplayer  && other.gameObject.Equals(player2.gameObject))
            {
                player2InRange = false;
            }
        }


        void Attack(HealthManager health)
        {
            // Reset the timer
            timer = 0f;
            health.harm(currentAttackDamage);
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
                distance2 = Vector3.Distance(player2.position, transform.position);

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
                nav.SetDestination(player.position);
                moveCancelled = false;
                transform.LookAt(player.position);
                nav.isStopped = true;
                nav.velocity = Vector3.zero;
                animator.SetBool("walk", false);
                if(timer > timeBetweenAttacks)
                {
                    Attack(playerHealth);
                }
                
            }
            else if (multiplayer && player2InRange)
            {
                nav.SetDestination(player2.position);
                moveCancelled = false;
                //Debug.Log("in range");
                transform.LookAt(player2.position);
                nav.isStopped = true;
                nav.velocity = Vector3.zero;
                animator.SetBool("walk", false);
                if (timer > timeBetweenAttacks)
                {
                    Attack(playerHealth2);
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
                if (multiplayer)
                {
                    if (distance1 <= distance2)
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
               
            }
        }
    }
}
