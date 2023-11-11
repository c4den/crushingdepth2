using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int gameplaySceneBuildIndex = 1;
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    public GameObject optionsPanel;

    [SerializeField] GameObject mainFirstOption, creditsFirstOption, optionsFirstOption;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstOption);
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
}
