using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayLogController : MonoBehaviour
{
    [TextArea] public string logText;

    public GameObject logTextObj;
    public TextMeshProUGUI logTMPRef;

    public bool logEnabled = false;

    public void DisplayLog()
    {
        logEnabled = true;

        logTextObj.SetActive(true);
        logTMPRef.text = logText;
    }

    public void CloseLog()
    {
        logEnabled = false;

        logTextObj.SetActive(false);
    }
}
