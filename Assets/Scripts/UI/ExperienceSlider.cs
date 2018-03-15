using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSlider : MonoBehaviour {

    private UnityEngine.UI.Slider slider;
    // Use this for initialization
    void Start () {
        slider = GetComponent<UnityEngine.UI.Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        slider.value = slider.value + Time.deltaTime;
    }
}
