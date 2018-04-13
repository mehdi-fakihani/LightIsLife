using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour {

    private int playerNum;
    private Text text;
    private float maxHealth;
    private float currentHealth;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        playerNum = (int)transform.parent.gameObject.name[transform.parent.gameObject.name.Length - 1] - 48;
        maxHealth = GeneralData.GetInitialLife(playerNum);
        currentHealth = GeneralData.GetCurrentLife(playerNum);
        text.text = currentHealth + "/" + maxHealth;
    }
	
	// Update is called once per frame
	void Update () {

        if (maxHealth != GeneralData.GetInitialLife(playerNum))
            maxHealth = GeneralData.GetInitialLife(playerNum);

        if (currentHealth != GeneralData.GetCurrentLife(playerNum))
            currentHealth = GeneralData.GetCurrentLife(playerNum);

        text.text = currentHealth + "/" + maxHealth;

    }
}
