using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FlickerBehavior))]
public class FlickerBehaviorEditor : Editor {

	public enum presetList{BrokenLamp,Horror,Electrical};
	public presetList currentPreset = presetList.BrokenLamp;

	public override void OnInspectorGUI()
	{
		FlickerBehavior thisTarget = (FlickerBehavior)target;

		//General Properties
		EditorGUILayout.LabelField ("General Properties", EditorStyles.boldLabel);
		thisTarget.minimumValue = EditorGUILayout.Slider ("Minimum Light", thisTarget.minimumValue, 0.0f, 1.0f);
		thisTarget.frequency = EditorGUILayout.Slider ("Flicker Strength", thisTarget.frequency, 1.00f, 60.00f);
		thisTarget.chanceToFlicker = EditorGUILayout.Slider ("Chance To Flicker", thisTarget.chanceToFlicker, 0.001f, 0.1f);
		thisTarget.maxFlickerTime = EditorGUILayout.Slider ("Max Flicker Time", thisTarget.maxFlickerTime, 0.01f, 1.0f);
		thisTarget.currentStay = (FlickerBehavior.stayList)EditorGUILayout.EnumPopup ("After Flicker", thisTarget.currentStay);
		EditorGUILayout.Space ();

		//Restriction Properties
		EditorGUILayout.LabelField ("Restriction Properties", EditorStyles.boldLabel);
		thisTarget.restrict = EditorGUILayout.Toggle ("Restrict Flicker", thisTarget.restrict);
		if (thisTarget.restrict) {
			thisTarget.restrictValue = EditorGUILayout.Slider("Time (Seconds)",thisTarget.restrictValue,0.1f,10.0f);
		}
		EditorGUILayout.Space();

		//Preset Properties
		EditorGUILayout.LabelField ("Preset Properties", EditorStyles.boldLabel);
		currentPreset = (presetList)EditorGUILayout.EnumPopup ("Presets", currentPreset);
		if(GUILayout.Button("Use Chosen Preset"))
		{
			SetPresets(thisTarget);
		}
	}

	private void SetPresets(FlickerBehavior thisTarget)
	{
		switch (currentPreset) {
		case presetList.BrokenLamp:
			thisTarget.minimumValue=0.63f;
			thisTarget.frequency=55f;
			thisTarget.chanceToFlicker=0.02f;
			thisTarget.maxFlickerTime=0.52f;
			thisTarget.currentStay = FlickerBehavior.stayList.StayBright;
			thisTarget.restrict=true;
			thisTarget.restrictValue=3f;
			break;
		case presetList.Horror:
			thisTarget.minimumValue=0.3f;
			thisTarget.frequency=41.5f;
			thisTarget.chanceToFlicker=0.04f;
			thisTarget.maxFlickerTime=0.88f;
			thisTarget.currentStay = FlickerBehavior.stayList.Randomize;
			thisTarget.restrict=true;
			thisTarget.restrictValue=1.5f;
			break;
		case presetList.Electrical:
			thisTarget.minimumValue = 0f;
			thisTarget.frequency = 36.5f;
			thisTarget.chanceToFlicker = 0.022f;
			thisTarget.maxFlickerTime=0.39f;
			thisTarget.currentStay = FlickerBehavior.stayList.StayDark;
			thisTarget.restrict=true;
			thisTarget.restrictValue=0.5f;
			break;
		}
	}

}