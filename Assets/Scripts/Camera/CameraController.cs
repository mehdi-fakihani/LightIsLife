using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.
    [SerializeField] private float zoomMin = 10.0f;
    [SerializeField] private float zoomMax = 20.0f;
    [SerializeField] private float angle = 60.0f;       // in-place rotation of the camera
    [SerializeField] private float fixedAngle = -20.0f; // angle between the camera and the player
    
    private float zoom = 15.0f;                 // distance between the camera and the player
    private Vector3 offset;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        // Set the angle from which look at the target
        transform.localEulerAngles = new Vector3(angle, 0, 0);
        
        SetCameraOffset();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Mouse ScrollWheel");
        if(input != 0)
        {
            zoom -= input * 3;
            zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
            SetCameraOffset();
        }

        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    void SetCameraOffset()
    {
        offset = new Vector3(0, zoom * Mathf.Cos(Mathf.Deg2Rad * fixedAngle),
                                zoom * Mathf.Sin(Mathf.Deg2Rad * fixedAngle));
    }

}
