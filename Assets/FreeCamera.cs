using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float sensitivityX = 2.0f;
    public float sensitivityY = 2.0f;
    public float rotationSpeed = 30.0f; // Rotation speed when floating

    private float rotationX = 0;
    private float rotationY = 0;
    private bool isCameraControllable = true; // Flag to control camera rotation
    private bool isFloating = false; // Flag to control floating behavior
    private Vector3 randomRotationAxis;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isCameraControllable)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            if (!isFloating)
            {
                rotationX -= mouseY * sensitivityY;
                rotationY += mouseX * sensitivityX;
            }
            else
            {
                // Rotate the camera continuously
                float randomX = Random.Range(-360f, 360f);
                float randomY = Random.Range(-1f, 1f);
                rotationX += randomX * rotationSpeed * Time.deltaTime;
                rotationY += randomY * rotationSpeed * Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Stop floating and resume normal camera control
                    isFloating = false;
                }
            }

            //rotationX = Mathf.Clamp(rotationX, -90, 90);
            rotationY %= 360;
        }

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.parent.localRotation = Quaternion.Euler(0, rotationY, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle camera control or initiate floating
            if (!isFloating)
            {
                isCameraControllable = !isCameraControllable;
            }
            else
            {
                // Start floating with random rotation axes
                randomRotationAxis = Random.onUnitSphere;
                isFloating = !isFloating;
            }
        }
    }
}






