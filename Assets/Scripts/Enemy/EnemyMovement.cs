using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;               // Reference to the player's position.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    Animator anim;
    bool moveCancelled = false;


    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
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