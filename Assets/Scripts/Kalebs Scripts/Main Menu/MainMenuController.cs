using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int gameplaySceneBuildIndex = 1;
    public GameObject mainMenuPanel;
    public GameObject[] panels;

    public void OpenPanel(GameObject panel)
    {
        foreach (GameObject panelObj in panels)
        {
            if (GameObject.ReferenceEquals(panelObj, panel) != true)
            {
                panelObj.SetActive(false);
            }
            else
            {
                panelObj.SetActive(true);
            }
        }
    }

    public void ClosePanel(GameObject panelToClose)
    {
        panelToClose.SetActive(false);
        mainMenuPanel.SetActive(true);
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
