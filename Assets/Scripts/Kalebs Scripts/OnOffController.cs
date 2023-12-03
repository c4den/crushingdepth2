using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnOffController : MonoBehaviour
{
    private UILogController logController;
    [SerializeField] private Animator animator;
    public GameObject interactText;
    public GameObject onInd;
    public GameObject offInd;

    public UnityEvent OnTurnOn;
    public UnityEvent OnTurnOff;

    [TextArea] public string logText;

    bool isOn = false;

    
    private void Start()
    {
        logController = GameObject.FindGameObjectWithTag("LogController").GetComponent<UILogController>();
    }

    private void Update()
    {
        if (isOn)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKey("joystick button 0"))
            {
                logController.DisplayLog(logText);
            }
        }
    }

    public void FlipPower()
    {
        if (isOn) // if on
        {
            // turn off
            OnTurnOff?.Invoke();
            onInd.SetActive(false);
            interactText.SetActive(false);
            offInd.SetActive(true);
            isOn = false;

            animator.SetBool("Powered", false);
        }
        else // if off
        {
            // turn on
            OnTurnOn?.Invoke();
            onInd.SetActive(true);
            interactText.SetActive(true);
            offInd.SetActive(false);
            isOn = true;

            animator.SetBool("Powered", true);
        }
    }
}
