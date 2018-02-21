using UnityEngine;
using System.Collections;

public class FlickerBehavior : LightBehavior {

	#region Public Variables
	//Enum containing all possible variations of behaviors when light stops flickering
	public enum stayList{StayDark,StayBright,Randomize};
	public stayList currentStay = stayList.StayDark;
	
	public float frequency = 28f; //The frequency in which the light flickers
	public float minimumValue = 0.0f; //Minimum allowed color strength in lightsource
	public float chanceToFlicker = 0.1f; //Chance to start flickering
	public float maxFlickerTime = 1.0f; //Maximum allowed time flickering can occur at 1 time
	public bool restrict = false; //If true: light must wait a set amount of time before it can flicker again
	public float restrictValue = 1.0f; //The time the light must wait if Restrict = true
	#endregion
	
	#region Private Variables
	private float randomValue; //Used to store randomly generated numbers
	private bool flickering; //If true: a flickering process is ongoing
	private float minFlickerTime = 0.001f; //minimum allowed time the light can flicker
	private float stopFlickering; //Used to store next time light should stop flickering
	private float newTimeValue; //Used as X in calculation, stores the Result of multiplying TimePassed and frequency
	private float stopRestrict; //Stores when light should no longer be restricted from flickering
	#endregion
	
	//Initialization
	void Start()
	{
		flickering = false;
		stopRestrict = TimePassed + restrictValue; //Prepares stopRestrict to prevent potentional bugs
	}
	
	
	void Update()
	{
		TimePassed = Time.time; //Grabs time spent in game
		
		//Check if flickering should be restricted, if not: go directly to flickering process
		if (restrict) {
			
			//Check if it's time to stop restricting flickering
			if(TimePassed >= stopRestrict)
			{
				RandomFlickering();
			}
			
		} else {
			RandomFlickering();
		}
	}
	
	//Call to either make changes in LightSource or to start a new flickering process
	private void RandomFlickering()
	{
		//If Flickering is not yet actived, randomly tries to active flickering
		if (!flickering) {
			//Generate a random value between 0-1
			randomValue = Random.value;
			
			//If random value is less than our chance to flicker, new flickering process is started
			if (randomValue <= chanceToFlicker) {
				flickering = true; //Flickering process has started
				randomValue = Random.Range (minFlickerTime, maxFlickerTime); //Generate random time for this flickering process
				stopFlickering = TimePassed + randomValue; //Prepares when we should stop flickering
			}
			
		} else {
			//Updates color of lightsource
			ThisLight.color  = OriginalColor * UpdateLightSource();
		}
	}
	
	
	//Called to make changes in LightSource
	private float UpdateLightSource()
	{
		//Check if Flickering should stop
		if (TimePassed >= stopFlickering) {
			EndFlicker();
			
		} else {
			newTimeValue = TimePassed * frequency; //Multiplies TimPassed with frequency for better control of flickering
			
			//Y = -Sin(X)
			ChangeValue = -Mathf.Sin (newTimeValue);
			
			//Make sure ChangeValue is not less than minimumValue
			if (ChangeValue <= minimumValue) {
				ChangeValue=minimumValue;
			}
			
		}
		
		return ChangeValue; //Returns float ChangeValue to act as multiplier
	}
	
	
	
	//Call when Ending a Flicker to reset values and Set ChangeValue depending on currentStay
	private void EndFlicker()
	{
		flickering = false; //Turns of flickering
		
		//ChangeValue is assigned a value depedning on what currentStay has been chosen
		switch (currentStay) {
			
		case stayList.StayDark:
			ChangeValue=minimumValue; //Set to stay dark
			break;
			
		case stayList.StayBright:
			ChangeValue = 1f; //Set to stay Bright
			break;
			
		case stayList.Randomize:
			
			//Random number between 0-1 is generated, if less than 0.5 Light Stays bright, else stays dark
			randomValue = Random.value;
			if(randomValue<=0.5f)
			{
				ChangeValue = 1.0f;
			}else{
				ChangeValue = minimumValue;
			}
			break;
			
		}
		
		//If resrict is used, stopRestrict is prepared for when a new flicker is allowed
		if (restrict) {
			stopRestrict = TimePassed + restrictValue;
		}
	}

}
