using UnityEngine;
using System.Collections;

public class Lerping : LightBehavior {

	/** DESCRIPTION
	 * This is the base class for all behaviors using any sort of .Lerp
	 * It contains all of the general variables used by all Lerp-behaviors
	 * ALL of the possible Lerp-behaviors added should in some way inherit from this base class
	 * 
	 * All general variables are initialized within Lerping.CS - Public Void Initialize()
	 * This method should be called from child class BEFORE initializing child class values
	 * This is done within their void Start();
	 **/

	#region private variables
	private Vector3 originalPosition; //Stores originalPosition of light source
	private Vector3 secondPosition; //Stores the secondPosition of light source
	
	private float changeInTimer; //How much timer should change each update
	private bool movingTowardSecond; // if light is currently moving towards the second position/Color
	
	private float timer;//Used as a subsitute to TimePassed
	#endregion

	#region GET/SET
	public Vector3 OriginalPosition{
		get{ return originalPosition;}
	}

	public Vector3 SecondPosition{
		get{ return secondPosition;}
		set{ secondPosition = value;}
	}

	public float ChangeInTimer{
		get{ return changeInTimer;}
	}

	public bool MovingTowardsSecond{
		get{return movingTowardSecond;}
		set{movingTowardSecond=value;}
	}

	public float Timer{
		get{return timer;}
		set{timer=value;}
	}
	#endregion
	
	
	//Called from child class to initialize basic values of Lerping
	public void Initialize()
	{
		originalPosition = ThisLight.transform.position; //Gets original position of light
		timer = 0;//Reset timer
		movingTowardSecond = true;//reset movingTowardsSecond
	}

	
	//Called by child class to update timer
	public void ChangeTimer()
	{
		timer += changeInTimer;//Increase timer

		//If timer has reached 1, reset and set bool movingTowardsSecond equal to its opposite
		if (timer >= 1) {
			timer = 0;
			movingTowardSecond = !movingTowardSecond;
		}
	}

	//Called to calculate how many times light should be subject to change each second
	public void Calculate(float secondsBetween)
	{
		//Calculate how many times light have to change to keep up with secondsBetween
		changeInTimer = 1 / (60 * secondsBetween); 
	}


}
