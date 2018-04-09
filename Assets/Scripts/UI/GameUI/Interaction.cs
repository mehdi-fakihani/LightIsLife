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

      

        public void displayInteractionMsg(int playerNum)
        {
            var model = Profile.Models[playerNum];
            key = model.keys[PlayerAction.Interaction].ToString();

            if (key.StartsWith("Keyboard")) key = key.Substring(8);
            if (key.StartsWith("Gamepad")) key = key.Substring(7);

            interactionPanel.SetActive(true);
            interactionPanel.GetComponentInChildren<Text>().text = "Press " + key + " to use";
        }

        public void displayInteractionMsg(string msg)
        {
            StartCoroutine(ShowMessage(msg, 2));
        }

        public void hideInteractionMsg()
        {
            interactionPanel.SetActive(false);
            interactionPanel.GetComponentInChildren<Text>().text = "";
        }

        IEnumerator ShowMessage(string message, float delay)
        {
            interactionPanel.GetComponentInChildren<Text>().text = message;
            interactionPanel.SetActive(true);
            yield return new WaitForSeconds(delay);
            hideInteractionMsg();
        }

    }
}