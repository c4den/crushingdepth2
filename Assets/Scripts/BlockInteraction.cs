using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public bool isDoorOpener; // A boolean to check if this block opens the door

    //public DoorOpenedByButton door;
    public void OnBlockClicked()
    {
        if (isDoorOpener)
        {
            //door.OpenDoor();
            Debug.Log("Door Open");
        }
    }
}
