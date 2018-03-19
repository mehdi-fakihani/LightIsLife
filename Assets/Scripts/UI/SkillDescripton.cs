using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LIL.Inputs;
using UnityEngine.UI;

namespace LIL
{
    public class SkillDescripton : MonoBehaviour
    {

        private PlayerController pc;
        private string removeSelectedSkill = "";
        private string close;
        private string[] skillKeys;
        private string[] browseKeys;

        // Use this for initialization
        void Start()
        {

            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            var model = Profile.Models[pc.getInput()];
            close = model.keys[PlayerAction.Interaction].ToString();
            if (close.StartsWith("Keyboard"))
            {
                close = close.Substring(8);

                skillKeys = new string[] { model.keys[PlayerAction.Skill1].ToString().Substring(8),
                    model.keys[PlayerAction.Skill2].ToString().Substring(8),
                model.keys[PlayerAction.Skill3].ToString().Substring(8),
                    model.keys[PlayerAction.Skill4].ToString().Substring(8) };

                browseKeys = new string[] { model.keys[PlayerAction.Left].ToString().Substring(8),
                    model.keys[PlayerAction.Up].ToString().Substring(8),
                model.keys[PlayerAction.Down].ToString().Substring(8),
                    model.keys[PlayerAction.Right].ToString().Substring(8) };

                removeSelectedSkill = model.keys[PlayerAction.Attack].ToString().Substring(8);

            }
            if (close.StartsWith("Gamepad"))
            {
                close = close.Substring(7);

                skillKeys = new string[] { model.keys[PlayerAction.Skill1].ToString().Substring(7),
                    model.keys[PlayerAction.Skill2].ToString().Substring(7),
                model.keys[PlayerAction.Skill3].ToString().Substring(7),
                    model.keys[PlayerAction.Skill4].ToString().Substring(7) };

                browseKeys = new string[] { model.keys[PlayerAction.Left].ToString().Substring(7),
                    model.keys[PlayerAction.Up].ToString().Substring(7),
                model.keys[PlayerAction.Down].ToString().Substring(7),
                    model.keys[PlayerAction.Right].ToString().Substring(7) };

                removeSelectedSkill = model.keys[PlayerAction.Attack].ToString().Substring(7);
            }

            this.gameObject.transform.GetChild(0).GetComponent<Text>().text = "How to :";
            this.gameObject.transform.GetChild(2).GetComponent<Text>().text = "To browse skill : "+ browseKeys[0]+","+ browseKeys[1]+","+
                browseKeys[2]+","+ browseKeys[3]+"\n"+
            "To select a skill : " + skillKeys[0] + "," + skillKeys[1] + "," +
                skillKeys[2] + "," + skillKeys[3]+"\n"+
            "To remove a selected skill : " + removeSelectedSkill + "\n"+"To close the interface : " + close + "\n";


            // Set already selected skills sprites
            
            GeneralData.Skill[] selectedSkills = GeneralData.GetCurrentSkills();
            Transform selectedSkillsPanel = GameObject.FindGameObjectWithTag("SkillSelected").transform;
            for (int i = 0; i < 4; i++)
            {
                if (selectedSkills[i] != null)
                {
                    Transform skillSlot = selectedSkillsPanel.GetChild(i);
                    skillSlot.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(selectedSkills[i].spritePath);
                    if (!skillSlot.transform.GetChild(1).GetComponent<Image>().enabled)
                    skillSlot.transform.GetChild(1).GetComponent<Image>().enabled = true;
                    skillSlot.tag = selectedSkills[i].name;
                }
            }

        }
    }
}
