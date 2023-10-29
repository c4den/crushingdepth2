using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SubmersibleMovement : MonoBehaviour
{
    public float moveSpeed = 20.0f;
    public float rotateSpeed = 45.0f; // Rotation speed when not controlled by the player
    
    private Rigidbody rb;
    private Transform cameraTransform;
    private bool areWASDKeysEnabled = true;
    private bool isFloating = false; // Flag to control floating behavior
    private Vector3 randomDirection;

    bool inCooldown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        rb.freezeRotation = true;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKey("joystick button 7"))
        {
            print("Quit game");
            Application.Quit();
        }

        // Check if the spacebar is pressed to toggle floating behavior
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey("joystick button 1"))
        {
            if (inCooldown) return;

            StartCoroutine(Cooldown());

            isFloating = !isFloating;
            if (isFloating)
            {
                // Set a random direction for floating
                randomDirection = Random.insideUnitSphere;
                randomDirection.y = 0;
                randomDirection.Normalize();
            }
        }
    }

    void FixedUpdate()
    {
        if (isFloating)
        {
            // Apply random direction and rotation for floating
            rb.velocity = randomDirection * moveSpeed / 10;
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        else if (areWASDKeysEnabled)
        {
            // Handle movement when 'WASD' keys are enabled
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = cameraTransform.TransformDirection(new Vector3(horizontalInput, 0, verticalInput));
            moveDirection.Normalize();

            rb.velocity = moveDirection * moveSpeed;
            transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        }
    }

    private IEnumerator Cooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(1.0f);
        inCooldown = false;
    }
}