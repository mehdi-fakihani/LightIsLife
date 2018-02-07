using LIL.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LIL
{
    public class PlayerMovement : MonoBehaviour
    {

        public float movementSpeed = 6f;
        Animator anim;
        private float moveHorizontal;
        private float moveVertical;
        Skill fireball;
        public ProfilsID input;
        private Profile profile;

        void Start()
        {
            fireball = GetComponent<SkillManager>().getSkill("Fireball");
            profile = new Profile(input, 0);
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            ControllPlayer();
        }



        void ControllPlayer()
        {
            moveVertical = 0.0f;
            moveHorizontal = 0.0f;
            /*float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical"); 

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            transform.rotation = Quaternion.LookRotation(movement);

            Debug.Log(moveHorizontal);
            transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

            Animating(moveHorizontal, moveVertical);*/

            if (profile.getKeyDown(PlayerAction.Skill1)) fireball.tryCast();


            if (profile.getKey(PlayerAction.Up))
            {
                moveVertical = 1.0f;
            }
            if (profile.getKey(PlayerAction.Down))
            {
                moveVertical = -1.0f;
            }
            if (profile.getKey(PlayerAction.Left))
            {
                moveHorizontal = -1.0f;
            }
            if (profile.getKey(PlayerAction.Right))
            {
                moveHorizontal = 1.0f;
            }
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            movement.Normalize();
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
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
