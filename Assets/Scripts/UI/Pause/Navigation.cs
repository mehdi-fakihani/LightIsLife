using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using LIL.Inputs;

namespace LIL
{
    public class Navigation : MonoBehaviour
    {

        private int playerNum = 1;
        private Pause pause;
        private GameObject Player;
        private Profile profile;

        void Start()
        {
            pause = this.transform.parent.GetComponent<Pause>();
            playerNum = pause.getPlayerNum();
            profile = new Profile(playerNum,0);
        }

        void Update()
        {

            AxisEventData ad = new AxisEventData(EventSystem.current);
            if (profile.getKeyDown(PlayerAction.Up))
            {
                ad.moveDir = MoveDirection.Up;
            }
            else if (profile.getKeyDown(PlayerAction.Down))
            {
                ad.moveDir = MoveDirection.Down;
            }
            else if (profile.getKeyDown(PlayerAction.Left))
            {
                ad.moveDir = MoveDirection.Left;
            }
            else if (profile.getKeyDown(PlayerAction.Right))
            {
                ad.moveDir = MoveDirection.Right;
            }
            else
                return;

            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, ad, ExecuteEvents.moveHandler);


        }
    }
}
