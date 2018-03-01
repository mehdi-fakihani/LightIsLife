
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
<<<<<<< HEAD:Assets/Scripts/Player/PlayerMovement.cs
        Skill fireball;
        public ProfilsID input;
        public Profile profile;
        [SerializeField] private GameObject otherPlayertorch;
        private Light otherlight;
        [SerializeField] private GameObject Playertorch;
        [SerializeField] private Transform player;
        [SerializeField] private GameObject secondPlayer;
        private Light light;
        private CameraController cam;
        SceneManager scenemanager;
        // Added by Sidney
        private MovementManager movementManager;
        Skill charge;
        Skill icyBlast;
        Skill bladesDance;
        Skill attack;
        private bool multi;
        private bool multiplayer = false;
=======
        private Profile profile;
        
        private MovementManager movementManager;

        private Skill fireball;
        private Skill charge;
        private Skill icyBlast;
        private Skill bladesDance;
>>>>>>> a3a95e74ed99c9b6b1bbdf6bedc5318e54f78b38:Assets/Scripts/Player/PlayerController.cs

        // Added by Julien
        private Vector3 lastMove;

        void Start()
        {
<<<<<<< HEAD:Assets/Scripts/Player/PlayerMovement.cs
           // torch = torch.GetComponent<Light>();
            fireball = GetComponent<SkillManager>().getSkill(SkillsID.Fireball);
            
            light = Playertorch.GetComponent<Light>();
            cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
            // Added by Sidney
=======
            fireball    = GetComponent<SkillManager>().getSkill(SkillsID.Fireball);
>>>>>>> a3a95e74ed99c9b6b1bbdf6bedc5318e54f78b38:Assets/Scripts/Player/PlayerController.cs
            charge      = GetComponent<SkillManager>().getSkill(SkillsID.Charge);
            icyBlast    = GetComponent<SkillManager>().getSkill(SkillsID.IcyBlast);
            bladesDance = GetComponent<SkillManager>().getSkill(SkillsID.BladesDance);
            attack = GetComponent<SkillManager>().getSkill(SkillsID.HeroAttack);
            profile = new Profile(input, 0);
            animator = GetComponent<Animator>();
            movementManager = GetComponent<MovementManager>();
<<<<<<< HEAD:Assets/Scripts/Player/PlayerMovement.cs
            scenemanager = GetComponent<SceneManager>();
            lastMove = Vector3.zero;
            bool multi = SceneManager.getMulti();
            Debug.Log(multi);
            if (multi == false)
            {
                secondPlayer.SetActive(false);
                
            }
            if (multi == true)
            {
                otherlight = otherPlayertorch.GetComponent<Light>();
                multiplayer = true;
            }
=======
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
>>>>>>> a3a95e74ed99c9b6b1bbdf6bedc5318e54f78b38:Assets/Scripts/Player/PlayerController.cs
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
         
            // Modified by Sidney
            if (profile.getKeyDown(PlayerAction.Skill1)) bladesDance.tryCast();

            // Added by Sidney
            if (profile.getKeyDown(PlayerAction.Skill4)) charge.tryCast();
            if (profile.getKeyDown(PlayerAction.Skill3)) icyBlast.tryCast();
            if (profile.getKeyDown(PlayerAction.Skill2)) attack.tryCast();

            if (multiplayer == true)
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
<<<<<<< HEAD:Assets/Scripts/Player/PlayerMovement.cs
            if (profile.getKey(PlayerAction.Up))
            {
                moveVertical += 1.0f;
            }
            if (profile.getKey(PlayerAction.Down))
            {
                moveVertical -= 1.0f;
            }
            if (profile.getKey(PlayerAction.Left))
            {
                moveHorizontal -= 1.0f;
            }
            if (profile.getKey(PlayerAction.Right))
            {
                moveHorizontal += 1.0f;
            }
=======
            
            if (profile.getKey(PlayerAction.Up))    moveVertical += 1.0f;
            if (profile.getKey(PlayerAction.Down))  moveVertical -= 1.0f;
            if (profile.getKey(PlayerAction.Left))  moveHorizontal -= 1.0f;
            if (profile.getKey(PlayerAction.Right)) moveHorizontal += 1.0f;
>>>>>>> a3a95e74ed99c9b6b1bbdf6bedc5318e54f78b38:Assets/Scripts/Player/PlayerController.cs

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            movement.Normalize();

            if (movement != Vector3.zero)
            {
                // smooth rotation only if movement is not opposed to last movement
                if(lastMove + movement != Vector3.zero)
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
                cam.target = player;
            }
        }

        void Animating(float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            animator.SetBool("walk", walking);
        }

        bool CameraFollow()
        {
            if (light.intensity !=0 )
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
