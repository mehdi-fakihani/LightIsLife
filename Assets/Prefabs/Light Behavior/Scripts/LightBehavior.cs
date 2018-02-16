using UnityEngine;
using System.Collections;

public class LightBehavior : MonoBehaviour {

	/** DESCRIPTION
	 * This is the base class for all light behaviors
	 * It contains all of the general variables used by all behaviors
	 * ALL of the possible behaviors added should in some way inherit from this base class
	 * 
	 * All general variables are initialized within LightBehavior.CS - Void Awake()
	 * If any of the individual behaviors want to make potential changes to the base variables within their initializion
	 * This is done within their void Start();
	 **/


	#region Private Variables
	private Light thisLight; //Stores a reference to the current lightsoruce
	private Color originalColor; //Stores the original color of the lightsource
	private float timePassed; //Stores gametime
	private float changeValue; // Used as a multiplier when changing the color of the lightsource
	#endregion

	#region Get/Set
	// GET thisLight
	public Light ThisLight{
		get{return thisLight;}
	}

	// GET originalColor
	public Color OriginalColor{
		get{ return originalColor;}
	}

	// GET - SET timePassed
	public float TimePassed{
		get{ return timePassed;}
		set{ timePassed = value;}
	}

	// GET - SET changeValue
	public float ChangeValue{
		get{ return changeValue;}
		set{ changeValue = value;}
	}
	#endregion

	//Initialization of general values
	void Awake()
	{
		thisLight = this.GetComponent<Light> (); //Stores a reference of the object.Light() for future use
		if (thisLight != null) {
			originalColor = thisLight.color; // Stores originalColor of object.Light()
		} else {
			//Object did not have a component of .Light() attached to it
			Debug.LogError("ERROR:");
		}
		
		timePassed = Time.time; // Gets current GameTime
		changeValue = 1; // To prevent possible bugs, changeValue is by default set to 1
	}


}
