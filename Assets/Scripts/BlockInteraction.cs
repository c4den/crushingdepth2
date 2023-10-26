using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public bool isDoorOpener; // A boolean to check if this block opens the door

    public void OnBlockClicked()
    {
        if (isDoorOpener)
        {
            Debug.Log("Door Open");
        }
    }
}
