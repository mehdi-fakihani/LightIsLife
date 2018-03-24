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
        GameObject player2;
        bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
        bool player2InRange;
        HealthManager playerHealth;
        HealthManager playerHealth2;
        float timer;                                // Timer for counting up to the next attack.
        private bool multiplayer = false;
        private float currentAttackDamage;

        void Start()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectsWithTag("Player")[0];
            playerHealth = player.GetComponent<HealthManager>();
            anim = GetComponent<Animator>();
            currentAttackDamage = baseAttackDamage;
            if (SceneManager.getMulti())
            {
                player2 = GameObject.FindGameObjectsWithTag("Player")[1];
                playerHealth2 = player2.GetComponent<HealthManager>();
                multiplayer = true;
            }
        }


        void OnTriggerEnter(Collider other)
        {
            // If the entering collider is the player...
            if (other.gameObject == player)
            {
                // ... the player is in range.
                playerInRange = true;
            }
            else if (other.gameObject == player2)
            {
                player2InRange = true;
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

            if (multiplayer && other.gameObject == player2)
            {
                player2InRange = false;
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
                Attack(playerHealth);
                anim.SetBool("walk", false);
            }

            if ( multiplayer && !playerInRange && !player2InRange)
            {

                anim.SetBool("walk", true);
            }

            else if(!playerInRange)
            {
                anim.SetBool("walk", true);
            }

            if ( multiplayer && timer >= timeBetweenAttacks && player2InRange && GetComponent<HealthManager>().isAlive())
            {
                // ... attack.
                Attack(playerHealth2);
                anim.SetBool("walk", false);
            }

        }


        void Attack( HealthManager health)
        {
            // Reset the timer.
            timer = 0f;
            health.harm(currentAttackDamage);
            anim.SetTrigger("attack");
        }

        public void increaseDamage(float ratio)
        {
            currentAttackDamage += baseAttackDamage * ratio;
        }

        public bool IsPlayerInRange()
        {
            return playerInRange || player2InRange;
        }

    }
}
