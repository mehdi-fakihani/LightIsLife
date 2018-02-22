using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace LIL
{
    public class EnemyMovement : MonoBehaviour
    {
        public float minSpeed = 3.5f;
        public float maxSpeed = 7f;

        private Transform player;               // Reference to the player's position.
        private Transform player2;
        private NavMeshAgent nav;               // Reference to the nav mesh agent.
        private Animator anim;
        private LightFuel torchLight;
        private bool moveCancelled = false;
        private bool multi;
        private float distance;


        void Start()
        {
            // Set up the references.
            multi = SceneManager.getMulti();
            player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            if (multi == true)
            {
                player2 = GameObject.FindGameObjectsWithTag("Player")[1].transform;
            }
            torchLight = player.GetComponent<LightFuel>();
            anim = GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();
        }


        void Update()
        {
            if (GetComponent<HealthEnemy>().getCurrentHealth() > 0)
            {
                float distance1 = Vector3.Distance(player.position, transform.position);
                if (multi == true)
                {
                    float distance2 = Vector3.Distance(player2.position, transform.position);
                    distance = Mathf.Min(distance1, distance2);
                }
                else
                {
                    distance = distance1;
                }
                nav.speed = minSpeed + (maxSpeed - minSpeed)
                                     * Mathf.Clamp(distance / torchLight.GetLightRange(), 0, 1);

                // Added by Sidney
                nav.speed *= GetComponent<MovementManager>().getSpeedRatio();
                if (GetComponent<MovementManager>().isImmobilized()) nav.speed = 0;

                if (anim.GetBool("walk"))
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
}
