using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class BlinkBehavior : LightBehavior {

	#region Public Variables
	public enum typeList{OnOff,ColorChange};
	public typeList currentType = typeList.OnOff;
	
	public int blinks = 2; //Amount of blinks should be preformed
	public bool startBright = true; //If true: the light starts of with originalColor
	public float frequency = 1.0f; //Amount of time, in seconds, between each blink
	public float minimumValue = 0.0f; // MInimum value for lightsource in  OnOff mode
	
	public Color secondColor = Color.red; // Second Color used in ColorChange
	#endregion
	
	#region Private Variables
	private float changeOn; //Stores next time light should change in color
	#endregion
	
	//Initialization
	void Start () {
		changeOn = TimePassed + frequency; //Prepares changeOn for first change
		
		//Prepares the lightsource to be either turned on or off at start
		if (startBright) {
			ThisLight.color = OriginalColor;
		} else {
			ThisLight.color = OriginalColor * minimumValue;
		}
		
	}
	
	// Update
	void Update () {
		
		TimePassed = Time.time; //Grabs time spent in game
		frequency = Mathf.Clamp (frequency, 0, 100); //Clamps frequency to be a number between 0 and 100
		
		//Check if time to change light source
		if (TimePassed >= changeOn) {
			
			//Depnding on what type of blink is chosen, preforms different actions
			switch(currentType)
			{
				
			case typeList.OnOff:
				ThisLight.color = OriginalColor * UpdateLightSourceOnOff(); //Updates light source on/off
				break;
				
			case typeList.ColorChange:
				UpdateLightSourceColorChange(); //Updates Light source Color Change
				break;
				
			default:
				Debug.LogError("ERROR: Unrecognized type in currentType.");
				break;
			}
			
			changeOn = TimePassed + frequency; //Sets next time light should be changed
		}
		
	}
	
	
	
	private void UpdateLightSourceColorChange()
	{
		if (ThisLight.color == OriginalColor) {
			ThisLight.color = secondColor;
		} else {
			ThisLight.color = OriginalColor;
		}
	}
	
	//Calculates changes made to the light source's color when currentType = OnOff
	private float UpdateLightSourceOnOff()
	{
		//Sets ChangeValue to equal the oposite of the light in current state
		if (ThisLight.color == OriginalColor) {
			ChangeValue = minimumValue;
		} else {
			ChangeValue = 1f;
		}
		
		return ChangeValue;
	}
}
