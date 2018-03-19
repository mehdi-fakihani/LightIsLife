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
        public float fleeingRadius = 15f;      // distance from player to reach if he gets too close
        public float fleeingDistance = 3f;     // distance fled when player is too clos
        public float statIncreaseRatio = 1.5f;   // multiply enemy stat by ratio
        public float sizeIncreaseRatio = 1.5f;   // % of enemy size to add (+100%)
        public float barycenterTolerance = 3f;
        
        [SerializeField] private float scalingDuration = 0; //seconds to increase size

        private EnemyManager em;
        private Transform player;               // Reference to the player's position.
        private NavMeshAgent nav;               // Reference to the nav mesh agent.
        private Animator animator;
        private LightFuel torchLight;
        private bool moveCancelled = false;

        private delegate void StateAction();
        private StateAction currentAction;
        private float currentFledDistance;

        private List<GameObject> neighbours = new List<GameObject>();
        private List<GameObject> targets = new List<GameObject>();

        private enum EnemyType
        {
            Warrior = 0,
            Mage = 1,
            Support = 2,
        }

        void Start()
        {
            // Set up the references.
            em = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            torchLight = GameObject.FindGameObjectWithTag("Spirit").GetComponent<LightFuel>();
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
                em.CountDeath();
                Destroy(gameObject, 1.5f);
            });

            currentAction = this.FindTargets;
            currentFledDistance = 0;
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
                    targets.Remove(other.gameObject);

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
            WarriorController wc = enemy.GetComponent<WarriorController>();
            // if enemy is a warrior type
            if (wc != null)
            {
                wc.increaseDamage(statIncreaseRatio);
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
            // set current agent speed
            SetSpeed();
            // do something
            currentAction();
        }

        private List<GameObject> ListAlliesToSupport()
        {
            List<GameObject> list = new List<GameObject>();
            MageController[] mageAllies = GameObject.FindObjectsOfType(typeof(MageController)) as MageController[];
            if(mageAllies.Length == 0)
            {
                WarriorController[] warriorAllies = GameObject.FindObjectsOfType(typeof(WarriorController)) as WarriorController[];
                if(warriorAllies.Length == 0)
                {
                    return list;
                }
                foreach(var a in warriorAllies)
                {
                    list.Add(a.gameObject);
                }
            }
            else
            {
                foreach (var a in mageAllies)
                {
                    list.Add(a.gameObject);
                }
            }
            return list;
        }

        private void FindTargets()
        {
            List<GameObject> allies = ListAlliesToSupport();
            if (allies.Count == 0)
            {
                currentAction = this.Flee;
                return;
            }

            targets.Clear();
            GameObject closestAlly = allies[0].gameObject;
            float distance = Mathf.Infinity;
            float currentDistance;
            foreach(var a in allies)
            {
                currentDistance = Vector3.Distance(transform.position, a.transform.position);
                if(currentDistance != 0 && currentDistance < distance)
                    //do not take current support enemy into account
                {
                    closestAlly = a.gameObject;
                    distance = currentDistance;
                }
            }
            
            foreach (var a in allies)
            {
                if(Vector3.Distance(closestAlly.transform.position, a.transform.position) < range / 2)
                {
                    targets.Add(a.gameObject);
                }
            }

            currentAction = this.SupportAllies;
        }

        private void Flee()
        {
            FleeFrom(player.position);
            currentFledDistance += nav.speed * Time.deltaTime;
            if(currentFledDistance > fleeingDistance)
            {
                currentAction = this.FindTargets;
                currentFledDistance = 0;
            }
        }

        private void Retreat()
        {
            FleeFrom(player.position);
            float distance = Vector3.Distance(player.position, transform.position);
            if(distance > fleeingRadius)
            {
                currentAction = this.FindTargets;
            }
        }

        private void SupportAllies()
        {
            if(Vector3.Distance(player.position, transform.position) < range)
            {
                // if player gets too close, retreat
                currentAction = this.Retreat;
                return;
            }

            if (neighbours.Count == 0 || targets.Count == 0)
            {
                currentAction = this.FindTargets;
            }

            Vector3 barycenter = Vector3.zero;
            foreach(var t in targets)
            {
                barycenter += t.transform.position;
            }
            barycenter /= targets.Count;
            // move the barycenter from group to the side opposite to the player
            Vector3 shiftBarycenter = barycenter - player.position;
            barycenter = barycenter + shiftBarycenter.normalized * (range / 2);

            if (nav.speed > 0 && Vector3.Distance(transform.position, barycenter) > barycenterTolerance / nav.speed)
            {
                // if support needs to move toward barycenter to get into tolerance radius, move
                MoveToward(barycenter);
            }
            else
            {
                FacePosition(player.position);
                StopMovement();
            }
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
            transform.LookAt(position);
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
