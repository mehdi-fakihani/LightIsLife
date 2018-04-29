using UnityEngine;
using System.Collections;
using LIL.Inputs;

namespace LIL
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 5f;        // The speed with which the camera will be following.
        public float rotateSpeed = 3.0f;
        private float x = 0;
        private float y = 0;
        private float moveCamera;
        private bool freeCamera;

        Vector3 offset;                     // The initial offset from the target.
        Profile profile = new Profile(1, 0);

        void Start()
        {
            freeCamera = false;
            // Calculate the initial offset
            offset = transform.position - target.position;
        }

        void FixedUpdate()
        {
            moveCamera = 0.0f;

            if (profile.getKey(PlayerAction.CameraLeft)) moveCamera += 1.0f;
            if (profile.getKey(PlayerAction.CameraRight)) moveCamera -= 1.0f;
            x = moveCamera * rotateSpeed;

            if (freeCamera)
            {
                Debug.Log("freee");
            }
                y = Input.GetAxis("Vertical") * rotateSpeed;

            offset = Quaternion.AngleAxis(x, Vector3.up) * offset;

            //if(freeCamera)
                offset = Quaternion.AngleAxis(y, Vector3.right) * offset;

            // Create a postion the camera is aiming for based on the offset from the target.
            //Vector3 targetCamPos = target.position + offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            //transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }

        private void Update()
        {
            if (!freeCamera && Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("freee 2 ");
            } freeCamera = true;
            if (freeCamera && Input.GetKeyDown(KeyCode.Return)) freeCamera = false;
        }
    }

}
