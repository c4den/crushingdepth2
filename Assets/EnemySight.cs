using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float viewRadius = 15;       // Radius of the enemy's view
    public float viewAngle = 90;        // Angle of the enemy's view
    public LayerMask playerMask;        // Layer mask to detect the player
    public LayerMask obstacleMask;      // Layer mask for obstacles
    public PlayerInvisibility playerInvisibility; // Reference to the PlayerInvisibility script
    public float maxVisibility = 100f;
    public float increaseRate = 5f;
    public float currentVisibility = 0f;
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
                    Debug.Log("Visibility Increased: " + currentVisibility);
                    currentVisibility += increaseRate * Time.deltaTime;
                    // Ensure visibility doesn't exceed the maximum
                    currentVisibility = Mathf.Min(maxVisibility, currentVisibility);
                    Debug.Log("Player Detected!"); // Output a debug message when the player is seen.
                    Gizmos.color = Color.red; // Change Gizmos color when player is seen.
                    return true;
                    // The player is within the FOV, you can perform actions here.
                }
            }
        }

        return false; // Return false when the player is not in the field of view
    }
}
