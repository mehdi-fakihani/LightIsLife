using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//------------------------------------------------------------------------
//
//  Name:   Health.cs
//
//  Desc:   The class responsible for the health 
//
//  Attachment : This class is used in the characters' (both player and enemy) GameObject
//
//  Last modification :
//      Aub AH - 04/02/2018 : Init version
//      
//------------------------------------------------------------------------

namespace LIL
{
    public class HealthEnemy : MonoBehaviour
    {

        // Public :

        public AudioClip soundDead, soundHurt;      // Hurt and Dead AudioClips
        public int maxHealth = 10;                  // Max health

        // Private :

        private Animator animator;                  // The Animator
        private AudioSource audioSource;            // The AudioSource 
        private float currentHealth;                // The current health of the character
        private bool dead;                          // Check if dead or not


        //---------------------------------------------------------
        // Method called when the game starts
        //---------------------------------------------------------
        void Start()
        {

            // Health
            dead = false;
            currentHealth = maxHealth;

            // Get the Animator of the character
            animator = GetComponent<Animator>();
            // Get the AudioSource of the character
            audioSource = GetComponent<AudioSource>();

        }


        //---------------------------------------------------------
        // This method is called when the character is hurt
        //---------------------------------------------------------
        public void takeDammage(float amount)
        {

            if (!dead)
            {

                //Health
                currentHealth -= amount;

                Debug.Log("Enemy hurt, health is now : " + currentHealth);
                if (currentHealth < 0) currentHealth = 0;

                if (currentHealth == 0) playDead();
                else
                {
                    animator.SetTrigger("hurt");
                    if (!audioSource.isPlaying) audioSource.PlayOneShot(soundHurt);
                }


            }
        }


        //---------------------------------------------------------
        // This method is called when the character is dead
        // /!\ This mehtod is not complet /!\
        //---------------------------------------------------------
        public void playDead() // COPIED IN ENEMY MOVEMENT - To get death behavior (Sidney)
        {

            dead = true;

            // Play death animation
            animator.SetTrigger("death");

            // Play death sound
            audioSource.PlayOneShot(soundDead);
            Debug.Log("Enemy dead, health is now : " + currentHealth);
            Destroy(this.gameObject, 1.5f);
            
        }


        public float getCurrentHealth()
        {
            return currentHealth;
        }

        public bool isAlive()
        {
            return !dead;
        }
    }

}
