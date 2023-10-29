using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenedByButton : MonoBehaviour
{
    bool isOpen = false;
    public void OpenDoor()
    {
        if (isOpen) return;
        this.transform.Rotate(0, -90, 0);
        isOpen = true;
    }
}
