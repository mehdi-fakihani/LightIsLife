using UnityEngine;
using System.Collections;

public class MoveBetween : Lerping {

	/**DESCRIPTION
	 * ThisLight.transform.position is controlled by .Lerp()
	 * 
	 * Moves from OriginalPosition towards secondPosition
	 * when secondPosition is reached, moves back towards OriginalPosition
	 * 
	 * Move speed is controlled by Timer which is depended on secondsBetween
	 * **/

	#region public Variables
	public GameObject targetLocation; //Stores new location
	public float secondsBetween = 1; //Amount of time, in seconds, between each switch in direction
	#endregion


	//Initialize
	void Start()
	{
		Initialize ();//Initializes base values in Lerping.Cs

		//if targetLocation object is not null, get second position
		if (targetLocation != null) {
			SecondPosition = targetLocation.transform.position;
		} else {
			Debug.LogError("ERROR: targetLocation was null, MoveOneWay.cs");
		}

		//Check that secondsBetween is a valid number above 0
		if (secondsBetween < 0) {
			secondsBetween=0;
		}

		//Calculate what each change in timer should be
		Calculate (secondsBetween);
	}


	void Update()
	{
		ChangeTimer (); //Updates timer
		UpdateLightSource ();
	}


	//Used to move the lightsource
	private void UpdateLightSource()
	{
		if (MovingTowardsSecond){
			//Light moves from originalPosition to secondPosition, depended on timer
			ThisLight.transform.position = Vector3.Lerp (OriginalPosition, SecondPosition, Timer);
		} else {
			//Light changes from secondPosition to originalPosition, depended on timer
			ThisLight.transform.position = Vector3.Lerp (SecondPosition, OriginalPosition, Timer);
		}
	}

}
