using UnityEngine;
using System.Collections;

public class HorrorFlicker : LightBehavior {

	/**DESCRIPTION
	 * ThisLight.color is multiplied by ChangeValue to change color of light source
	 * TimePassed is set to always be a number between 0 and 1
	 *
	 * ChangeValue is depended on TimePassed
	 * If TimePassed is a number below 0.1 then ChangeValue is a random number between 0 and 1
	 * If TimePassed is a number greater than 0.1 then ChangeValue is randomly set to either 1 or minimumValue
	 * **/

	#region public variables
	public float minimumValue=0.6f;//Minimum allowed value for lightsource
	#endregion

	#region private variables
	private bool change;//Used to see if times to change the state of lightsource
	#endregion

	//Initialize
	void Start()
	{
		//Makes sure that minimumValue is set to a valid value
		if (minimumValue < 0) {
			minimumValue = 0;
		} else if (minimumValue > 1) {
			minimumValue=1;
		}

		change = false;
	}


	void Update()
	{
		TimePassed = Time.time; //Gets game time
		TimePassed = TimePassed - Mathf.Floor (TimePassed); //Normalize TimePassed to be a value between 0-1

		ThisLight.color = OriginalColor * UpdateLightSource (); //Updates the color of the lightsource
		
	}


	//Used to make potential changes in changeValue
	private float UpdateLightSource()
	{
		//While timePassed is of a value less than 0.1, the light will flicker
		if (TimePassed <= 0.1f) {

			// changeValue is set to a random number between 0-1 multiplied by 2
			ChangeValue = 1 - (Random.value * 2);
			change = true; //Set change light to true

		} else if (change) {

			//The changevalue is set randomly to either minimumvalue or 1 until it starts to flicker again
			ChangeValue = Random.value;
			if(ChangeValue>=0.5f)
			{
				ChangeValue=1;
			}else{
				ChangeValue=minimumValue;
			}

			change=false;
		}

		return ChangeValue;//Returns float changeValue
	}


}
