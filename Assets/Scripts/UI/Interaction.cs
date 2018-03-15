using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LIL.Inputs;

namespace LIL
{
    public class Interaction : MonoBehaviour
    {

        public GameObject interactionPanel;
        private string key = "E";

      

        public void displayInteractionMsg(ProfilsID input)
        {
            var model = Profile.Models[input];
            key = model.keys[PlayerAction.Interaction].ToString();

            if (key.StartsWith("Keyboard")) key = key.Substring(8);
            if (key.StartsWith("Gamepad")) key = key.Substring(7);

            interactionPanel.SetActive(true);
            interactionPanel.GetComponentInChildren<Text>().text = "press " + key + " to use";
        }

        public void hideInteractionMsg()
        {
            interactionPanel.SetActive(false);
            interactionPanel.GetComponentInChildren<Text>().text = "";
        }
    }
}