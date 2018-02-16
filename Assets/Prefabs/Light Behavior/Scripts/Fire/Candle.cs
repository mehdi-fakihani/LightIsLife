using UnityEngine;
using System.Collections;

public class Candle : LightBehavior {

	/**DESCRIPTION
	 * ThisLight.color is multiplied by ChangeValue to change color of light source
	 * ChangeValue is depended on TimePassed,  ChangeValue = -Sin(TimePassed * pi) * 0.04 + 0.96
	 * **/

	void Update()
	{
		TimePassed = Time.time; //Gets game time
		ThisLight.color = OriginalColor * UpdateLightSource (); //Updates the color of the lightsource
	}

	//Used to make potential changes in changeValue
	private float UpdateLightSource()
	{
		// Y = -Sin(X * pi) * 0.04 + 0.96
		ChangeValue = -Mathf.Sin (TimePassed * Mathf.PI) * 0.04f + 0.96f;

		return ChangeValue; //Returns float changeValue
	}

}
