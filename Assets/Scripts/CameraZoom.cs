using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public float zoom = 15.0f; // distance between the camera and the player
    [SerializeField] private float zoomMin = 10.0f;
    [SerializeField] private float zoomMax = 20.0f;
    [SerializeField] private float angle = 60.0f; // in-place rotation of the camera
    [SerializeField] private float fixedAngle = -20.0f; // angle between the camera and the player

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        zoom -= Input.GetAxis("Mouse ScrollWheel");
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);


        transform.localEulerAngles = new Vector3(angle, 0, 0);
        transform.localPosition = new Vector3(0, zoom * Mathf.Cos(Mathf.Deg2Rad * fixedAngle), zoom * Mathf.Sin(Mathf.Deg2Rad * fixedAngle));
	}
}
