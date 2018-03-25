using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LIL.Inputs;
using System;
using LIL;

public class ChangeKeys : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private bool selected;
    private int playerNum;
    private Event keyEvent;
    private Key newKey;

    public void OnDeselect(BaseEventData eventData)
    {
        selected = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        selected = true;

    }


    // Use this for initialization
    void Start () {
        playerNum = SettingsMenu.getPlayerNum();
	}

    // Update is called once per frame
    void OnGUI()
    {
        if (selected)
        {
            if (playerNum != SettingsMenu.getPlayerNum())
                playerNum = SettingsMenu.getPlayerNum();

            keyEvent = Event.current;
            if (keyEvent.isKey)
            {
                Key[] values = (Key[])Enum.GetValues(typeof(Key));
                for (int i = 0; i < values.Length; i++)
                {

                    if ((int)keyEvent.keyCode == (int)values[i])
                    {
                        newKey = values[i];
                        if (!Profile.Models[playerNum].keys.ContainsValue(newKey))
                        {
                            PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                            Profile.Models[playerNum].keys[action] = newKey;
                            if(newKey.ToString().StartsWith("Keyboard"))
                                this.GetComponent<Text>().text = newKey.ToString().Substring(8);
                            else if(newKey.ToString().StartsWith("Gamepad"))
                                this.GetComponent<Text>().text = newKey.ToString().Substring(7);
                        }
                        break;
                    }
                }
            }
        }
    }
}
