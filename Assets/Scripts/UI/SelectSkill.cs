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
        private GameObject skillTitle;
        private GameObject skillDescription;
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
          
        }

        // Use this for initialization
        void Start()
        {
            pm = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillManager>();
            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            profile = pc.getProfile();
            selectedSkills = GameObject.FindGameObjectWithTag("SkillSelected");
            skillTitle = GameObject.FindGameObjectWithTag("SkillTitle");
            skillDescription = GameObject.FindGameObjectWithTag("SkillDescription");

        }

        // Update is called once per frame
        void Update()
        {
            if (selected)
            {
                isUsed = false;

                for (int i = 0; i < selectedSkills.transform.childCount; i++)
                {
                    if (selectedSkills.transform.GetChild(i).tag == this.gameObject.name)
                    {
                        isUsed = true;
                        break;
                    }
                }

                if (!isUsed)
                {

                    if (profile.getKeyDown(PlayerAction.Skill1))
                    {
                        skillSlot = selectedSkills.transform.GetChild(0);
                        skillSlot.tag = this.gameObject.name;
                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(getSkill(this.gameObject.name), 0);
                    }
                    if (profile.getKeyDown(PlayerAction.Skill2))
                    {
                        skillSlot = selectedSkills.transform.GetChild(1);
                        skillSlot.tag = this.gameObject.name;
                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(getSkill(this.gameObject.name), 1);
                    }
                    if (profile.getKeyDown(PlayerAction.Skill3))
                    {
                        skillSlot = selectedSkills.transform.GetChild(2);
                        skillSlot.tag = this.gameObject.name;
                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(getSkill(this.gameObject.name), 2);
                    }
                    if (profile.getKeyDown(PlayerAction.Skill4))
                    {
                        skillSlot = selectedSkills.transform.GetChild(3);
                        skillSlot.tag = this.gameObject.name;
                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(getSkill(this.gameObject.name), 3);
                    }
                }
            }
        }


        public Skill getSkill(string name)
        {
            Skill skill = null;

            switch (name)
            {
                case "Fireball":
                    skill = pm.getSkill(SkillsID.Fireball);
                    break;
                case "Charge":
                    skill = pm.getSkill(SkillsID.Charge);
                    break;
                case "BladesDance":
                    skill = pm.getSkill(SkillsID.BladesDance);
                    break;
                case "IcyBlast":
                    skill = pm.getSkill(SkillsID.IcyBlast);
                    break;
            }

            return skill;
        }
    }
}
