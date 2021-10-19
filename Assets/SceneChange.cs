using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            Time.timeScale = 0f;
        }
    }

    private void Update()
    {
        GetReloadInput();
        GetReadyToStartInput();
        

        //return to main by pressing esc button
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().buildIndex != 0)
            {
                ReturnToMain();
            }
        }
    }

    private void GetReadyToStartInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                Time.timeScale = 1f;
            }
        }
    }

    private void GetReloadInput()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(SceneManager.GetActiveScene().buildIndex != 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("HowToPlay");
    }


    public void LoadHighScore()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
