using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] PlayerInput input;

    public GameObject PauseMenuUI;
    bool isGamePaused = false;

    public GameObject firstButton, secondButton, thirdButton;

    void Update()
    {
        if (input.GetPauseGame())
        {
            Pause();
        }
        else if (!input.GetPauseGame())
        {
            Resume();
        }


    }


    public void Resume()
    {
        isGamePaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        input.UnpauseGame();
    }

    void Pause()
    {

        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        if (!isGamePaused)
        {
            //clear previously selcted objects
            EventSystem.current.SetSelectedGameObject(null);

            //set a new selected object
            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        isGamePaused = true;

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    void MenuScreen() //this is not currently in use because there is no menu screen.
    {
        //clear previously selcted objects
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected object
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool GetGamePaused()
    {
        return isGamePaused;
    }

}
