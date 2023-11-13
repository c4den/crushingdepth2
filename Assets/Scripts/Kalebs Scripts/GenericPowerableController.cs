using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericPowerableController : MonoBehaviour, PowerableInterface
{
    public UnityEvent FlipPowerEvent = new UnityEvent();

    bool isPowered = false;

    AudioSource audioSource;
    public AudioClip powerOnClip;
    public AudioClip powerOffClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

        PowerSound(powerOnClip);
    }

    void TurnOff()
    {
        print("Turning off");
        isPowered = false;
        SetMaterial(Color.red);
        FlipPowerEvent?.Invoke();

        PowerSound(powerOffClip);
    }

    void SetMaterial(Color color)
    {
        this.GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    void PowerSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
