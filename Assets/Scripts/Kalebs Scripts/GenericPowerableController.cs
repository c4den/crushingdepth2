using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericPowerableController : MonoBehaviour, PowerableInterface
{
    public UnityEvent FlipPowerEvent = new UnityEvent();

    bool isPowered = false;

    public bool FlipPower(bool isPlayerStaticCharged)
    {
        if (isPowered && !isPlayerStaticCharged)
        {
            TurnOff();
            return true;
        }

        if (!isPowered && isPlayerStaticCharged)
        {
            TurnOn(); 
            return false;
        }

        return isPlayerStaticCharged; // Nothing changes so return whatever the passed in charged bool is
    }

    void TurnOn()
    {
        print("Turning on");
        isPowered = true;
        SetMaterial(Color.green);
        FlipPowerEvent?.Invoke();
    }

    void TurnOff()
    {
        print("Turning off");
        isPowered = false;
        SetMaterial(Color.red);
        FlipPowerEvent?.Invoke();
    }

    void SetMaterial(Color color)
    {
        this.GetComponent<Renderer>().material.SetColor("_Color", color);
    }
}
