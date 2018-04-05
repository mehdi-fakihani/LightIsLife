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
        private bool available;
        private PlayerController pc;
        private SkillManager pm;
        private Profile profile;
        private Transform inventory;
        private GameObject skillTitle;
        private GameObject skillDescription;
        private GameObject skillClass;
        Transform skillSlot;
        private int playerNum = 1;
        private GameObject Player;

        public void OnDeselect(BaseEventData eventData)
        {
             selected = false;
        }

        public void OnSelect(BaseEventData eventData)
        {
            selected = true;
            skillTitle.GetComponent<Text>().text = this.gameObject.name + " :" ;
            skillDescription.GetComponent<Text>().text = GeneralData.GetSkillByName(this.gameObject.name, playerNum).description;
            skillClass.GetComponent<Text>().text = GeneralData.GetSkillByName(this.gameObject.name, playerNum)._class.name;
            byte[] color = GeneralData.GetSkillByName(this.gameObject.name, playerNum)._class.color;
            skillClass.GetComponent<Text>().color = new Color32(color[0], color[1], color[2], color[3]);

        }

        // Use this for initialization
        void Awake()
        {
            available = true;
            inventory = this.transform.parent.parent.parent;                            // Get the Inventory
            selectedSkills = inventory.GetChild(3).gameObject;                          // Get the SkillsSelected GameObject
            skillTitle = inventory.GetChild(2).GetChild(0).gameObject;                  // Get the SkillTitle GameObject
            skillClass = inventory.GetChild(2).GetChild(1).gameObject;                  // Get the SkillClass GameObject
            skillDescription = inventory.GetChild(2).GetChild(2).gameObject;            // Get the SkillDescription GameObject

            playerNum = inventory.name[inventory.name.Length - 1]-48;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject player = players[0];

            foreach (GameObject _player in players)
            {
               if (_player.name[_player.name.Length - 1] == (char)(playerNum+48))
               {
                    player = _player;
                    break;
               }
            }


            pm = player.GetComponent<SkillManager>();
            pc = player.GetComponent<PlayerController>();
            profile = pc.getProfile();

            

            // Load the skill sprite
            string spritePath = GeneralData.GetSkillByName(this.gameObject.name, playerNum).spritePath;
            this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath);
            this.gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;

            if (!GeneralData.getPlayerbyNum(playerNum).skills[GeneralData.GetSkillIndexByName(this.gameObject.name, playerNum)].deblocked)
            {
                if (!GeneralData.isAvailable(this.gameObject.name, playerNum))
                {
                    available = false;
                    this.gameObject.GetComponent<Selectable>().enabled = false;
                    this.gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 50);
                }
                else
                {
                    this.gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                    this.gameObject.transform.GetChild(2).GetComponentInChildren<Text>().text = "" + GeneralData.GetSkillByName(this.gameObject.name, playerNum).CapPointsToUnlock;
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

                if (!isUsed && GeneralData.GetSkillByName(this.gameObject.name, playerNum).deblocked)
                {

                    if (profile.getKeyDown(PlayerAction.Skill1))
                    {
                        
                        skillSlot = selectedSkills.transform.GetChild(0);

                        if (skillSlot.tag != "Untagged") GeneralData.ChangeUsedSkill(this.gameObject.name, 0, playerNum);
                        else
                        {
                            GeneralData.GetSkillByName(this.gameObject.name, playerNum).isUsed = true;
                            GeneralData.getPlayerbyNum(playerNum).usedSkills[0] = GeneralData.GetSkillByName(this.gameObject.name, playerNum);
                        }

                        skillSlot.tag = this.gameObject.name;

                        if (!skillSlot.GetChild(1).GetComponent<Image>().enabled)
                            skillSlot.GetChild(1).GetComponent<Image>().enabled = true;

                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(pc.getSkillByName(this.gameObject.name), 0);

                        

                    }
                    if (profile.getKeyDown(PlayerAction.Skill2))
                    {
                        skillSlot = selectedSkills.transform.GetChild(1);

                        if (skillSlot.tag != "Untagged") GeneralData.ChangeUsedSkill(this.gameObject.name, 1, playerNum);
                        else
                        {
                            GeneralData.GetSkillByName(this.gameObject.name, playerNum).isUsed = true;
                            GeneralData.getPlayerbyNum(playerNum).usedSkills[1] = GeneralData.GetSkillByName(this.gameObject.name, playerNum);
                        }

                        skillSlot.tag = this.gameObject.name;

                        if (!skillSlot.GetChild(1).GetComponent<Image>().enabled)
                            skillSlot.GetChild(1).GetComponent<Image>().enabled = true;

                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(pc.getSkillByName(this.gameObject.name), 1);

                    }
                    if (profile.getKeyDown(PlayerAction.Skill3))
                    {
                        skillSlot = selectedSkills.transform.GetChild(2);

                        if (skillSlot.tag != "Untagged") GeneralData.ChangeUsedSkill(this.gameObject.name, 2, playerNum);
                        else
                        {
                            GeneralData.GetSkillByName(this.gameObject.name, playerNum).isUsed = true;
                            GeneralData.getPlayerbyNum(playerNum).usedSkills[2] = GeneralData.GetSkillByName(this.gameObject.name, playerNum);
                        }

                        skillSlot.tag = this.gameObject.name;

                        if (!skillSlot.GetChild(1).GetComponent<Image>().enabled)
                            skillSlot.GetChild(1).GetComponent<Image>().enabled = true;

                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(pc.getSkillByName(this.gameObject.name), 2);


                    }
                    if (profile.getKeyDown(PlayerAction.Skill4))
                    {
                        skillSlot = selectedSkills.transform.GetChild(3);

                        if (skillSlot.tag != "Untagged") GeneralData.ChangeUsedSkill(this.gameObject.name, 3, playerNum);
                        else
                        {
                            GeneralData.GetSkillByName(this.gameObject.name, playerNum).isUsed = true;
                            GeneralData.getPlayerbyNum(playerNum).usedSkills[3] = GeneralData.GetSkillByName(this.gameObject.name, playerNum);
                        }

                        skillSlot.tag = this.gameObject.name;

                        if (!skillSlot.GetChild(1).GetComponent<Image>().enabled)
                            skillSlot.GetChild(1).GetComponent<Image>().enabled = true;

                        skillSlot.GetChild(1).GetComponent<Image>().sprite = this.transform.GetChild(1).GetComponent<Image>().sprite;
                        pc.setSkill(pc.getSkillByName(this.gameObject.name), 3);

                    }
                }
                else if (isUsed && profile.getKeyDown(PlayerAction.Attack))
                {
                    skillSlot = selectedSkills.transform.GetChild(index);
                    skillSlot.tag = "Untagged";
                    skillSlot.GetChild(1).GetComponent<Image>().enabled = false;
                    skillSlot.GetChild(1).GetComponent<Image>().sprite = null;
                    pc.setSkill(null, index);
                    GeneralData.GetSkillByName(this.gameObject.name, playerNum).isUsed = false;
                    GeneralData.getPlayerbyNum(playerNum).usedSkills[index] = null;

                    GeneralData.GetSkillByName(this.gameObject.name, playerNum).isUsed = false;
                }
                else if(!GeneralData.GetSkillByName(this.gameObject.name, playerNum).deblocked && profile.getKeyDown(PlayerAction.Submit))
                {
                    if(GeneralData.GetCapacityPoints(playerNum) >= GeneralData.GetSkillByName(this.gameObject.name, playerNum).CapPointsToUnlock)
                    {
                        GeneralData.deblockSkill(playerNum, this.gameObject.name);
                        this.gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                        this.gameObject.transform.GetChild(2).GetComponentInChildren<Text>().text = "";
                        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    }
                }
            }
            if (!available && GeneralData.isAvailable(this.gameObject.name, playerNum))
            {
                available = true;
                this.gameObject.GetComponent<Selectable>().enabled = true;
                this.gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                this.gameObject.transform.GetChild(2).GetComponentInChildren<Text>().text = "" + GeneralData.GetSkillByName(this.gameObject.name, playerNum).CapPointsToUnlock;
                this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }
}
