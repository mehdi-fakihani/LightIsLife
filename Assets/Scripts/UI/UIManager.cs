using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] GameObject SecondPlayerExpUI;
    [SerializeField] GameObject SecondPlayerHealthUI;
    // Use this for initialization
    void Start () {
		if (GeneralData.multiplayer)
        {
            SecondPlayerExpUI.SetActive(true);
            SecondPlayerHealthUI.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
