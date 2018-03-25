using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    public class SceneManager : MonoBehaviour
    {

        private static bool multiplayer;

        public void ChangeScene(bool multiplayer)
        {
            SceneManager.multiplayer = multiplayer;
            Application.LoadLevel("aubTest");
        }

        public static bool getMulti()
        {
            return multiplayer;
        }
    }
}

