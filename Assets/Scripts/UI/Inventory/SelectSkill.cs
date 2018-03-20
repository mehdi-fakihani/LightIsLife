using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LIL.Inputs;

namespace LIL
{
    public class SelectSkill : MonoBehaviour, ISelectHandler, IDeselectHandler
    {

        private GameObject selectedSkills;
        private bool selected;
        private bool isUsed;
        private PlayerController pc;
        private SkillManager pm;
        private Profile profile;
        private Transform inventory;
        private GameObject skillTitle;
        private GameObject skillDescription;
        private GameObject skillClass;
        Transform skillSlot;

        public void OnDeselect(BaseEventData eventData)
        {
             selected = false;
        }

        public void OnSelect(BaseEventData eventData)
        {
            selected = true;
            skillTitle.GetComponent<Text>().text = this.gameObject.name + " :" ;
            skillDescription.GetComponent<Text>().text = GeneralData.GetSkillByName(this.gameObject.name).description;
            skillClass.GetComponent<Text>().text = GeneralData.GetSkillByName(this.gameObject.name)._class.name;
            skillClass.GetComponent<Text>().color = GeneralData.GetSkillByName(this.gameObject.name)._class.color;

        }

        // Use this for initialization
        void Awake()
        {
            pm = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillManager>();
            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            profile = pc.getProfile();

            inventory = this.transform.parent.parent.parent;                            // Get the Inventory
            selectedSkills = inventory.GetChild(3).gameObject;                          // Get the SkillsSelected GameObject
            skillTitle = inventory.GetChild(2).GetChild(0).gameObject;                  // Get the SkillTitle GameObject
            skillClass = inventory.GetChild(2).GetChild(1).gameObject;                  // Get the SkillClass GameObject
            skillDescription = inventory.GetChild(2).GetChild(2).gameObject;            // Get the SkillDescription GameObject

            // Load the skill sprite
            string spritePath = GeneralData.GetSkillByName(this.gameObject.name).spritePath;
            this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath);
            this.gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;

            if (!GeneralData.GetSkillByName(this.gameObject.name).deblocked)
            {
                if (!GeneralData.isAvailable(this.gameObject.name))
                {
                    this.gameObject.GetComponent<Selectable>().enabled = false;
                    this.gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 50);
                }
                else
                {
                    this.gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                    this.gameObject.transform.GetChild(2).GetComponentInChildren<Text>().text = "" + GeneralData.GetSkillByName(this.gameObject.name).CapPointsToUnlock;
                    this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (selected)
            {
                
                isUsed = false;
                int index = -1;

                for (int i = 0; i < selectedSkills.transform.childCount; i++)
                {
                    if (selectedSkills.transform.GetChild(i).tag == this.gameObject.name)
                    {
                        isUsed = true;
                        index = i;
                        break;
                    }
                }

                if (!isUsed && GeneralData.GetSkillByName(this.gameObject.name).deblocked)
                {

                    if (profile.getKeyDown(PlayerAction.Skill1))
                    {
                        
                        skillSlot = selectedSkills.transform.GetChild(0);
                        skillSlot.tag = this.gameObject.name;

                        if (!skillSlot.GetChild(1).GetComponent<Image>().enabled)
                            skillSlot.GetChild(1).GetComponent<Image>().enabled = true;

                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(pc.getSkillByName(this.gameObject.name), 0);
                    }
                    if (profile.getKeyDown(PlayerAction.Skill2))
                    {
                        skillSlot = selectedSkills.transform.GetChild(1);
                        skillSlot.tag = this.gameObject.name;

                        if (!skillSlot.GetChild(1).GetComponent<Image>().enabled)
                            skillSlot.GetChild(1).GetComponent<Image>().enabled = true;

                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(pc.getSkillByName(this.gameObject.name), 1);
                    }
                    if (profile.getKeyDown(PlayerAction.Skill3))
                    {
                        skillSlot = selectedSkills.transform.GetChild(2);
                        skillSlot.tag = this.gameObject.name;

                        if (!skillSlot.GetChild(1).GetComponent<Image>().enabled)
                            skillSlot.GetChild(1).GetComponent<Image>().enabled = true;

                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(pc.getSkillByName(this.gameObject.name), 2);
                    }
                    if (profile.getKeyDown(PlayerAction.Skill4))
                    {
                        skillSlot = selectedSkills.transform.GetChild(3);
                        skillSlot.tag = this.gameObject.name;

                        if (!skillSlot.GetChild(1).GetComponent<Image>().enabled)
                            skillSlot.GetChild(1).GetComponent<Image>().enabled = true;

                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(pc.getSkillByName(this.gameObject.name), 3);
                    }
                }
                else if (profile.getKeyDown(PlayerAction.Attack))
                {
                    skillSlot = selectedSkills.transform.GetChild(index);
                    skillSlot.tag = "Untagged";
                    skillSlot.GetChild(1).GetComponent<Image>().enabled = false;
                    skillSlot.GetChild(1).GetComponent<Image>().sprite = null;
                    pc.setSkill(null, index);
                }
            }
        }
    }
}
