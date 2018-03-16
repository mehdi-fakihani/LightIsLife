using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFuel : MonoBehaviour {
    
    public float lightDurability = 15.0f;
    public float lightFullRange = 150f;
    public float lightMinRange = 50f;

    private Light soulLight;
    private float timer;

	// Use this for initialization
	void Start () {
        soulLight = GetComponentInChildren<Light>();
        timer = lightDurability;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LightSource"))
        {
            LightProvider provider = other.gameObject.GetComponent<LightProvider>();
            if (provider != null)
            {
                FillLight(provider.GetFuelFactor());
            }
        }
    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, lightDurability);
		soulLight.range = GetLightRange();
    }


    private void FillLight(float fuelFactor)
    {
        timer += lightDurability * fuelFactor;
    }

    public float GetLightRange()
    {
        return lightMinRange + (lightFullRange - lightMinRange) * timer / lightDurability;
    }
}
