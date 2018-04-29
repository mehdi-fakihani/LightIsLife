using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LIL
{
    public class Inventory : MonoBehaviour
    {

        public GameObject inventory_1;
        public GameObject inventory_2;


        public void active(int playerNum)
        {
            if (playerNum == 2)
            {
                inventory_2.SetActive(true);
                inventory_2.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Selectable>().Select();
            }
            else
            {
                inventory_1.SetActive(true);
                inventory_1.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Selectable>().Select();
            }
            //Time.timeScale = 0;
        }

        public void disable(int playerNum, float[] _pos)
        {
            float[] pos = GeneralData.getPlayerbyNum(playerNum).pos;
            GeneralData.getPlayerbyNum(1).pos= _pos;
            if(GeneralData.multiplayer)
                GeneralData.getPlayerbyNum(2).pos = _pos;

            if (playerNum == 2) inventory_2.SetActive(false);
            else inventory_1.SetActive(false);

            GeneralData.Save(GeneralData.fileName);
            pos = GeneralData.getPlayerbyNum(playerNum).pos;
            //Time.timeScale = 1;
        }

    }
}