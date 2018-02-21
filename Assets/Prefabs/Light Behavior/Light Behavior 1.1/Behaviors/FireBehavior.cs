using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class FireBehavior : LightBehavior {
	#region Public Variables
	public float fireIntensity = 0.01f; //Default intensity of change in color
	public float frequency =0.01f; //Frequency of change in color

	//Range
	public bool changeRange=false;
	public float rangeIntensity = 1.00f;
	public float rangeFrequency = 0.01f;

	public bool dualMode = false; //If true, has two seperate modes
	public float dualFireIntensity = 0.01f; //Dual Intensity of change in color
	public float dualFrequency = 0.01f; // Dual Frequency of change in color
	public bool randomDual = false; //If true, dual mode is randomly entered
	public float mode1Time =1.00f;//time spent in mode1 if randomDual is false
	public float mode2Time = 1.00f;//time spent in mode2 if randomdual is false
	public float chanceOfSwitch = 0.50f;// Maximu amount of time allowed in one mode if randomDual is true
	public float changeFrequency = 1.0f; // Seconds between each random change if randomDual is true

	public bool windSimulation = false; //If true: Simulate fake wind
	public float windFrequency=0.01f; //Frequency of wind simulation
	public float windStrength = 1.01f; //How much fireIntensity should be amplified by wind 
	public bool moveAround=false; //if true: move light around it's point of origin
	public float moveDistance = 0.01f; // How much the light should move around its origin
	#endregion
	
	
	
	#region Private Variables
	private float remainder;//Used to store remainder of fireIntensity
	private float randomValue;//Used to store a randomly generated number
	private Vector3 originalPosition; //Stores a reference to the original position of the lightsource
	private float lightChangeIntensity; //Used as intensity of change
	private bool mode1; //If true: Mode1 active, if False: Mode2 active
	private float newTimeValue; // Used as X in calculations, the result of TimePassed multiplied by frequency
	private float changeOn; //Used to see when next potentional change in mode should occur if dual-mode is active
	private float goBackOn;//Stores the next time mode1 should be reactivated in dual-mode, non random mode
	#endregion
	
	
	//Initialization
	void Start () {
		originalPosition = ThisLight.transform.localPosition; //Stores original local-position
		mode1 = true; //Mode 1 is activated by default to prevent potentional bugs
		newTimeValue = TimePassed; //Prepares newTimeValue


		//Check if dualValue is set to set to random, prepares values for first mode change depnded on this
		if (!randomDual) {

			//Used in time base dual-mode
			changeOn = TimePassed + mode1Time;
			goBackOn = changeOn + mode2Time;
		} else {

			//Used in randomized dual-mode
			changeOn = TimePassed + changeFrequency; //Prepares next potentional change in mode, depended on minimumTime
		}

	}

	
	// Update
	void Update () {
		clamping (); //Clamps all necessary values
		TimePassed = Time.time; //Gets time spent in game

		if (dualMode) {
			ChangeMode();
		}

		//DUAL MODE
		if (dualMode) {
			if (mode1) {
				newTimeValue = TimePassed * frequency;
			} else {
				newTimeValue = TimePassed * dualFrequency;
			}
		} else {
			newTimeValue = TimePassed * frequency;
		}

		ThisLight.color = OriginalColor * UpdateLightSource (); //Updates Color of LightSource

		
		//If true, changes the position of the lightsource
		if (moveAround) {
			changePosition();
		}
	}


	//Called to potentionally change the current mode of the effect
	private void ChangeMode()
	{
		//Check if mode should be changed by random, if not: is changed based on time
		if (!randomDual) {

			//Check time to see if mode2 should be active
			if(TimePassed >= changeOn)
			{
				mode1=false; //Mode 2 is active

				//Check time to see if Mode1 should be reactivated
				if(TimePassed>=goBackOn)
				{
					mode1=true;

					//Prepares for when next change in mode should occur
					changeOn = TimePassed + mode1Time;
					goBackOn = changeOn + mode2Time;
				}
			}


		}else{
			//Check time to see if mode has been active the minimum amount of time
			if(TimePassed >= changeOn)
			{
				//Generate a random value and look if that value is less than the chance of changing mode, if true: change mode
				randomValue = Random.value;
				if(randomValue<=chanceOfSwitch)
				{
					if(!mode1){
						mode1=true; //Mode 1 active
					}
					else{
						mode1=false; //Mode 2 active
					}
				}

				changeOn = TimePassed + changeFrequency;
			}
		}
	}
	
	
	//Calculates changes made to the light source's color
	private float UpdateLightSource()
	{
		//Check if DualMode is active, if false: go to default, if true: check which mode is currently active
		if (dualMode) {
			if (mode1) {
				calculateChangePattern1 (fireIntensity);
			} else {
				calculateChangePattern1(dualFireIntensity);
			}
		} else {
			calculateChangePattern1 (fireIntensity);
		}
		
		return ChangeValue;
	}
	
	
	//Calculates the changes that should be made to the lightsource color
	private void calculateChangePattern1(float intensityValue)
	{
		setIntensity (intensityValue); //Sets intensity of this change

		// Y = -Sin(X * 8 * pi - 1.5) * lightChangeIntensity + remainder
		ChangeValue = -Mathf.Sin (newTimeValue * 8 * Mathf.PI - 1.5f) * lightChangeIntensity + remainder;
		
	}
	

	
	//Sets lightChangeIntensity depending on if light should be affected by "wind" or not
	private void setIntensity(float intensityValue)
	{
		//Check if windsimulation, if true: check if windeffect should be added, if false: uses fireIntensity by default
		if (windSimulation) {
			
			//Generate a random number and see if this is less than windFrequency, if true:light is affected by windeffect
			randomValue = Random.value;
			if (randomValue <= windFrequency) {
				lightChangeIntensity = intensityValue * windStrength; //Amplifies fireIntensity by windStrength
			} else {
				lightChangeIntensity = intensityValue; //If no windEffect, fireIntensity is used by default
			}
			
		} else {
			lightChangeIntensity = intensityValue; //fireIntensity is used by default
		}
		
		remainder = 1 - lightChangeIntensity; //Calculates remainder of lightChangeIntensity
	}
	
	//Gets original position of light and modifes it 
	private void changePosition()
	{
		Vector3 newPosition = originalPosition;//All movment is made from the original position of the lightsource

		//Generate random value, if less than 0.5 - increase position.x, else decrease
		randomValue = Random.value;
		if (randomValue >= 0.5f) {
			newPosition.x -= Random.Range (0, moveDistance);
		} else {
			newPosition.x += Random.Range (0, moveDistance);
		}

		//Generate random value, if less than 0.5 - increase position.y, else decrease
		randomValue = Random.value;
		if (randomValue >= 0.5) {
			newPosition.z -= Random.Range (0, moveDistance);
		} else {
			newPosition.z += Random.Range (0, moveDistance);
		}
		
		ThisLight.transform.localPosition = newPosition;
	}
	
	
	
	//Clamps all necessary values to make sure everything is within valid range
	private void clamping()
	{
		fireIntensity = Mathf.Clamp (fireIntensity, 0.01f, 0.10f);
		frequency = Mathf.Clamp (frequency, 0.01f, 1.00f);
		windFrequency = Mathf.Clamp (windFrequency, 0.01f, 0.10f);
		windStrength = Mathf.Clamp (windStrength, 1.01f, 2.30f);
		moveDistance = Mathf.Clamp (moveDistance, 0.01f, 0.25f);
		
	}
}
