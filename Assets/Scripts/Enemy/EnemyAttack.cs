using UnityEngine;
using System.Collections;
using System;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    Health playerHealth;
    float timer;                                // Timer for counting up to the next attack.


    void Start()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange)
        {
            // ... attack.
            Attack();
            anim.SetBool("walk", false);
        }

        if (!playerInRange)
        {
            
            anim.SetBool("walk", true);
        }

    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;
        playerHealth.takeDammage(attackDamage);
        Console.WriteLine("Health of the player after the attack : "+ playerHealth.getCurrentHealth());
        anim.SetTrigger("attack");
    }
}