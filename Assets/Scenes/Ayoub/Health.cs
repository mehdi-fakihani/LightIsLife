using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//------------------------------------------------------------------------
//
//  Name:   Health.cs
//
//  Desc:   The class responsible for the health 
//
//  Attachment : This class is used in the "Player" GameObject
//
//  Last modification :
//      Aub AH - 04/02/2018 : Init version
//      
//------------------------------------------------------------------------


public class Health : MonoBehaviour
{
	// Public :
	
	public AudioClip soundDead, soundHurt;		// Hurt and Dead AudioClips
	public int maxHealth=100;					// Max health
	public Slider healthSlider;					// The Slider of the players' health
	//public GameOverScript gameOver;				// GameOver script that is launched when the player is dead
	public string hurtAnimation, deadAnimation;	// The name of the animations to play
	
	// Private :
	
	private Animator animator;					// The players' Animator
    private AudioSource audioSource;			// The players' AudioSource 
    private float currentHealth;				// The current health of the player
	private bool dead;							// Player dead or not
	private bool endGameReached;				// End Game reached


    //---------------------------------------------------------
    // Method called when the game starts
    //---------------------------------------------------------
    void Start() {

		endGameReached = false;
	
        // Health
		dead = false;
        currentHealth = maxHealth;
        healthSlider.value = 1;					// It's a ratio (0 <-> 1)

        // Get the Animator of the player
        animator = GetComponent<Animator>();
        // Get the AudioSource of the player
        audioSource = GetComponent<AudioSource>();
		
    }


    //---------------------------------------------------------
    // This method is called when the player is hurt
    //---------------------------------------------------------
    public void takeDammage(float amount) {
        
		if (!dead) {

            //Health
            currentHealth -= amount;
            if (currentHealth < 0) currentHealth = 0;
            healthSlider.value = currentHealth / (float) maxHealth;

            if (currentHealth == 0) playerDead();
            else {
				animator.SetTrigger(hurtAnimation);
				if (!audioSource.isPlaying) audioSource.PlayOneShot(soundHurt);
            }
            

        }
    }


    //---------------------------------------------------------
    // This method is called when the player is dead
    // /!\ This mehtod is not complet /!\
    //---------------------------------------------------------
    public void playerDead() {
        
		dead = true;

        // Play death animation
        animator.SetTrigger(deadAnimation);
        
		// Play death sound
        audioSource.PlayOneShot(soundDead);

        //Pause le jeu
        Time.timeScale = 0;
        //gameOver.ShowPanel(false);
    }


    public float getCurrentHealth() {
        return currentHealth;
    }


}