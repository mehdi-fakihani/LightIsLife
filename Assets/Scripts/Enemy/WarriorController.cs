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
        private NavMeshAgent nav;               // Reference to the nav mesh agent.
        private Animator animator;
        private LightFuel torchLight;
        private bool moveCancelled = false;


        void Start()
        {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag("Player").transform;
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

            // set speed depending on player's light 
            float distance = Vector3.Distance(player.position, transform.position);

            nav.speed = minSpeed + (maxSpeed - minSpeed)
                                    * Mathf.Clamp(distance / torchLight.GetLightRange(), 0, 1);
                
            nav.speed *= GetComponent<MovementManager>().getSpeedRatio();
            if (GetComponent<MovementManager>().isImmobilized()) nav.speed = 0f;

            // update target position and walk animation
            if (animator.GetBool("walk"))
            {
                nav.SetDestination(player.position);
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
