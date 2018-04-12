using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceText : MonoBehaviour {

    // Use this for initialization
    private UnityEngine.UI.Text text;
    private GameObject experienceUI;
    private int playerNum;

    void Start()
    {
        experienceUI = transform.parent.gameObject;
        playerNum = (int)experienceUI.name[experienceUI.name.Length - 1] - 48;
        text = GetComponent<UnityEngine.UI.Text>();
    }
    void Update()
    {
        text.text = " level : " + GeneralData.getLevel(playerNum);
    }
}
