using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvisibility : MonoBehaviour
{
    public float invisibilityDuration = 5.0f; //Duration of invisibility in seconds
    public KeyCode invisibilityKey = KeyCode.Space; //Key to toggle invisibility
    private Renderer playerRenderer; //Reference to the player's renderer component
    private bool isPlayerInvisible = false; //Flag to track player's invisibility state

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        //Check if the invisibility key is pressed
        if (Input.GetKeyDown(invisibilityKey))
        {
            //Toggle player's invisibility
            ToggleInvisibility();
        }
    }

    private void ToggleInvisibility()
    {
        if (isPlayerInvisible)
        {
            //Make the player visible again
            playerRenderer.enabled = true;
            Debug.Log("Player is now visible.");
        }
        else
        {
            //Make the player invisible
            playerRenderer.enabled = false;
            Debug.Log("Player is now invisible."); 
            //Set a timer to make the player visible after the specified duration
            Invoke("MakePlayerVisible", invisibilityDuration);
        }

        //Toggle the invisibility state
        isPlayerInvisible = !isPlayerInvisible;
    }

    private void MakePlayerVisible()
    {
        //Make the player visible again
        playerRenderer.enabled = true;
        Debug.Log("Player is now visible (invisibility timer expired).");
        //Update the invisibility state
        isPlayerInvisible = false;
    }

    public bool IsPlayerInvisible()
    {
        return isPlayerInvisible;
    }
}
