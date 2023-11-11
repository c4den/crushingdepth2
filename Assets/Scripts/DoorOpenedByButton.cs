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
            animator.Play("A_Door");


            isOpen = false;
        }
        else
        {
            animator.Play("A_Door");


            isOpen = true;
        }
    }
}
