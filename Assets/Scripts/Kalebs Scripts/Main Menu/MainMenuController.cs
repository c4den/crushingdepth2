using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public int gameplaySceneBuildIndex = 1;
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    public GameObject optionsPanel;
    public GameObject howToPlayPanel;

    [SerializeField] GameObject mainFirstOption, creditsFirstOption, optionsFirstOption, howToPlayFirstOption;

    public Toggle invertToggle;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    Resolution[] resolutions;

    public static int currentResolutionIndex = -1;

    private void Start()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstOption);


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
        FreeCameraController.invertUpDown = invertToggle.isOn;

        try
        {
            if (currentResolutionIndex > -1)
            {
                print("Resolution index check true");
                Resolution resolution = resolutions[MainMenuController.currentResolutionIndex];
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
                resolutionDropdown.value = currentResolutionIndex;
                resolutionDropdown.RefreshShownValue();
            }
            volumeSlider.value = AudioListener.volume;
            invertToggle.isOn = FreeCameraController.invertUpDown;
        }
        catch { print("Try catch in start caught something"); }
    }

    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsFirstOption);
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstOption);
    }

    public void OpenHowToPlay()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(howToPlayFirstOption);
    }

    public void ClosePanel(GameObject panelToClose)
    {
        panelToClose.SetActive(false);
        mainMenuPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstOption);
    }

    public void PlayGame()
    {
        print("Play game");
        SceneManager.LoadScene(gameplaySceneBuildIndex);
    }

    public void QuitGame()
    {
        print("Quit game");
        Application.Quit();
    }

    public void SetResolution(int index)
    {
        print("Changing resolution");

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
