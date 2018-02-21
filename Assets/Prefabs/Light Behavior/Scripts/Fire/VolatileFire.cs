using UnityEngine;
using System.Collections;

public class VolatileFire : LightBehavior {

	/**DESCRIPTION
	 * ThisLight.color is multiplied by ChangeValue to change color of light source
	 * The behavior transitions between "calm mode" and "intense mode"
	 * Transition between these are depended on calmTime and intenseTime
	 *
	 * ChangeValue is depended on TimePassed, Calm Mode: ChangeValue = -Sin(TimePassed * 8 * pi - 1.5) * 0.05 + 0.95
	 * ChangeValue is depended on TimePassed, Intense Mode: ChangeValue = -Sin(TimePassed * 8 * pi -1.5) * 0.08 + 0.92
	 * **/

	#region public variables
	public int calmTime = 5; //How long, in seconds, the light should behave 'calm'
	public int intenseTime = 3; // How long, in seconds, the light should behave 'intense'
	#endregion

	#region private variables
	private float changeFireOn; //When the light should start behaving 'intense' next time
	private float stopChangeFireOn; //When the light sould go back to behaving 'calm' next time
	#endregion

	//Initialize
	void Start()
	{
		//Making sure that calmTime and intenseTime are assigned valid values
		if (calmTime < 1) {
			calmTime=1;
		}
		if (intenseTime < 1) {
			intenseTime = 1;
		}

		changeFireOn = TimePassed + calmTime;//Prepares changeFireOn for the first transition into intense mode
		stopChangeFireOn = changeFireOn + intenseTime;//Prepares stopChangeFireOn for the first transition back into calm mode
	}


	void Update()
	{
		TimePassed = Time.time; //Gets game time
		ThisLight.color = OriginalColor * UpdateLightSource (); //Updates the color of the lightsource
		
	}


	//Used to make potential changes in changeValue
	private float UpdateLightSource()
	{
	
		//Check if its time to go into intense-mode, else continue in calm-mode
		if (TimePassed >= changeFireOn) {

			//Intense Mode
			// Y = -Sin(X * 8 * pi - 1.5) * 0.08 + 0.92
			ChangeValue = -Mathf.Sin (TimePassed * 8 * Mathf.PI - 1.5f) * 0.08f + 0.92f;

			//Check if its time exit intense-mode
			if(TimePassed >=stopChangeFireOn)
			{
				changeFireOn=TimePassed + calmTime; //Stores when should go into intense-mode next time
				stopChangeFireOn = changeFireOn + intenseTime; //Stores when should exit intense-mode next time
			}

		} else {

			//Calm Mode
			// Y = -Sin(X * 8 * pi - 1.5) * 0.05 + 0.95
			ChangeValue = - Mathf.Sin (TimePassed * 8 * Mathf.PI -1.5f) * 0.05f + 0.95f;

		}

		return ChangeValue;//Returns float changeValue
	}

}
