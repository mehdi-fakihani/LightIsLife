using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LIL
{
    public class CapacityPoint : MonoBehaviour
    {

        private int playerNum = 1;
        private int capacityPoint;

        // Use this for initialization
        void Start()
        {
            playerNum = this.transform.parent.name[this.transform.parent.name.Length - 1];

            setCapacityPoint();

        }

        // Update is called once per frame
        void Update()
        {
           // if (capacityPoint != GeneralData.GetCapacityPoints(playerNum-48))
           setCapacityPoint();

        }

        public void setCapacityPoint()
        {
            capacityPoint = GeneralData.GetCapacityPoints(playerNum-48);
            if (capacityPoint > 1) this.GetComponent<Text>().text = "You have " + capacityPoint + " capacity Points";
            else if (capacityPoint >= 0) this.GetComponent<Text>().text = "You have " + capacityPoint + " capacity point";
        }
    }
}

