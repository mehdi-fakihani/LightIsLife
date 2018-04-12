using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.UI
{
    public class ExperienceUI : MonoBehaviour
    {

        private UnityEngine.UI.Slider slider;
        private UnityEngine.UI.Text text;
        private int playerNum;
        private int level = 1;
        [SerializeField] private ExperienceManager player;
        // Use this for initialization
        void Awake()
        {
            slider = GetComponentInChildren<UnityEngine.UI.Slider>();
            text = GetComponentInChildren<UnityEngine.UI.Text>();
            playerNum = (int)gameObject.name[gameObject.name.Length - 1] - 48;

        }

        // Update is called once per frame
        void Update()
        {
            if (GeneralData.GetExperience(playerNum) % slider.maxValue != 0)
                slider.value = GeneralData.GetExperience(playerNum) % slider.maxValue;

            if (slider.value != 0 && GeneralData.GetExperience(playerNum) % slider.maxValue == 0 && GeneralData.GetExperience(playerNum)>0)
            {
                slider.value = GeneralData.GetExperience(playerNum) % slider.maxValue;
                GeneralData.incrLevel(playerNum);
                //ResetExperience();
            }
        }

        void ResetExperience()
        {
            slider.minValue = slider.value;
            slider.maxValue = slider.maxValue + 3 ;
        }
    }
}
