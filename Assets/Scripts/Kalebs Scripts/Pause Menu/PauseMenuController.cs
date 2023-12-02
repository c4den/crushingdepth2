using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject howToPlayMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject inGameUI;

    [SerializeField] GameObject resumeButton, quitButton, howToPlayFirstOption, settingsFirstOption;

    public Toggle invertToggle;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    Resolution[] resolutions;

    bool isPaused = false;
    bool inCooldown = false;

    private void Start()
    {
        // Resolution setup
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        int currentResolutionInd = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionInd = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionInd;
        resolutionDropdown.RefreshShownValue();

        try
        {
            if (MainMenuController.currentResolutionIndex > -1)
            {
                print("Resolution index check true");
                Resolution resolution = resolutions[MainMenuController.currentResolutionIndex];
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
                resolutionDropdown.value = MainMenuController.currentResolutionIndex;
                resolutionDropdown.RefreshShownValue();
            }
            volumeSlider.value = AudioListener.volume;
            invertToggle.isOn = FreeCameraController.invertUpDown;
        }
        catch { }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKey("joystick button 7"))
        {
            if (inCooldown) return;

            StartCoroutine(Cooldown());

            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;

        pauseMenu.SetActive(true);

        inGameUI.SetActive(false);

        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  // Unhides cursor

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;

        pauseMenu.SetActive(false);

        inGameUI.SetActive(true);

        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  // Hide the default system cursor
    }

    public void HowToPlay()
    {
        pauseMenu.SetActive(false);

        howToPlayMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(howToPlayFirstOption);
    }

    public void SettingsMenu()
    {
        pauseMenu.SetActive(false);

        settingsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstOption);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        pauseMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void QuitToMenu()
    {
        print("Quit to menu");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    private IEnumerator Cooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(1.0f);
        inCooldown = false;
    }

    public void SetResolution(int index)
    {
        print("Pause menu changing resolution");

        Resolution resolution = resolutions[index];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        MainMenuController.currentResolutionIndex = index;
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void InvertUpDown(bool value)
    {
        FreeCameraController.invertUpDown = value;
    }
}
