using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SubmersibleMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private Rigidbody rb;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform; 

        //Disables the rigidbody rotation and angular velocity
        rb.freezeRotation = true;
        rb.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        //Handles movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Calculate the movement vector based on the camera direction
        Vector3 moveDirection = cameraTransform.TransformDirection(new Vector3(horizontalInput, 0, verticalInput));
        moveDirection.Normalize();

        //Apply movement
        rb.velocity = moveDirection * moveSpeed;

        //Update the character rotation to match the camera rotation
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }
}