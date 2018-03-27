using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using LIL.Inputs;

namespace LIL
{
    public class SelectPauseOption : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        private bool selected;
        Pause pause;
        int playerNum;
        Profile profile;

        private void Awake()
        {
            pause = this.transform.parent.parent.GetComponent<Pause>();
            playerNum = pause.getPlayerNum();
            profile = new Profile(playerNum, 0);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            selected = false;
        }

        public void OnSelect(BaseEventData eventData)
        {
            selected = true;
        }

        private void Update()
        {
            if (selected && profile.getKeyDown(PlayerAction.Submit))
            {
                if (this.gameObject.name == "Continue_Btn")
                {
                    selected = false;
                    pause.Continue();
                }
                else if(this.gameObject.name == "MainMenu_Btn")
                {
                    selected = false;
                    pause.MainMenu();
                }
            }
        }
    }
}

