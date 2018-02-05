using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Walking toward point clicked");
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            Debug.Log("Mouse ScrollWheel");
        if (Input.GetAxis("Hit") != 0)
            Debug.Log("Hit");
        if (Input.GetAxis("Skill1") != 0)
            Debug.Log("Skill 1");
        if (Input.GetAxis("Skill2") != 0)
            Debug.Log("Skill 2");
        if (Input.GetAxis("Skill3") != 0)
            Debug.Log("Skill 3");
    }
}
