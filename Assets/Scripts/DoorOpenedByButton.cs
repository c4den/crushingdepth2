using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenedByButton : MonoBehaviour
{
    Animator animator;
    bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DoorInteract()
    {
        if (isOpen)
        {
            // Close it
            animator.SetBool("Open", false);


            isOpen = false;
        }
        else
        {
            // Open it
            animator.SetBool("Open", true);


            isOpen = true;
        }
    }
}
