using UnityEngine;
using System.Collections;

public class IntenseEmergency : LightBehavior {

	/**DESCRIPTION
	 * ThisLight.color is multiplied by ChangeValue to change color of light source
	 * TimePassed is set to always be a number between 0 - 1
	 * ChangeValue is depended on TimePassed,  ChangeValue = -Sin(TimePassed * pi) + 1
	 * **/

	void Update()
	{
		TimePassed = Time.time; //Gets game time
		TimePassed = TimePassed - Mathf.Floor (TimePassed); //Normalize timePassed to be a value between 0-1
		ThisLight.color = OriginalColor * UpdateLightSource (); //Updates the color of the lightsource
		
	}
	
	//Used to make potential changes in changeValue
	private float UpdateLightSource()
	{

		// Y = -Sin(X * pi) + 1
		ChangeValue = -Mathf.Sin (TimePassed * Mathf.PI) + 1;

		return ChangeValue;//Returns float changeValue
	}
	
}
