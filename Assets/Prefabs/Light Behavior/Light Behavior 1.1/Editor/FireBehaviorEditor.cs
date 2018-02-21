using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FireBehavior))]
public class FireBehaviorEditor : Editor {

	public enum presetList{CampFire,Candle,Torch,WildFire};
	public presetList chosenPreset=presetList.CampFire;


	public override void OnInspectorGUI()
	{
		FireBehavior thisTarget = (FireBehavior)target;

		EditorGUILayout.Space ();

		//General Properties
		EditorGUILayout.LabelField ("General Properties", EditorStyles.boldLabel);
		thisTarget.fireIntensity = EditorGUILayout.Slider ("Fire Strength", thisTarget.fireIntensity, 0.01f, 0.10f);
		thisTarget.frequency = EditorGUILayout.Slider ("Strength Frequency", thisTarget.frequency, 0.01f, 1.00f);
		EditorGUILayout.Space ();


		//Dual Properties
		EditorGUILayout.LabelField ("Dual Strength Properties", EditorStyles.boldLabel);
		thisTarget.dualMode = EditorGUILayout.Toggle ("Dual Strength", thisTarget.dualMode);

		if (thisTarget.dualMode) {
			thisTarget.dualFireIntensity = EditorGUILayout.Slider("Fire Strength 2", thisTarget.dualFireIntensity,0.01f,0.10f);
			thisTarget.dualFrequency = EditorGUILayout.Slider("Strength Frequency 2", thisTarget.dualFrequency,0.01f,1.00f);
			EditorGUILayout.Space ();
			thisTarget.randomDual = EditorGUILayout.Toggle("Random Dual Mode", thisTarget.randomDual);
			EditorGUI.indentLevel ++;
			if(thisTarget.randomDual){
				thisTarget.randomDual = true;
				thisTarget.changeFrequency = EditorGUILayout.Slider ("Minimum Time",thisTarget.changeFrequency,0.01f,10.00f);
				thisTarget.chanceOfSwitch =EditorGUILayout.Slider("Chance To Switch",thisTarget.chanceOfSwitch,0.01f,1.00f);
			}else{
				thisTarget.randomDual =false;
				thisTarget.mode1Time = EditorGUILayout.Slider("Time In Mode 1", thisTarget.mode1Time,0.01f,10.00f);
				thisTarget.mode2Time = EditorGUILayout.Slider("Time In Mode 2", thisTarget.mode2Time,0.01f,10.00f);
			}
			EditorGUI.indentLevel --;
		}

		EditorGUILayout.Space ();

		//Wind Properties
		EditorGUILayout.LabelField ("Flicker Porperties", EditorStyles.boldLabel);
		thisTarget.windSimulation = EditorGUILayout.Toggle ("Flicker simulation", thisTarget.windSimulation);
		if (thisTarget.windSimulation) {
			thisTarget.windFrequency = EditorGUILayout.Slider("Flicker Frequency", thisTarget.windFrequency,0.01f,0.10f);
			thisTarget.windStrength = EditorGUILayout.Slider("Flicker Strength", thisTarget.windStrength,1.01f,2.30f);
		}
		EditorGUILayout.Space ();


		//Move Properties
		EditorGUILayout.LabelField ("Move Properties", EditorStyles.boldLabel);
		thisTarget.moveAround = EditorGUILayout.Toggle ("Move Around", thisTarget.moveAround);
		if (thisTarget.moveAround) {
			thisTarget.moveDistance = EditorGUILayout.Slider("Move Distance", thisTarget.moveDistance,0.01f,0.25f);
		}

		EditorGUILayout.Space ();
		//Presets
		EditorGUILayout.LabelField ("Presets", EditorStyles.boldLabel);
		chosenPreset = (presetList)EditorGUILayout.EnumPopup ("Chose Preset:", chosenPreset);
		if(GUILayout.Button("Use Chosen Preset"))
		{
			SetPresetValues(thisTarget);
		}
	}


	private void SetPresetValues(FireBehavior thisTarget)
	{
		switch(chosenPreset)
		{
		case presetList.CampFire:
			thisTarget.fireIntensity = 0.06f;
			thisTarget.frequency = 1f;
			thisTarget.windSimulation=true;
			thisTarget.windFrequency = 0.1f;
			thisTarget.windStrength = 1.57f;
			thisTarget.moveAround =true;
			thisTarget.moveDistance = 0.077f;
			thisTarget.dualMode=true;
			thisTarget.dualFireIntensity = 0.074f;
			thisTarget.dualFrequency = 0.57f;
			thisTarget.randomDual=false;
			thisTarget.mode1Time = 2.3f;
			thisTarget.mode2Time = 1.4f;
			break;

		case presetList.Candle:
			thisTarget.fireIntensity =0.021f;
			thisTarget.frequency = 0.26f;
			thisTarget.windSimulation = true;
			thisTarget.windFrequency = 0.034f;
			thisTarget.windStrength = 1.78f;
			thisTarget.moveAround=true;
			thisTarget.moveDistance=0.024f;
			thisTarget.dualMode=true;
			thisTarget.dualFireIntensity = 0.045f;
			thisTarget.dualFrequency = 0.22f;
			thisTarget.randomDual = false;
			thisTarget.mode1Time=5f;
			thisTarget.mode2Time=1f;
			break;

		case presetList.Torch:
			thisTarget.fireIntensity = 0.1f;
			thisTarget.frequency=1f;
			thisTarget.windSimulation=true;
			thisTarget.windFrequency =0.058f;
			thisTarget.windStrength = 2.1f;
			thisTarget.moveAround=true;
			thisTarget.moveDistance = 0.163f;
			thisTarget.dualMode=true;
			thisTarget.dualFireIntensity=0.091f;
			thisTarget.dualFrequency=0.5f;
			thisTarget.randomDual=true;
			thisTarget.changeFrequency=2f;
			thisTarget.chanceOfSwitch=0.5f;
			break;

		case presetList.WildFire:
			thisTarget.fireIntensity = 0.069f;
			thisTarget.frequency = 1f;
			thisTarget.windSimulation=true;
			thisTarget.windFrequency=0.088f;
			thisTarget.windStrength = 2.3f;
			thisTarget.moveAround=true;
			thisTarget.moveDistance=0.25f;
			thisTarget.dualMode=true;
			thisTarget.dualFireIntensity=0.091f;
			thisTarget.dualFrequency=0.26f;
			thisTarget.randomDual=true;
			thisTarget.changeFrequency=2.6f;
			thisTarget.chanceOfSwitch=0.44f;
			break;

		default:
			Debug.LogError("ERROR: Unrecognized value in chosenPreset");
			break;
		}
	}
}
