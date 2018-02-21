using UnityEngine;
using System.Collections;

public class Torch : LightBehavior {

	/**DESCRIPTION
	 * ThisLight.color is multiplied by ChangeValue to change color of light source
	 * ChangeValue is depended on TimePassed,  ChangeValue = -Sin(TimePassed * 8 * pi) * 0.05 + 0.95
	 * **/

	void Update()
	{
		TimePassed = Time.time; //Gets game time
		ThisLight.color = OriginalColor * UpdateLightSource (); //Updates the color of the lightsource

	}


	//Used to make potential changes in changeValue
	private float UpdateLightSource()
	{
		// Y = -Sin(X * 8 * pi) * 0.05 + 0.95
		ChangeValue = -Mathf.Sin (TimePassed * 8 * Mathf.PI) * 0.05f + 0.95f;

		return ChangeValue;//Returns float changeValue
	}

}
