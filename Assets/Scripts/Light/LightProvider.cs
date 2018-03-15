using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProvider : MonoBehaviour {

    public float fuelFactor = 1.0f;
    
    private GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            LightFuel fuel = player.GetComponent<LightFuel>();
            if(fuel != null)
            {
                fuel.FillLight(fuelFactor);
            }
        }
    }
}
