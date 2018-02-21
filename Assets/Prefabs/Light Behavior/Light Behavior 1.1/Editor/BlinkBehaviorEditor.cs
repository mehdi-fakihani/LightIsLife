using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BlinkBehavior))]
public class BlinkBehaviorEditor : Editor {

	public override void OnInspectorGUI()
	{
		BlinkBehavior thisTarget = (BlinkBehavior)target;

		thisTarget.currentType = (BlinkBehavior.typeList)EditorGUILayout.EnumPopup ("Type",thisTarget.currentType);
		EditorGUILayout.LabelField ("General Properties",EditorStyles.boldLabel);
		thisTarget.startBright = EditorGUILayout.Toggle ("Start Bright", thisTarget.startBright);
		thisTarget.frequency = EditorGUILayout.FloatField ("Seconds Between", thisTarget.frequency);

		EditorGUILayout.Space ();

		switch(thisTarget.currentType)
		{
		case BlinkBehavior.typeList.OnOff:
			EditorGUILayout.LabelField ("On/Off Properties",EditorStyles.boldLabel);
			thisTarget.minimumValue =EditorGUILayout.Slider("Minimum Value", thisTarget.minimumValue,0.0f,1.0f);
			break;

		case BlinkBehavior.typeList.ColorChange:
			EditorGUILayout.LabelField("Color Change Properties",EditorStyles.boldLabel);
			thisTarget.secondColor = EditorGUILayout.ColorField("Second Color",thisTarget.secondColor);
			break;

		default:
			Debug.LogError("ERROR: Unrecognized type in currenType in BlinkBehaviorEditor on target: " + thisTarget.gameObject);
			break;
		}
	}
}
