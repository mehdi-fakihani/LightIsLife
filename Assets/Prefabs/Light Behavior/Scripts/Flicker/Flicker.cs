using UnityEngine;
using System.Collections;

public class Flicker : LightBehavior {

	/**DESCRIPTION
	 * ThisLight.color is multiplied by ChangeValue to change color of light source
	 * ChangeValue transition between 1 and minimunValue
	 * transition is depnded on a randomly generated number between 0 and 100
	 * if random number is less than flickerFrequency ChangeValue = 1
	 * if random number is greater than flickerFrequency ChangeValue = minimumValue
	 * **/


	#region public variables
	public float minimumValue=0.5f; //Minimum allowed value for lightsource
	public int flickerFrequency = 15; //The likelyhood of the light flickering, keep between 0 and 100
	#endregion


	#region private variables
	private int randomValue; //Used to generate a random integer
	#endregion


	//Initialization
	void Start()
	{
		//Makes sure that minimumValue is set to a valid value
		if (minimumValue < 0) {
			minimumValue = 0;
		} else if (minimumValue > 1) {
			minimumValue=1;
		}
		
		if (flickerFrequency < 0) {
			flickerFrequency = 0;
		} else if (flickerFrequency > 100) {
			flickerFrequency=100;
		}
	}
	
	void Update()
	{
		ThisLight.color = OriginalColor * UpdateLightSource (); //Updates the color of the lightsource
		
	}
	
	//Used to make potential changes in changeValue
	private float UpdateLightSource()
	{
		
		//generate a random number between 0 and 10
		randomValue = Random.Range (0, 100);

		
		//check if random number generated above is a lesser value than flickerFrequency
		if (randomValue <= flickerFrequency) {
			//Light should be bright
			ChangeValue = 1;
		} else {
			//Light should be dark
			ChangeValue = minimumValue;
		}
		
		return ChangeValue;//Returns float changeValue
	}
}
