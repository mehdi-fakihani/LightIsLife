using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace LIL
{
    public class SkillUIManager : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private GameObject skill1;
        [SerializeField] private GameObject skill2;
        [SerializeField] private GameObject skill3;
        [SerializeField] private GameObject skill4;
        [SerializeField] private GameObject cooldown1;
        [SerializeField] private GameObject cooldown2;
        [SerializeField] private GameObject cooldown3;
        [SerializeField] private GameObject cooldown4;
        [SerializeField] private GameObject chargeCount1;
        [SerializeField] private GameObject chargeCount2;
        [SerializeField] private GameObject chargeCount3;
        [SerializeField] private GameObject chargeCount4;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (player.GetAllSkill()[0] !=null)
            {
                skill1.GetComponent<Image>().sprite = player.GetAllSkill()[0].getImage();
                chargeCount1.GetComponent<Text>().text = player.GetAllSkill()[0].chargesAvailable().ToString();
                skill1.SetActive(true);
            }
            if (player.GetAllSkill()[1] != null)
            {
                skill2.GetComponent<Image>().sprite = player.GetAllSkill()[1].getImage();
                chargeCount2.GetComponent<Text>().text = player.GetAllSkill()[1].chargesAvailable().ToString();
                skill2.SetActive(true);
            }
            if (player.GetAllSkill()[2] != null)
            {
                skill3.GetComponent<Image>().sprite = player.GetAllSkill()[2].getImage();
                chargeCount3.GetComponent<Text>().text = player.GetAllSkill()[2].chargesAvailable().ToString();
                skill3.SetActive(true);
            }
            if (player.GetAllSkill()[3] != null)
            {
                skill4.GetComponent<Image>().sprite = player.GetAllSkill()[3].getImage();
                chargeCount4.GetComponent<Text>().text = player.GetAllSkill()[3].chargesAvailable().ToString();
                skill4.SetActive(true);
            }
        }

        public void StartCooldown(float cooldown, int indexSkill)
        {
            if (indexSkill == 0)
            {
                cooldown1.SetActive(true);
                cooldown1.GetComponent<Image>().fillAmount -= Time.deltaTime / cooldown;

                if (cooldown1.GetComponent<Image>().fillAmount == 0)
                {
                    ResetCooldown(cooldown1);
                }
            }
            if (indexSkill == 1)
            {
                cooldown2.GetComponent<Image>().fillAmount -= Time.deltaTime / cooldown;
                cooldown2.SetActive(true);
                if (cooldown2.GetComponent<Image>().fillAmount == 0)
                {
                    ResetCooldown(cooldown2);
                }
            }
            if (indexSkill == 2)
            {
                cooldown3.GetComponent<Image>().fillAmount -= Time.deltaTime / cooldown;
                cooldown3.SetActive(true);
                if (cooldown3.GetComponent<Image>().fillAmount == 0)
                {
                    ResetCooldown(cooldown3);
                }
            }
            if (indexSkill == 3)
            {
                cooldown4.GetComponent<Image>().fillAmount -= Time.deltaTime / cooldown;
                cooldown4.SetActive(true);
                if (cooldown4.GetComponent<Image>().fillAmount == 0)
                {
                    ResetCooldown(cooldown4);
                }
            }
        }

        public void ResetCooldown(GameObject cooldown)
        {
                cooldown.GetComponent<Image>().fillAmount = 1;
                cooldown.SetActive(false);
        }
    }
}
