using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixUIRotation : MonoBehaviour {

    GameObject mainCamera;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        transform.position += new Vector3(0, 0.7f, 0);
    }

    void LateUpdate()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}
