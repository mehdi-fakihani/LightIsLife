using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PoliceBehavior))]
public class PoliceBehaviorEditor : Editor {

	public override void OnInspectorGUI()
	{
		PoliceBehavior thisTarget = (PoliceBehavior)target;

		//General Properties
		EditorGUILayout.LabelField ("General Properties", EditorStyles.boldLabel);
		thisTarget.secondColor = EditorGUILayout.ColorField ("SecondColor", thisTarget.secondColor);
		thisTarget.timeBetween = EditorGUILayout.Slider ("Frequency", thisTarget.timeBetween, 0.1f, 1.0f);
		thisTarget.distanceBetween = EditorGUILayout.Slider("Distance Between", thisTarget.distanceBetween,-10f,10f);
		EditorGUILayout.Space ();

		//Blink Properties
		EditorGUILayout.LabelField ("Blink Properties", EditorStyles.boldLabel);
		thisTarget.blink = EditorGUILayout.Toggle ("Blinking", thisTarget.blink);
		if (thisTarget.blink) {
			thisTarget.blinkFrequency = EditorGUILayout.Slider("Blink Frequency",thisTarget.blinkFrequency,30f,50f);
		}
	}

}
