using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public bool isDoorOpener; // A boolean to check if this block opens the door
    public DoorOpenedByButton door; // Reference to the DoorOpenedByButton script
    public AudioClip doorOpenSound; // Sound to play when the door opens
    public AudioClip buttonPressSound; // Sound to play when the button is pressed
    public AudioClip incorrectSequenceSound; // Sound to play when the sequence is incorrect
    private AudioSource audioSource;

    // Combination related variables
    public int blockID; // Unique ID for each block
    private static List<int> currentSequence = new List<int>();
    private static readonly List<int> correctSequence = new List<int> { 1, 2, 3 };

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnBlockClicked()
    {
        // Play the button press sound
        if (buttonPressSound != null)
            audioSource.PlayOneShot(buttonPressSound);

        if (isDoorOpener)
        {
            currentSequence.Add(blockID); // Add this block's ID to the sequence

            if (IsCorrectSequence())
            {
                door.DoorInteract(); // Open the door
                if (doorOpenSound != null)
                    audioSource.PlayOneShot(doorOpenSound);
                Debug.Log("Door Opened");
                currentSequence.Clear(); // Reset the sequence after opening the door
            }
            else if (currentSequence.Count >= correctSequence.Count)
            {
                // If the sequence is wrong and at least as long as the correct sequence
                if (incorrectSequenceSound != null)
                    audioSource.PlayOneShot(incorrectSequenceSound);

                Debug.Log("Incorrect Sequence");
                currentSequence.Clear(); // Reset the sequence if it's wrong
            }
        }
    }

    private bool IsCorrectSequence()
    {
        if (currentSequence.Count != correctSequence.Count)
            return false;

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (currentSequence[i] != correctSequence[i])
                return false;
        }

        return true;
    }
}
