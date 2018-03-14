using UnityEngine;
using System.Collections;
using System;

namespace LIL
{
    public class WarriorAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
        public int baseAttackDamage = 10;               // The amount of health taken away per attack.


        Animator anim;                              // Reference to the animator component.
        GameObject player;                          // Reference to the player GameObject.
        bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
        HealthManager playerHealth;
        float timer;                                // Timer for counting up to the next attack.

        private float currentAttackDamage;

        void Start()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<HealthManager>();
            anim = GetComponent<Animator>();
            currentAttackDamage = baseAttackDamage;
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
            if (timer >= timeBetweenAttacks && playerInRange && GetComponent<HealthManager>().isAlive())
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
            playerHealth.harm(currentAttackDamage);
            anim.SetTrigger("attack");
        }

        public void increaseDamage(float ratio)
        {
            currentAttackDamage += baseAttackDamage * ratio;
        }

        public bool IsPlayerInRange()
        {
            return playerInRange;
        }

    }
}
