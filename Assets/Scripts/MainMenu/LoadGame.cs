using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

    public void Load()
    {
        if(this.gameObject.GetComponentInChildren<Text>().text != "EMPTY SLOT")
        {
            GeneralData.fileName = this.gameObject.GetComponentInChildren<Text>().text + ".sav";
            GeneralData.Load(GeneralData.fileName);
            SceneManager.LoadScene(GeneralData.sceneName);
        }
    }
}
