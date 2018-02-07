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
        public ProfilsID input;

        void Start()
        {
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
             Profile keyboard = new Profile(input, 0);
             Debug.Log(keyboard);
            if (keyboard.getKey(PlayerAction.Up))
            {
                moveVertical = 1.0f;
            }
            if (keyboard.getKey(PlayerAction.Down))
            {
                moveVertical = -1.0f;
            }
            if (keyboard.getKey(PlayerAction.Left))
            {
                moveHorizontal = -1.0f;
            }
            if (keyboard.getKey(PlayerAction.Right))
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
            anim.SetBool("IsWalking", walking);
        }
    }
}
