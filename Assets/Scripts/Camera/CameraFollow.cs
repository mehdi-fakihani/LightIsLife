using UnityEngine;
using System.Collections;

namespace LIL
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 5f;        // The speed with which the camera will be following.
        public float rotateSpeed = 3.0f;
        private float x = 0;

        Vector3 offset;                     // The initial offset from the target.

        void Start()
        {
            // Calculate the initial offset.
            offset = transform.position - target.position;
        }

        void FixedUpdate()
        {
            x = rotateSpeed;

            offset = Quaternion.AngleAxis(x, Vector3.up) * offset;

            // Create a postion the camera is aiming for based on the offset from the target.
            Vector3 targetCamPos = target.position + offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }

}
