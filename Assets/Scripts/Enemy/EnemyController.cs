using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace LIL
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float minSpeed = 3.5f;
        [SerializeField] private float maxSpeed = 7f;
        [SerializeField] private AudioClip hurtSound;
        [SerializeField] private AudioClip deathSound;

        private Transform player;               // Reference to the player's position.
        private NavMeshAgent nav;               // Reference to the nav mesh agent.
        private Animator animator;
        private LightFuel torchLight;
        private AudioSource audioSource;
        private bool moveCancelled = false;

        void Start()
        {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag("Player").transform;
            torchLight = player.GetComponent<LightFuel>();
            animator = GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();
            audioSource = GetComponent<AudioSource>();

            // Set hurt and death reactions
            var health = GetComponent<HealthManager>();
            health.setHurtCallback(() =>
            {
                animator.SetTrigger("hurt");
                if (hurtSound && !audioSource.isPlaying) audioSource.PlayOneShot(hurtSound);
            });
            health.setDeathCallback(() =>
            {
                animator.SetTrigger("death");
                if (deathSound) audioSource.PlayOneShot(deathSound);
                Destroy(gameObject, 1.5f);
            });
        }


        void Update()
        {
            if (!GetComponent<HealthManager>().isAlive()) return;

            float distance = Vector3.Distance(player.position, transform.position);

            nav.speed = minSpeed + (maxSpeed - minSpeed)
                                    * Mathf.Clamp(distance / torchLight.GetLightRange(), 0, 1);

            // Added by Sidney
            nav.speed *= GetComponent<MovementManager>().getSpeedRatio();
            if (GetComponent<MovementManager>().isImmobilized()) nav.speed = 0;

            if (animator.GetBool("walk"))
            {
                nav.SetDestination(player.position);
                moveCancelled = false;
            }
            else if (!moveCancelled)
            {
                nav.SetDestination(transform.position);
                moveCancelled = true;
            }
        }
    }
}
