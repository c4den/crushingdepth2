using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenedByButton : MonoBehaviour
{
    public void OpenDoor()
    {
        this.transform.Rotate(0, -90, 0);
    }
}
