using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public bool isDoorOpener; // A boolean to check if this block opens the door
    public DoorOpenedByButton door; // Reference to the DoorOpenedByButton script

    public AudioClip doorOpenSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnBlockClicked()
    {
        if (isDoorOpener)
        {
            door.DoorInteract(); // Call the DoorInteract method
            if (doorOpenSound != null) audioSource.PlayOneShot(doorOpenSound);
            Debug.Log("Door Interaction Triggered");
        }
    }
}
