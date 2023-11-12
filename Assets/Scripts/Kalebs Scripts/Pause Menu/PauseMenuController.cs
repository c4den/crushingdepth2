using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject howToPlayMenu;
    [SerializeField] GameObject inGameUI;

    [SerializeField] GameObject resumeButton, quitButton, howToPlayFirstOption;

    bool isPaused = false;
    bool inCooldown = false;


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
}
