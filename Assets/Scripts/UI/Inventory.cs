using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    public class Inventory : MonoBehaviour
    {

        public GameObject inventory;

        public void active()
        {
            Time.timeScale = 0;
            inventory.SetActive(true);
        }

        public void disable()
        {
            Time.timeScale = 1;
            inventory.SetActive(false);
        }
    }
}