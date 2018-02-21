using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class PoliceBehavior : LightBehavior {


	#region Public Variables
	public float timeBetween = 0.7f; //Time, in seconds, between each change in lightsource
	public Color secondColor = Color.blue; //Second color of lightsource
	public float distanceBetween = 1.0f; // distance the lightsource will jump
	public bool blink = false; //If true, the lightsource will blink
	public float blinkFrequency = 45f; //the rate at which the light blinks
	#endregion
	
	
	
	#region Private Variables
	private float changeOn; //When lightsource should be changed
	private Vector3 originalPosition; //Stores the original position of ligtsource
	private Vector3 secondPosition; //Stores second position of lightsource
	private bool isOriginalColor; //If true: Light is in originalColor mode
	#endregion
	
	
	
	//Initialization
	void Start()
	{
		isOriginalColor = true; //Color mode is set to originalColor
		changeOn = TimePassed + timeBetween; //Prepares when the lightsource should be subject to change next
		originalPosition = ThisLight.transform.localPosition; //Stores original local position of light source
	}
	
	
	void Update()
	{
		TimePassed = Time.time; //Grabs time spent in game
		
		//If Blink is active, the light blinks according to what color mode is currently active
		if (blink) {
			if(isOriginalColor)
			{
				ThisLight.color = OriginalColor * UpdateLightSourceBlink(); //OriginalColor blinks
			}else{
				ThisLight.color = secondColor * UpdateLightSourceBlink(); //SecondColor blinks
			}
		}
		
		//Check if it is time to change lightsource color and position
		if (TimePassed >= changeOn) {
			UpdateLightSourcePattern(); //Changes color mode and position of lightsource
			changeOn = TimePassed + timeBetween; //Calculates when next change should take place
		}
		
	}
	
	
	
	//Called to make current color "blink"
	private float UpdateLightSourceBlink()
	{
		// Y = -Sin(X * blinkFrequency) * 0.3 + 0.7
		ChangeValue = -Mathf.Sin (TimePassed * blinkFrequency) * 0.3f + 0.7f;
		
		return ChangeValue;
	}
	
	
	
	//Called to change color mode && to change position of light
	private void UpdateLightSourcePattern()
	{
		//Set color mode to opposite
		if (isOriginalColor) {
			ThisLight.color = secondColor;
			isOriginalColor = false; //Light is no longer in originalColor mode
		} else {
			ThisLight.color = OriginalColor;
			isOriginalColor = true; //Light is now in originalColor mode
		}
		
		//Change position to opposite
		if (ThisLight.transform.localPosition == originalPosition) {
			
			// calculates where secondposition should be located and stores it in secondPosition variable
			secondPosition = originalPosition;
			secondPosition.x +=distanceBetween;
			
			//Sets lightsource position to = secondPosition
			ThisLight.transform.localPosition = secondPosition;
			
			
		} else {
			//Sets lightsource position to = originalPosition
			ThisLight.transform.localPosition=originalPosition;
		}
	}
}
