using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject inGameUI;

    bool isPaused = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKey("joystick button 7"))
        {
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

    public void QuitToMenu()
    {
        print("Quit to menu");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
