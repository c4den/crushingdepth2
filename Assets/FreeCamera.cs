using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float sensitivityX = 2.0f;
    public float sensitivityY = 2.0f;

    private float rotationX = 0;
    private float rotationY = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX -= mouseY * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        rotationY += mouseX * sensitivityX;
        rotationY %= 360;

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.parent.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
}






