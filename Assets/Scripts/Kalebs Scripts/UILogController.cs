using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UILogController : MonoBehaviour
{
    public GameObject logMenu;
    public GameObject resumeButton;
    public GameObject inGameUI;

    [NonSerialized] public string logText;

    public TextMeshProUGUI logTMPRef;

    public void DisplayLog(string logText)
    {
        logMenu.SetActive(true);

        logTMPRef.text = logText;
        Pause();
    }

    public void CloseLog()
    {
        logMenu.SetActive(false);
        Unpause();
    }


    public void Pause()
    {
        Time.timeScale = 0.0f;

        inGameUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  // Unhides cursor

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;

        inGameUI.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  // Hide the default system cursor
    }
}
