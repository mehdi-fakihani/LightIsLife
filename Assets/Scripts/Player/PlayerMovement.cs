
using LIL.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LIL
{
    [RequireComponent(typeof(MovementManager))]
    public class PlayerMovement : MonoBehaviour
    {

        public float movementSpeed = 6f;
        Animator anim;
        private float moveHorizontal;
        private float moveVertical;
        Skill fireball;
        public ProfilsID input;
        public Profile profile;

        // Added by Sidney
        private MovementManager movementManager;
        Skill charge;
        Skill icyBlast;

        // Added by Julien
        private Vector3 lastMove;

        void Start()
        {
            fireball = GetComponent<SkillManager>().getSkill(SkillsID.Fireball);

            // Added by Sidney
            charge   = GetComponent<SkillManager>().getSkill(SkillsID.Charge);
            icyBlast = GetComponent<SkillManager>().getSkill(SkillsID.IcyBlast);

            profile = new Profile(input, 0);
            anim = GetComponent<Animator>();
            movementManager = GetComponent<MovementManager>();
            lastMove = Vector3.zero;
        }

        void Update()
        {
            ControllPlayer();
        }
        
        void ControllPlayer()
        {
            moveVertical = 0.0f;
            moveHorizontal = 0.0f;
         
            if (profile.getKeyDown(PlayerAction.Skill1)) fireball.tryCast();

            // Added by Sidney
            if (profile.getKeyDown(PlayerAction.Skill4)) charge.tryCast();
            if (profile.getKeyDown(PlayerAction.Skill3)) icyBlast.tryCast();

            // Added by Sidney
            if (movementManager.isImmobilized()) return;
            
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
        }

        void Animating(float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool("walk", walking);
        }
    }
}
