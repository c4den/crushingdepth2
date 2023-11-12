using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvisibility : MonoBehaviour
{
    public KeyCode invisibilityKey = KeyCode.Space; // Key to toggle invisibility
    private bool isPlayerInvisible = false; // Flag to track player's invisibility state
    public GameObject playerSpotlight; // Reference to the player's spotlight

    bool inCooldown = false;

    private void Start()
    {
        playerSpotlight = transform.Find("SpotlightGameObjectName").gameObject;


        // Ensure the playerSpotlight reference is not null
        if (playerSpotlight == null)
        {
            Debug.LogError("PlayerInvisibility script requires a GameObject named 'SpotlightGameObjectName' as a child.");
        }
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
            Debug.Log("Player is now visible.");
            if (playerSpotlight != null)
            {
                playerSpotlight.SetActive(true);
            }
        }
        else
        {
            // Make the player invisible
            Debug.Log("Player is now invisible.");
            if (playerSpotlight != null)
            {
                playerSpotlight.SetActive(false);
            }
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
