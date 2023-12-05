using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemySight : MonoBehaviour
{
    public float viewRadius = 500;       // Radius of the enemy's view
    public float viewAngle = 180;        // Angle of the enemy's view
    public LayerMask playerMask;        // Layer mask to detect the player
    public LayerMask obstacleMask;      // Layer mask for obstacles
    public PlayerInvisibility playerInvisibility; // Reference to the PlayerInvisibility script
    public float maxVisibility = 100f;
    public float increaseRate = 5f;
    public float decreaseRate = 5f;
    public float currentVisibility = 0f;
    public Slider visibilitySlider;
    public float rotationSpeed = 30f; // Speed of the rotation
    private float targetRotation1 = 310f; // First target rotation angle (in degrees)
    private float targetRotation2 = 230f; // Second target rotation angle (in degrees)
    private bool rotateClockwise = true; // Indicates the current rotation direction
    void Start()
    {
        playerInvisibility = GameObject.FindWithTag("Player").GetComponent<PlayerInvisibility>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        // Draw the FOV area
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        // Draw the FOV cone
        float halfFOV = viewAngle / 2;
        Quaternion leftRot = Quaternion.Euler(0, -halfFOV, 0);
        Quaternion rightRot = Quaternion.Euler(0, halfFOV, 0);
        Vector3 leftDir = leftRot * transform.forward;
        Vector3 rightDir = rightRot * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + leftDir * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightDir * viewRadius);
    }

    void Update()
    {
        if (!playerInvisibility.IsPlayerInvisible()) // Check if the player is not invisible
        {
            EnviromentView();
        }
        else
        {
            // Decrease visibility when the player is invisible
            currentVisibility -= decreaseRate * Time.deltaTime;
            // Ensure visibility doesn't go below 0
            currentVisibility = Mathf.Max(0, currentVisibility);
        }
        if (visibilitySlider != null)
        {
            visibilitySlider.value = currentVisibility;
        }
        if (currentVisibility == 100) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("EndScreen");
        }
        // Rotate the enemy's FOV
        RotateFOV();

    }


    public bool EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    //Debug.Log("Visibility Increased: " + currentVisibility);
                    currentVisibility += increaseRate * Time.deltaTime;
                    // Ensure visibility doesn't exceed the maximum
                    currentVisibility = Mathf.Min(maxVisibility, currentVisibility);
                    //Debug.Log("Player Detected!"); // Output a debug message when the player is seen.
                    Gizmos.color = Color.red; // Change Gizmos color when player is seen.
                    return true;
                    // The player is within the FOV, you can perform actions here.
                }
            }
        }

        return false; // Return false when the player is not in the field of view
    }

    private void RotateFOV()
    {
        // Calculate the rotation step based on the rotation speed and frame time
        float rotationStep = rotationSpeed * Time.deltaTime;

        // Determine the rotation direction
        int direction = rotateClockwise ? 1 : -1;

        // Choose the target rotation based on the current rotation direction
        float targetRotation = rotateClockwise ? targetRotation1 : targetRotation2;

        // Rotate the FOV towards the target rotation
        transform.Rotate(Vector3.up, rotationStep * direction);

        // Check if the target rotation is reached
        if (Mathf.Abs(transform.eulerAngles.y - targetRotation) < rotationStep)
        {
            // Change rotation direction
            rotateClockwise = !rotateClockwise;
        }
    }
}

