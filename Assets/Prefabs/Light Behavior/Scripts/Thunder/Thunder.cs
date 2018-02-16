using UnityEngine;
using System.Collections;

public class Thunder : LightBehavior {

	/**DESCRIPTION
	 * ThisLight.color is multiplied by ChangeValue to change color of light source
	 * 
	 * timeBetween sets how long between each lightningstrike, next lightningstrike is stored within nextThunder
	 * when TimePassed is the same value or a greater value than nextThunder a lightningstrike occurs
	 * During Lightningstrike, ChangeValue is depended on TimePassed; ChangeValue = -Sin(TimePassed * 8) * Random(0,1)
	 * 
	 * How long each lightningstrike should occur is controlled by flashTime
	 * When Lightningstrike should end is stored within stopThunder
	 * **/

	#region Public variables
	public float timeBetween = 12; // The Amount of time, in seconds, between each lightning strike'
	public AudioClip sound; //Stores a potential sound effect
	#endregion

	#region private variables
	private float nextThunder; //Stores the next time when the lightning should occur
	private float flashTime; // How long it will remain lit when lightning occur
	private float stopThunder; //Used to store the next time when lightning should stop
	private AudioSource audioSource;// Used to play potentional soundeffect
	#endregion

	
	//Initialize
	void Start()
	{
		//Makes sure that timeBetween is a value above 0
		if (timeBetween < 0) {
			timeBetween =0;
		}

	
		nextThunder = 0 + timeBetween;//Prepares when next thunder should occur, is depended on timeBetween
		flashTime = 0.4f; //Sets the amount of time it will remain lit when lightning occur
		stopThunder = nextThunder + flashTime;//Prepares stopThunder, is depended on timeBetween & flashTime

		//Checks if there is a soundeffect added
		if (sound != null) {
			audioSource = gameObject.AddComponent<AudioSource>(); //Adds a AudioSource component to play soundeffect
		}


		ChangeValue = 0; //Resets ChangeValue
		
	}


	void Update()
	{
		TimePassed = Time.time; //Gets game time
		ThisLight.color = OriginalColor * UpdateLightSource (); //Updates the color of the lightsource
		
	}


	//Used to make potential changes in changeValue
	private float UpdateLightSource()
	{
		//Check if its time for next lightningStrike
		if (TimePassed >= nextThunder) {

			//Y = -Sin(X * 8) * Random(0,1)
			ChangeValue = -Mathf.Sin (TimePassed * 8) * Random.value;

			//Check if its time to exit this lightning strike
			if(TimePassed >= stopThunder)
			{

				//Check if a soundeffects exists
				if(sound!=null)
				{
					audioSource.PlayOneShot(sound); //Plays soundeffect
				}


				//Stores the next time lightning strike should occur
				nextThunder = TimePassed + timeBetween;
				stopThunder = nextThunder + flashTime;
			}

		} else {

			ChangeValue = 0;

		}

		return ChangeValue;//Returns float changeValue
	}


}
