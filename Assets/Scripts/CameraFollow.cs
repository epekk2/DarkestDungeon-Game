using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // Reference to the player's transform
    public float smoothSpeed = 0.125f;   // Adjust this for smoothness
    public Vector3 offset;     // Offset to maintain from the player

    void Update()
    {
        if (player != null)
        {
            // Calculate the desired position of the camera
            Vector3 desiredPosition = player.position + offset;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera position
            transform.position = smoothedPosition;
        }
    }
}
