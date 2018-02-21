using UnityEngine;
using System.Collections;

public class Blink : LightBehavior {


	/**DESCRIPTION
	 * ThisLight.color is multiplied by ChangeValue to change color of light source
	 * ChangeValue transition between OriginalColor and minimumValue to blink
	 * When each transition in color should occur is depended on float timeBetween
	 * **/


	public float minimumValue; //The minimum value that changeValue can be, should be a value between 0-1
	public float timeBetween = 1; // How long, in seconds, between each change in color
	public bool startBright=true; //If true the light should start in bright mode

	private float changeLight; //Stores the next time the light should be changed

	//Initialize
	void Start()
	{
		//Check that blinkFrequency has a value above or equal to 0, else set it to 0
		if (timeBetween < 0)
			timeBetween = 0;

		changeLight = TimePassed + timeBetween; //Prepare changeLight for the first change, depended on blinkfrequency

		if (!startBright) {
			ChangeValue=minimumValue;
		}

	}
	
	void Update()
	{
		TimePassed = Time.time; //Gets game time
		ThisLight.color = OriginalColor * UpdateLightSource (); //Updates the color of the lightsource
		
	}
	

	//Used to make potential changes in changeValue
	private float UpdateLightSource()
	{

		//Check if its time to change the lightsource
		if (TimePassed >= changeLight) {

			//Set ChangeValue to its opposite
			if(ChangeValue ==1)
			{
				ChangeValue = minimumValue;
			}else{
				ChangeValue = 1;
			}

			changeLight = TimePassed + timeBetween; //Sets next time the light should be changed
		}

		return ChangeValue;//Returns float changeValue
	}
	

}
