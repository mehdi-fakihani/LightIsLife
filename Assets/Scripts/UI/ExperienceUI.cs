using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.UI
{
    public class ExperienceUI : MonoBehaviour
    {

        private UnityEngine.UI.Slider slider;
        private UnityEngine.UI.Text text;
        private int level = 1;
        [SerializeField] private ExperienceManager player;
        // Use this for initialization
        void Start()
        {
            slider = GetComponentInChildren<UnityEngine.UI.Slider>();
            text = GetComponentInChildren<UnityEngine.UI.Text>();
            player.setLevel(level);
        }

        // Update is called once per frame
        void Update()
        {
            slider.value = player.GetExperience();
            text.text = " level : " + player.GetLevel();
            if (slider.value == slider.maxValue)
            {
                level++;
                player.setLevel(level);
                Debug.Log(level);
                ResetExperience();
            }
        }

        void ResetExperience()
        {
            slider.minValue = slider.value;
            slider.maxValue = slider.maxValue + 3 ;
        }
    }
}
