using UnityEngine;
using System.Collections;

public class DualColorSimple : Lerping {

	/**DESCRIPTION
	 * ThisLight.color is controlled by Color.Lerp()
	 * 
	 * Fades from OriginalColor to secondColor at first
	 * When secondColor is reached, jumps back to OriginalColor
	 * 
	 * Fade speed is controlled by Timer which is depnded on secondsBetween
	 * **/

	#region public variables
	public Color secondColor=Color.green; //Stores the second color
	public float secondsBetween = 1; //Amount of time, in seconds, between each full color switch
	#endregion

	//Initialize
	void Start()
	{
		Initialize (); //Initializes base values in Lerping.Cs

		//Check that secondsBetween is a valid number above 0
		if (secondsBetween < 0) {
			secondsBetween=0;
		}

		//Calculates what each change in timer should be
		Calculate (secondsBetween);
	}

	void Update()
	{
		ChangeTimer (); //Updates timer
		UpdateLightSource ();
	}

	private void UpdateLightSource()
	{
		//Color changes from originalColor to secondColor, depended on timer
		ThisLight.color = Color.Lerp (OriginalColor, secondColor, Timer);
	}
	

}
