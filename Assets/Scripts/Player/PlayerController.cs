﻿
using LIL.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace LIL
{
    [RequireComponent(typeof(MovementManager))]
    public class PlayerController : MonoBehaviour
    {

        private ProfilsID input;
        [SerializeField] private float movementSpeed = 6f;
        [SerializeField] private AudioClip hurtSound;
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private GameObject secondPlayer;
        public bool multiplayer = false;
        public float moveCamera;

        private Animator animator;
        private AudioSource audioSource;
        private float moveHorizontal;
        private float moveVertical;

        // public ProfilsID input;
        private CameraController cam;
        private SpiritController spirit;
        private Profile profile;
        private bool interactable;
        private bool inventoryActive;

        private MovementManager movementManager;
        private GameObject GUI;
        Interaction interaction;
        Inventory inventory;

        private Skill fireball;
        private Skill charge;
        private Skill icyBlast;
        private Skill bladesDance;

        private Skill[] selectedSkills = new Skill[] { null, null, null, null };

        private Skill attack;
        private Skill reflect;
        private int playerNum = 1;


        // Added by Julien
        private Vector3 lastMove;

        void Start()
        { 
            GUI = GameObject.FindGameObjectWithTag("GameUI");
            interaction = GUI.GetComponent<Interaction>();
            inventory = GUI.GetComponent<Inventory>();
            interactable = false;
            inventoryActive = false;

            // get the last digit of the player name (1 / 2)
            playerNum = this.transform.name[this.transform.name.Length - 1];
            playerNum -= 48;

            // Set Input system
            if (playerNum == 1) input = GeneralData.inputPlayer1;
            else if (playerNum == 2) input = GeneralData.inputPlayer2;

            // Load the previous position
            float[] pos = GeneralData.getPlayerbyNum(playerNum).pos;
            this.transform.position = new Vector3(pos[0], pos[1], pos[2]);

            profile = new Profile(playerNum, 0);
            Light light = GetComponentInChildren<Light>();

            cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
            spirit = GameObject.FindGameObjectWithTag("Spirit").GetComponent<SpiritController>();


            // set skills form DB
            setSkills();


            attack = GetComponent<SkillManager>().getSkill(SkillsID.HeroAttack);
            reflect = GetComponent<SkillManager>().getSkill(SkillsID.Reflect);

            animator = GetComponent<Animator>();
            movementManager = GetComponent<MovementManager>();
            lastMove = Vector3.zero;


            if (GeneralData.multiplayer)
            {
                multiplayer = true;
                if(playerNum == 1)  secondPlayer.SetActive(true);
                else if(playerNum == 2)
                {
                    //secondPlayer = GameObject.Find(this.transform.name.Substring(0, transform.name.Length-1)+"1");
                }

            }
            

            audioSource = GetComponent<AudioSource>();
            audioSource.volume = PlayerPrefs.GetFloat("SFXVolume");

            lastMove = Vector3.zero;

            // Added by Sidney (set hurt and death reactions)
            var health = GetComponent<HealthManager>();
            health.setHurtCallback(() =>
            {
                animator.SetTrigger("hurt");
                if (!audioSource.isPlaying) audioSource.PlayOneShot(hurtSound);
            });
            health.setDeathCallback(() =>
            {
                // Play death animation
                animator.SetTrigger("death");
                // Play death sound
                audioSource.PlayOneShot(deathSound);
                // End the game
                Time.timeScale = 0;
            });
        }

        void Update()
        {
            ControllPlayer();
        }

        void ControllPlayer()
        {
            moveVertical = 0.0f;
            moveHorizontal = 0.0f;


            // Modified by Sidney

            if (profile.getKeyDown(PlayerAction.Skill1) && !inventoryActive && selectedSkills[0] != null) selectedSkills[0].tryCast();
            if (profile.getKeyDown(PlayerAction.Skill2) && !inventoryActive && selectedSkills[1] != null) selectedSkills[1].tryCast();
            if (profile.getKeyDown(PlayerAction.Skill3) && !inventoryActive && selectedSkills[2] != null) selectedSkills[2].tryCast();
            if (profile.getKeyDown(PlayerAction.Skill4) && !inventoryActive && selectedSkills[3] != null) selectedSkills[3].tryCast();


            //Debug.Log(profile);
            //Debug.Log(multiplayer);




            if (profile.getKeyDown(PlayerAction.Attack) && !inventoryActive) attack.tryCast();

            if (multiplayer)
            {
                //Debug.Log("test");
                if (profile.getKeyDown(PlayerAction.ChangeTorch) && !inventoryActive && !secondPlayer.GetComponent<PlayerController>().inventoryActive)
                {
                    if (CameraFollow())
                    {
                        SetMainPlayer(secondPlayer);
                    }
                    else
                    {
                        SetMainPlayer(this.gameObject);
                    }
                }
            }
            // Added by Sidney

            if (movementManager.isImmobilized()) return;

            if (profile.getKey(PlayerAction.Up) && !inventoryActive) moveVertical += 1.0f;
            if (profile.getKey(PlayerAction.Down) && !inventoryActive) moveVertical -= 1.0f;
            if (profile.getKey(PlayerAction.Left) && !inventoryActive) moveHorizontal -= 1.0f;
            if (profile.getKey(PlayerAction.Right) && !inventoryActive) moveHorizontal += 1.0f;


            if (profile.getKey(PlayerAction.CameraLeft) && !inventoryActive) moveCamera += 1.0f;
            if (profile.getKey(PlayerAction.CameraRight) && !inventoryActive) moveCamera -= 1.0f;


            if (profile.getKeyDown(PlayerAction.Interaction) && interactable)
            {
                if(!CameraFollow()) interaction.displayInteractionMsg("Use the spirit's power !");
                else
                {
                    if (!inventoryActive)
                    {
                        interaction.hideInteractionMsg();
                        animator.SetTrigger("inventoryTrigger");
                        animator.SetBool("inventory", true);
                        inventory.active(playerNum);
                        inventoryActive = true;
                    }
                    else
                    {
                        animator.SetBool("inventory", false);
                        inventory.disable(playerNum, new float[] { this.transform.position.x, this.transform.position.y, this.transform.position.z });
                        inventoryActive = false;
                        interaction.displayInteractionMsg("Game saved !");
                    }
                }
            }
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            movement.Normalize();

            if (movement != Vector3.zero)
            {
                // smooth rotation only if movement is not opposed to last movement
                if (lastMove + movement != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
                }
                else
                {
                    transform.rotation = Quaternion.LookRotation(movement);
                }
                this.lastMove = movement;
            }

            // Added by Sidney
            float modifier = movementManager.getSpeedRatio();
            transform.Translate(movement * movementSpeed * Time.deltaTime * modifier, Space.World);
            Animating(moveHorizontal, moveVertical);
        }

        void Animating(float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            animator.SetBool("walk", walking);
        }


        public ProfilsID getInput()
        {
            return this.input;
        }

        public Profile getProfile()
        {
            return this.profile;
        }

        public void setSkill(Skill skill, int index)
        {
            if (index < 4)
            {
                selectedSkills[index] = skill;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Campfire")
            {
                interaction.displayInteractionMsg(playerNum);
                interactable = true;
            }
        }

        void OnTriggerExit(Collider other)
        {

            if (other.gameObject.tag == "Campfire")
            {
                interaction.hideInteractionMsg();
                interactable = false;
            }
        }

        bool CameraFollow()
        {
            if (spirit.GetTarget().position == transform.position)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public SkillsID getSkillIDByName(string name)
        {
            return (SkillsID)Enum.Parse(typeof(SkillsID), name);
        }

        public Skill getSkillByName(string name)
        {
            Skill skill = GetComponent<SkillManager>().getSkill(getSkillIDByName(name));
            return skill;
        }

        public void setSkills()
        {
            GeneralData.Player player;

            if (this.gameObject.name[this.gameObject.name.Length - 1] == '2')   player = GeneralData.getPlayerbyNum(2);
            else player = GeneralData.getPlayerbyNum(1);

            for (int i = 0; i < 4; i++)
            {
                if (player.usedSkills[i] != null)
                {
                    selectedSkills[i] = getSkillByName(player.usedSkills[i].name);
                }
            }

            //selectedSkills[0] = GetComponent<SkillManager>().getSkill(SkillsID.IcyBlast);
        }

        private void SetMainPlayer(GameObject player)
        {
            cam.SetTarget(player.transform);
            spirit.SetTarget(player.transform);

        }

        public int getPlayerNum()
        {
            return playerNum;
        }

    }
}
