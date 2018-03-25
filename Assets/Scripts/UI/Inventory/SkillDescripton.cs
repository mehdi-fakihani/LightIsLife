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
        private Profile profile;
        private string removeSelectedSkill = "";
        private string close;
        private string deblock;
        private string howTo;
        private string[] skillKeys;
        private string[] browseKeys;
        private bool howToDisplayed = false;
        int playerNum=1;
        

        // Use this for initialization
        void Start()
        {
            playerNum = this.transform.parent.name[this.transform.parent.name.Length - 1];
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject player = players[0];

            foreach (GameObject _player in players)
            {
                if (_player.name[_player.name.Length - 1] == (char)playerNum)
                {
                    player = _player;
                    break;
                }
            }
            playerNum -= 48;
            pc = player.GetComponent<PlayerController>();
            profile = pc.getProfile();
            var model = Profile.Models[playerNum];
            close = model.keys[PlayerAction.Interaction].ToString();
            if (close.StartsWith("Keyboard"))
            {
                close = close.Substring(8);

                deblock = model.keys[PlayerAction.Submit].ToString().Substring(8);

                howTo = model.keys[PlayerAction.ChangeTorch].ToString();
                howTo = howTo.Substring(8);

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

                deblock = model.keys[PlayerAction.Submit].ToString().Substring(7);
                howTo = model.keys[PlayerAction.ChangeTorch].ToString();
                howTo = howTo.Substring(7);

                skillKeys = new string[] { model.keys[PlayerAction.Skill1].ToString().Substring(7),
                    model.keys[PlayerAction.Skill2].ToString().Substring(7),
                model.keys[PlayerAction.Skill3].ToString().Substring(7),
                    model.keys[PlayerAction.Skill4].ToString().Substring(7) };

                Debug.Log("left key : "+model.keys[PlayerAction.Left].ToString());
                browseKeys = new string[] { model.keys[PlayerAction.Left].ToString().Substring(7),
                    model.keys[PlayerAction.Up].ToString().Substring(7),
                model.keys[PlayerAction.Down].ToString().Substring(7),
                    model.keys[PlayerAction.Right].ToString().Substring(7) };

                removeSelectedSkill = model.keys[PlayerAction.Attack].ToString().Substring(7);
            }

            // Display the how to description
            setHowToDescription();


            // Set already selected skills sprites
            GeneralData.Skill[] selectedSkills = GeneralData.GetCurrentSkills(playerNum);

            // SKillsSelected (4th child of the inventory)
            Transform selectedSkillsPanel = this.transform.parent.GetChild(3);

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

        void Update()
        {
            if(howToDisplayed && (profile.getKeyDown(PlayerAction.Up) || profile.getKeyDown(PlayerAction.Down) ||
                profile.getKeyDown(PlayerAction.Left) || profile.getKeyDown(PlayerAction.Right)))
            {
                this.transform.parent.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Selectable>().Select();
                howToDisplayed = false;
            }
            else if(!howToDisplayed && profile.getKeyDown(PlayerAction.ChangeTorch))
            {
                setHowToDescription();
            }
        }

        public void setHowToDescription()
        {
            // Title :
            this.transform.GetChild(0).GetComponent<Text>().text = "How to :";

            // Class :
            this.transform.GetChild(1).GetComponent<Text>().text = "";

            // Description :
            this.transform.GetChild(2).GetComponent<Text>().text = "To browse skill : " + browseKeys[0] + "," + browseKeys[1] + "," +
                browseKeys[2] + "," + browseKeys[3] + "\n" +
            "To select a skill : " + skillKeys[0] + "," + skillKeys[1] + "," +
                skillKeys[2] + "," + skillKeys[3] + "\n" +
            "To remove a selected skill : " + removeSelectedSkill + "\n" +
            "TO deblock a skill : "+deblock + "\n" +
            "To check How To again : " + howTo + "\n" +
            "To close the interface : " + close + "\n";

            howToDisplayed = true;
        }

        public bool isHowToDisplayed()
        {
            return this.howToDisplayed;
        }

        public void disableHowTo()
        {
            this.howToDisplayed = false;
        }
    }
}
