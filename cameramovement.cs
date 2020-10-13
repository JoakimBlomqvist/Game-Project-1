using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramovement : MonoBehaviour
{   
    [Tooltip("What do you want the camera to follow")]
    public Transform Player;

    [Tooltip("Which camera you want to follow the player")]
    public Transform Camera;

    [Tooltip("The delaye of the camera when following the player")] 
    public float smoothnessDelaye = 0.150f;

    private Vector3 Offset;

    //Change these values in inspector for the desired offset from the player
    public float cameraPosX;
    public float cameraPosY;
    public float cameraPosZ;

    [Tooltip("Sensitivity of the camera rotation")] public float turnSpeed = 3f;
    
    private float mouseX;
    private float mouseY;

    void Start()
    {
        //Sets the offset of the camera from the player position.
        Offset = new Vector3(Player.position.x + cameraPosX, Player.position.y + cameraPosY, Player.position.z + cameraPosZ);
        transform.rotation = Quaternion.Euler(Camera.rotation.x, Camera.rotation.y, Camera.rotation.z);
    }

    void Update()
    {
        Camera.LookAt(Player.position);

        //Sets Mouse axis to mouseX and mouseY
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        //Rotate camera when pressing mouse button
        if (Input.GetMouseButton(1))
        {
            Offset = Quaternion.AngleAxis(mouseX * turnSpeed, Vector3.up) * Quaternion.AngleAxis(mouseY * turnSpeed, Vector3.right) * Offset;
        }
    }

    void FixedUpdate()
    {
        //Makes the camera follow the player with a delaye set by the smoothnessDelaye variable
        Vector3 desiredPosition = Player.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothnessDelaye);
        Camera.position = smoothedPosition;
    }
}
