
using LIL.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LIL
{
    [RequireComponent(typeof(MovementManager))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ProfilsID input;
        [SerializeField] private float movementSpeed = 6f;
        [SerializeField] private AudioClip hurtSound;
        [SerializeField] private AudioClip deathSound;

        private Animator animator;
        private AudioSource audioSource;
        private float moveHorizontal;
        private float moveVertical;
        // public ProfilsID input;
        private Light otherlight;
        [SerializeField] private GameObject secondPlayer;
        private Light light;
        private CameraController cam;
        private bool multiplayer = false;
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

        private Skill[] selectedSkills;

        private Skill attack;
        private Skill reflect;


        // Added by Julien
        private Vector3 lastMove;

        void Start()
        {


            GUI = GameObject.FindGameObjectWithTag("GameUI");
            interaction = GUI.GetComponent<Interaction>();
            inventory = GUI.GetComponent<Inventory>();
            interactable = false;
            inventoryActive = false;


            Debug.Log("coucou");
            profile = new Profile(input, 0);
            Debug.Log(profile);
            light = GetComponentInChildren<Light>();
            cam = GameObject.Find("Main Camera").GetComponent<CameraController>();

            fireball = GetComponent<SkillManager>().getSkill(SkillsID.Fireball);
            charge = GetComponent<SkillManager>().getSkill(SkillsID.Charge);
            icyBlast = GetComponent<SkillManager>().getSkill(SkillsID.IcyBlast);
            bladesDance = GetComponent<SkillManager>().getSkill(SkillsID.BladesDance);


            selectedSkills = new Skill[] { bladesDance, fireball, icyBlast, charge }; // Just temporarily

            profile = new Profile(input, 0);

            attack = GetComponent<SkillManager>().getSkill(SkillsID.HeroAttack);
            reflect = GetComponent<SkillManager>().getSkill(SkillsID.Reflect);

            animator = GetComponent<Animator>();
            movementManager = GetComponent<MovementManager>();
            lastMove = Vector3.zero;
            if (SceneManager.getMulti())
            {
                otherlight = secondPlayer.GetComponentInChildren<Light>();
                multiplayer = true;
                secondPlayer.SetActive(true);
            }
            audioSource = GetComponent<AudioSource>();
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
            //Debug.Log(multiplayer);
        }

        void ControllPlayer()
        {
            moveVertical = 0.0f;
            moveHorizontal = 0.0f;

            //if (profile.getKeyDown(PlayerAction.Skill2)) reflect.tryCast();

            // Modified by Sidney

            if (profile.getKeyDown(PlayerAction.Skill1) && !inventoryActive) selectedSkills[0].tryCast();

            if (profile.getKeyDown(PlayerAction.Skill2) && !inventoryActive) selectedSkills[1].tryCast();
            // Added by Sidney
            if (profile.getKeyDown(PlayerAction.Skill4) && !inventoryActive) selectedSkills[3].tryCast();
            if (profile.getKeyDown(PlayerAction.Skill3) && !inventoryActive) selectedSkills[2].tryCast();

            //Debug.Log(profile);
            //Debug.Log(multiplayer);



            if (profile.getKeyDown(PlayerAction.Attack)) attack.tryCast();


            if (multiplayer)
            {
                //Debug.Log("test");
                if (profile.getKeyDown(PlayerAction.ChangeTorch) && light.intensity != 0)
                {
                    otherlight.intensity = light.intensity;
                    light.intensity = 0;
                }
            }
            // Added by Sidney

            if (movementManager.isImmobilized()) return;

            if (profile.getKey(PlayerAction.Up) && !inventoryActive) moveVertical += 1.0f;
            if (profile.getKey(PlayerAction.Down) && !inventoryActive) moveVertical -= 1.0f;
            if (profile.getKey(PlayerAction.Left) && !inventoryActive) moveHorizontal -= 1.0f;
            if (profile.getKey(PlayerAction.Right) && !inventoryActive) moveHorizontal += 1.0f;


            if (profile.getKeyDown(PlayerAction.Interaction) && interactable)
            {
                if (!inventoryActive)
                {
                    interaction.hideInteractionMsg();
                    inventory.active();
                    inventoryActive = true;
                }
                else
                {
                    interaction.displayInteractionMsg(this.input);
                    inventory.disable();
                    inventoryActive = false;
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

            if (CameraFollow())
            {
                cam.target = this.transform;
            }
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
                interaction.displayInteractionMsg(this.input);
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
            if (light.intensity != 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }


    }
}
