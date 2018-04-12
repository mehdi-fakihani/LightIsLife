using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    public class LightFuel : MonoBehaviour
    {

        public float lightDurability = 15.0f;
        public float lightFullRange = 150f;
        public float lightMinRange = 50f;
        public float healRatio = 0.25f;

        private SpiritController spirit;
        private Light soulLight;
        private float timer;

        // Use this for initialization
        void Start()
        {
            spirit = GetComponent<SpiritController>();
            soulLight = GetComponentInChildren<Light>();
            timer = lightDurability;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Campfire"))
            {
                LightProvider provider = other.gameObject.GetComponent<LightProvider>();
                if (provider != null)
                {
                    FillLight(provider.GetFuelFactor());
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, lightDurability);
            soulLight.range = GetLightRange();
        }


        private void FillLight(float fuelFactor)
        {
            timer += lightDurability * fuelFactor;
            //spirit.GetTarget().GetComponent<HealthPlayer>().Heal(healRatio);
        }

        public float GetLightRange()
        {
            return lightMinRange + (lightFullRange - lightMinRange) * timer / lightDurability;
        }
    }

}
