using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenedByButton : MonoBehaviour
{
    Animator animator;
    public AudioClip doorOpenSound;
    AudioSource audioSource;
    bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void DoorInteract()
    {
        if (isOpen)
        {
            // Close it
            animator.SetBool("Open", false);

            PlaySound();

            isOpen = false;
        }
        else
        {
            // Open it
            animator.SetBool("Open", true);

            PlaySound();

            isOpen = true;
        }
    }

    void PlaySound()
    {
        audioSource.PlayOneShot(doorOpenSound);
    }
}
