using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public bool isDoorOpener; // A boolean to check if this block opens the door
    public DoorOpenedByButton door; // Reference to the DoorOpenedByButton script
    public AudioClip doorOpenSound;
    public AudioClip clickSound;
    public AudioClip errorSound;
    AudioSource audioSource;

    // Combination related variables
    public int blockID; // Unique ID for each block
    private static List<int> currentSequence = new List<int>();
    private static readonly List<int> correctSequence = new List<int> { 1, 2, 3 }; // Replace with your correct sequence

    private Material originalMaterial;
    public Material pressedMaterial; // Material to represent a pressed button
    public Material errorMaterial; // Material for incorrect sequence

    public BlockInteraction patternButton; // Reference to the patternButton

    private bool isClickable = true; // Flag to determine if the button is clickable

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalMaterial = GetComponent<Renderer>().material;
    }

    public void OnBlockClicked()
    {
        if (isClickable)
        {
            if (isDoorOpener)
            {
                currentSequence.Add(blockID); // Add this block's ID to the sequence

                // Play click sound
                if (clickSound != null) audioSource.PlayOneShot(clickSound);

                // Change the material of the clicked button to pressedMaterial
                GetComponent<Renderer>().material = pressedMaterial;

                StartCoroutine(ResetButtonColorAfterDelay()); // Reset button color after a short delay

                if (IsCorrectSequence())
                {
                    door.DoorInteract(); // Open the door
                    if (doorOpenSound != null) audioSource.PlayOneShot(doorOpenSound);
                    Debug.Log("Door Opened");
                    currentSequence.Clear(); // Reset the sequence after opening the door
                }
                else if (currentSequence.Count == correctSequence.Count)
                {
                    ShowErrorMaterial();
                    Debug.Log("Incorrect Sequence");
                    currentSequence.Clear(); // Reset the sequence if it's wrong
                }

                // Check if the patternButton is assigned and call OnPatternButtonPressed
                if (patternButton != null)
                {
                    patternButton.OnPatternButtonPressed();
                }
            }
        }
    }

    public void OnPatternButtonPressed()
    {
        StartCoroutine(LightUpCorrectSequence());
    }

    private IEnumerator LightUpCorrectSequence()
    {
        foreach (int buttonID in correctSequence)
        {
            GameObject buttonObject = GameObject.Find("Button" + buttonID); // Replace with your button naming convention

            if (buttonObject == null)
            {
                Debug.LogError("Button not found: Button" + buttonID);
                yield break; // Exit the coroutine if the button is not found
            }

            // Change the material of the button to pressedMaterial
            buttonObject.GetComponent<BlockInteraction>().LightUpButton();

            yield return new WaitForSeconds(1f); // Wait for a short duration before moving to the next button
        }
    }

    public void LightUpButton()
    {
        // Change the material of the button to pressedMaterial
        GetComponent<Renderer>().material = pressedMaterial;
        StartCoroutine(ResetButtonColorAfterDelay()); // Reset button color after a short delay
    }
    private void ShowErrorMaterial()
    {
        // Play error sound
        if (errorSound != null) audioSource.PlayOneShot(errorSound);

        // Change the material of the button to errorMaterial for a brief duration
        GetComponent<Renderer>().material = errorMaterial;
        StartCoroutine(ResetButtonColorAfterDelay()); // Reset button color after a short delay
    }
    private IEnumerator ResetButtonColorAfterDelay()
    {
        // Wait for a short duration before resetting button color
        yield return new WaitForSeconds(1f); // Adjust the duration as needed

        // Reset the material of the button to the original material
        GetComponent<Renderer>().material = originalMaterial;
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

    // Method to set the button as clickable or not
    public void SetClickable(bool value)
    {
        isClickable = value;
    }
}
