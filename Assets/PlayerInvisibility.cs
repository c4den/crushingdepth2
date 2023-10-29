using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvisibility : MonoBehaviour
{
    public KeyCode invisibilityKey = KeyCode.Space; // Key to toggle invisibility
    private Renderer playerRenderer; // Reference to the player's renderer component
    private bool isPlayerInvisible = false; // Flag to track player's invisibility state

    bool inCooldown = false;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // Check if the invisibility key is pressed
        if (Input.GetKeyDown(invisibilityKey) || Input.GetKey("joystick button 1"))
        {
            if (inCooldown) return;

            StartCoroutine(Cooldown());

            // Toggle player's invisibility
            ToggleInvisibility();
        }
    }

    private void ToggleInvisibility()
    {
        if (isPlayerInvisible)
        {
            // Make the player visible again
            playerRenderer.enabled = true;
            Debug.Log("Player is now visible.");
        }
        else
        {
            // Make the player invisible
            playerRenderer.enabled = false;
            Debug.Log("Player is now invisible.");
        }

        // Toggle the invisibility state
        isPlayerInvisible = !isPlayerInvisible;
    }

    public bool IsPlayerInvisible()
    {
        return isPlayerInvisible;
    }

    private IEnumerator Cooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(1.0f);
        inCooldown = false;
    }
}
