using UnityEngine;
using System.Collections;
using System;

namespace LIL
{
    public class EnemyAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
        public int attackDamage = 10;               // The amount of health taken away per attack.


        Animator anim;                              // Reference to the animator component.
        GameObject player;
        GameObject player2;     // Reference to the player GameObject.
        bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
        bool player2InRange;
        HealthPlayer playerHealth;
        HealthPlayer playerHealth2;
        float timer;                                // Timer for counting up to the next attack.
        private bool multi;


        void Start()
        {
            // Setting up the references.
            multi = SceneManager.getMulti();
            player = GameObject.FindGameObjectsWithTag("Player")[0];
            playerHealth = player.GetComponent<HealthPlayer>();
            if (multi == true)
            {
                player2 = GameObject.FindGameObjectsWithTag("Player")[1];
                playerHealth2 = player2.GetComponent<HealthPlayer>();
            }
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
            if (multi == true)
            {
            if (other.gameObject == player2)
                {
                    player2InRange = true;
                }
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
            if (multi == true)
            {
                if (other.gameObject == player2)
                {
                    player2InRange = false;
                }
            }
        }


        void Update()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if (timer >= timeBetweenAttacks &&
                playerInRange &&
                GetComponent<HealthEnemy>().isAlive() &&
                // Added by Sidney
                GetComponent<SkillManager>().canCast())
            {
                // ... attack.
                Attack(playerHealth);
                anim.SetBool("walk", false);
            }

            if (!playerInRange)
            {

                anim.SetBool("walk", true);
            }

            if (multi == true)
            {
                if (timer >= timeBetweenAttacks &&
               player2InRange &&
               GetComponent<HealthEnemy>().isAlive() &&
               // Added by Sidney
               GetComponent<SkillManager>().canCast())
                {
                    // ... attack.
                    Attack(playerHealth2);
                    anim.SetBool("walk", false);
                }

                if (!player2InRange)
                {

                    anim.SetBool("walk", true);
                }
            }

        }


        void Attack(HealthPlayer health)
        {
            // Reset the timer.
            timer = 0f;
            health.takeDammage(attackDamage);
            anim.SetTrigger("attack");
        }
    }
}
