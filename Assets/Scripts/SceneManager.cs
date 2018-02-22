using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    private static bool multiplayer;

    public void ChangeScene(bool multiplayer)
    {
        SceneManager.multiplayer = multiplayer;
        Application.LoadLevel("SceneDorian2");
    }

    public static bool getMulti()
    {
        return multiplayer;
    }
}
