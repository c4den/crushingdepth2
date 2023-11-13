using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey("joystick button 0"))
        {
            PlayGame();
        }
    }

    void PlayGame()
    {
        SceneManager.LoadScene(2);
    }
}
