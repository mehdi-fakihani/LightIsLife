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
            if(playerNum == 2) inventory_2.SetActive(true);
            else inventory_1.SetActive(true);
            //Time.timeScale = 0;
        }

        public void disable(int playerNum, float[] _pos)
        {
            float[] pos = GeneralData.getPlayerbyNum(playerNum).pos;
            GeneralData.getPlayerbyNum(playerNum).pos= _pos;

            if (playerNum == 2) inventory_2.SetActive(false);
            else inventory_1.SetActive(false);

            GeneralData.Save(GeneralData.fileName);
            pos = GeneralData.getPlayerbyNum(playerNum).pos;
            //Time.timeScale = 1;
        }

    }
}