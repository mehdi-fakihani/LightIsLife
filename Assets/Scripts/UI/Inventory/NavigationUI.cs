using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using LIL.Inputs;

namespace LIL
{
    public class NavigationUI : MonoBehaviour {

        private int playerNum=1;
        private GameObject Player;
        private Profile profile;

        void Start()
        {
            playerNum = this.transform.name[this.transform.name.Length - 1];
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject player = players[0];

            foreach (GameObject _player in players)
            {
                if (_player.name[_player.name.Length - 1] == (char)playerNum)
                {
                    player = _player;
                    break;
                }
            }

            profile = player.GetComponent<PlayerController>().getProfile();
        }

        void Update()
        {
            
            AxisEventData ad = new AxisEventData(EventSystem.current);
            if (profile.getKeyDown(PlayerAction.Up))
                ad.moveDir = MoveDirection.Up;
            else if (profile.getKeyDown(PlayerAction.Down))
                ad.moveDir = MoveDirection.Down;
            else if (profile.getKeyDown(PlayerAction.Left))
                ad.moveDir = MoveDirection.Left;
            else if (profile.getKeyDown(PlayerAction.Right))
                ad.moveDir = MoveDirection.Right;
            else
                return;

            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, ad, ExecuteEvents.moveHandler);

            
        }
    }
}

