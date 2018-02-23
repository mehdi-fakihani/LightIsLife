using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.UI
{
    public class HealthSlider : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        private HealthManager health;
        private UnityEngine.UI.Slider slider;

        // Use this for initialization
        void Start()
        {
            health = player.GetComponent<HealthManager>();
            slider = GetComponent<UnityEngine.UI.Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            slider.value = health.getInitialLife() / health.getLife();
            Debug.Log("life percent = " + slider.value);
        }
    }
}
