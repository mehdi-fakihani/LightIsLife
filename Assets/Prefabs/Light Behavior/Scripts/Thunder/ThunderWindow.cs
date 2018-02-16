using UnityEngine;
using System.Collections;

public class ThunderWindow : LightBehavior {

	/**DESCRIPTION
	 * ThisLight.color is multiplied by ChangeValue to change color of light source
	 * 
	 * timeBetween sets how long between each lightningstrike, next lightningstrike is stored within nextThunder
	 * when TimePassed is the same value or a greater value than nextThunder a lightningstrike occurs
	 * 
	 * How many flashes should occur during one lightningstrike is randomly changed and stored within randomValue
	 * randomValue is depended on minFlashes, maxFlashes and amplitude: randomValue = (Random(minFlashes, maxFlashes)) * amplitude
	 * 
	 * During Lightningstrike, ChangeValue is depended on TimePassed and randomValue; ChangeValue = Sin(TimePassed * randomValue * pi)
	 * 
	 * How long each lightningstrike should occur is controlled by flashTime
	 * When Lightningstrike should end is stored within stopThunder
	 * **/


	#region public variables
	public float timeBetween = 12; // The Amount of time, in seconds, between each lightning strike'
	#endregion


	#region private variables
	private float nextThunder; //Stores the next time when the lightning should occur
	private float flashTime; // How long it will remain lit when lightning occur
	private float stopThunder; //Used to store the next time when lightning should stop

	private int minFlashes; //The minimum amount of flashes that will occur during 1 lightningstrike
	private int maxFlashes; //The maximum amount of flashes that can occur during 1 lightning strike
	private int randomValue; //Used to store a random value
	private int amplitude; //Used to increase randomvalue
	private bool makeRandom; //used to see if randomValue should be assigned a new randomly generated value
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
		minFlashes = 1;
		maxFlashes = 3;

		amplitude = 3; //Sets amplitude which will be a multiplier to randomValue during lightningstrike
		makeRandom = true; //Should only be false during a lightningstrike
		ChangeValue = 0;

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

			//Check if this is the first flash inside of this lightningstrike
			if(makeRandom)
			{
				randomValue= Random.Range (minFlashes, maxFlashes+1);
				randomValue = randomValue * amplitude;

				//Lets us know that next flash will not be the first in this lightning strike
				makeRandom = false;
			}

			// Y = Sin(X * Flashes * pi)
			ChangeValue =  Mathf.Sin (TimePassed * randomValue * Mathf.PI);

			//Check if its time to exit this lightning strike
			if(TimePassed>= stopThunder)
			{
				//Stores the next time lightning strike should occur
				nextThunder = TimePassed + timeBetween;
				stopThunder = nextThunder + flashTime;

				//lets us know that next time lightning should occur it will be our first flash
				makeRandom=true;
			}


		} else {
			ChangeValue=0;
		}

		return ChangeValue;//Returns float changeValue
	}

}
