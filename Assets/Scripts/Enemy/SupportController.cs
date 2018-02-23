using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

namespace LIL
{
    [RequireComponent (typeof (SphereCollider))]
    public class SupportController : MonoBehaviour
    {
        public float minSpeed = 3.5f;
        public float maxSpeed = 7f;
        public float range = 7f;
        public float statIncreaseRatio = 1.5f;   // multiply enemy stat by ratio
        public float sizeIncreaseRatio = 1.5f;   // % of enemy size to add (+100%)
        
        [SerializeField] private float scalingDuration = 0; //seconds to increase size

        private Transform player;               // Reference to the player's position.
        private NavMeshAgent nav;               // Reference to the nav mesh agent.
        private Animator animator;
        private LightFuel torchLight;
        private bool moveCancelled = false;
     
        private List<GameObject> neighbours = new List<GameObject>();

        private enum EnemyType
        {
            Warrior = 0,
            Mage = 1,
            Support = 2,
        }

        void Start()
        {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag("Player").transform;
            torchLight = player.GetComponent<LightFuel>();
            animator = GetComponent<Animator>();
            animator.SetBool("walk", true);
            nav = GetComponent<NavMeshAgent>();
            moveCancelled = false;
            GetComponent<SphereCollider>().radius = range;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                // if a new neighbour gets close
                if (!neighbours.Contains(other.gameObject))
                {
                    neighbours.Add(other.gameObject);
                    // add extra attack damage and make the enemy look bigger if not a support
                    if (ChangeStat(other.gameObject, statIncreaseRatio) != EnemyType.Support)
                    {
                        ScaleObject(other.gameObject, sizeIncreaseRatio);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                // if a neighbour gets out
                if (neighbours.Contains(other.gameObject) 
                    && Vector3.Distance(transform.position, other.gameObject.transform.position) > range)
                {
                    neighbours.Remove(other.gameObject);

                    // retrieve normal attack and normal size if not a support
                    if(ChangeStat(other.gameObject, -statIncreaseRatio) != EnemyType.Support)
                    {
                        ScaleObject(other.gameObject, 1 / sizeIncreaseRatio);
                    }
                }
            }
        }

        private EnemyType ChangeStat(GameObject enemy, float ratio)
        {
            WarriorAttack wa = enemy.GetComponent<WarriorAttack>();
            // if enemy is a warrior type
            if (wa != null)
            {
                wa.increaseDamage(statIncreaseRatio);
                return EnemyType.Warrior;
            }

            MageController mm = enemy.GetComponent<MageController>();
            // if enemy is a mage type
            if (mm != null)
            {
                mm.increaseDamage(statIncreaseRatio);
                return EnemyType.Mage;
            }

            return EnemyType.Support;
        }

        private void ScaleObject(GameObject obj, float ratio)
        {
            obj.transform.localScale *= ratio;
        }

        private void OnDestroy()
        {
            // debuff all buffed neighbours
            foreach(var n in neighbours)
            {
                //Check if neighbour has been destroyed
                if(n != null)
                {
                    // retrieve normal attack and normal size if not a support
                    if (ChangeStat(n, -statIncreaseRatio) != EnemyType.Support)
                    {
                        ScaleObject(n, 1 / sizeIncreaseRatio);
                    }
                }
            }
        }

        void Update()
        {
            //check if not dead
            if (!GetComponent<HealthManager>().isAlive()) return;

            float distance = Vector3.Distance(player.position, transform.position);

            nav.speed = minSpeed + (maxSpeed - minSpeed)
                                    * Mathf.Clamp(distance / torchLight.GetLightRange(), 0, 1);

            nav.speed *= GetComponent<MovementManager>().getSpeedRatio();
            if (GetComponent<MovementManager>().isImmobilized()) nav.speed = 0f;

            Vector3 movement = player.position - transform.position;
            //if in range
            if (Vector3.Magnitude(movement) < range)
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
            }
            else
            {
                animator.SetBool("walk", true);
                moveCancelled = false;
                nav.SetDestination(player.position);
            }
        }
    }
}
