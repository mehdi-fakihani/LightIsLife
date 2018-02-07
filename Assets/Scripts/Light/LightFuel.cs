using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFuel : MonoBehaviour {

    public Light lightObject;
    public float lightDurability = 20.0f;
    public float lightIntensity = 1.5f;
    
    private float timer;

	// Use this for initialization
	void Start () {
        timer = lightDurability;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, lightDurability);
		lightObject.intensity = lightIntensity * timer / lightDurability;
    }

    public void FillLight(float fuelFactor)
    {
        timer += lightDurability * fuelFactor;
    }
}
