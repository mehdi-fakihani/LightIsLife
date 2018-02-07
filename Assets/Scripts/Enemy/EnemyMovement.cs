using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float minSpeed = 3.5f;
    public float maxSpeed = 7f;

    private Transform player;               // Reference to the player's position.
    private NavMeshAgent nav;               // Reference to the nav mesh agent.
    private Animator anim;
    private LightFuel torchLight;
    private bool moveCancelled = false;


    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        torchLight = player.GetComponent<LightFuel>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        nav.speed = minSpeed + (maxSpeed - minSpeed)
                             * Mathf.Clamp(distance / torchLight.GetLightRange(), 0, 1);
        if (anim.GetBool("walk"))
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