using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixUIRotation : MonoBehaviour {

    Quaternion rotation;
    GameObject mainCamera;
    Quaternion cameraRotation;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraRotation = mainCamera.transform.rotation;
        rotation = transform.rotation;
    }
    void Update()
    {
        if(mainCamera.transform.rotation.y != cameraRotation.y)
        {
            Quaternion newCamRotation = mainCamera.transform.rotation;
            Quaternion newRotation = rotation;
            Debug.Log("rotation y before " + cameraRotation.y + " rotation y after " + newCamRotation.y);
            Debug.Log("rotation y of the canvas " + transform.rotation.y);
            newRotation.y += cameraRotation.y - newCamRotation.y;
            transform.rotation = newRotation;
        }
        else    transform.rotation = rotation;
    }
}
