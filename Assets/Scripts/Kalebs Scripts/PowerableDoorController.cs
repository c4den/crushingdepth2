using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerableDoorController : MonoBehaviour
{
    [SerializeField] GameObject door;
    bool isPowered = false;


    public void FlipPower()
    {
        if (isPowered) // If door is powered, turn off and return static charge to player
        {
            CloseDoor();
            isPowered = false;
        }
        else // If door is not powered, turn on and take static charge from player
        {
            OpenDoor();
            isPowered = true;
        }
    }

    void OpenDoor()
    {
        door.transform.Rotate(0, -90, 0);
    }

    void CloseDoor()
    {
        door.transform.Rotate(0, 90, 0);
    }
}
