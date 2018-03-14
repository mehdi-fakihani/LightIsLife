using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace LIL
{
    public class WarriorController : MonoBehaviour
    {
        public float minSpeed = 3.5f;
        public float maxSpeed = 7f;

        private Transform player;               // Reference to the player's position.
        private Transform player2;
        private NavMeshAgent nav;               // Reference to the nav mesh agent.
        private Animator animator;
        private LightFuel torchLight;
        private bool moveCancelled = false;


        void Start()
        {
            // Set up the references.
            player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            player2 = GameObject.FindGameObjectsWithTag("Player")[1].transform;
            torchLight = player.GetComponent<LightFuel>();
            animator = GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();

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

            float distance1 = Vector3.Distance(player.position, transform.position);
            float distance2 = Vector3.Distance(player2.position, transform.position);

            float distance = Mathf.Min(distance1, distance2);

            nav.speed = minSpeed + (maxSpeed - minSpeed)
                                    * Mathf.Clamp(distance / torchLight.GetLightRange(), 0, 1);
                
            nav.speed *= GetComponent<MovementManager>().getSpeedRatio();
            if (GetComponent<MovementManager>().isImmobilized()) nav.speed = 0f;

            if (animator.GetBool("walk"))
            {
                if (distance == distance1)
                {
                    nav.SetDestination(player.position);
                }
                else
                {
                    nav.SetDestination(player2.position);
                }
                moveCancelled = false;
            }
            else
            {
                if (!moveCancelled)
                {
                    nav.SetDestination(transform.position);
                    moveCancelled = true;
                }
            }
            
        }
    }
}
