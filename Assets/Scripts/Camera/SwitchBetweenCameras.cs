using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBetweenCameras : MonoBehaviour {

    public GameObject cam1;
    public GameObject cam2;
    public GameObject gameUI;

    // Use this for initialization
    void Start () {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (cam1.activeSelf)
            {
                cam1.SetActive(false);
                cam2.SetActive(true);
                gameUI.SetActive(false);
            }
            else
            {
                cam1.SetActive(true);
                cam2.SetActive(false);
                gameUI.SetActive(true);
            }
        }



    }
}
