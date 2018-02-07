using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;               
    NavMeshAgent nav;              
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
        if (anim.GetBool("Move"))
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