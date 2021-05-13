using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenUI : MonoBehaviour
{
    public GameManager gameManager;
    public List<GameObject> screens;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale > 0)
            {
                Pause();
            }
            else if (Time.timeScale < 1)
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;

        screens[0].SetActive(false);
        screens[1].SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;

        screens[1].SetActive(false);
        screens[0].SetActive(true);
    }

    public void Restart()
    {
        if(gameManager != null)
        {
            gameManager.Lose(true);
        }
        else
        {
            Debug.LogWarning("Te faltó el Game Manager pa'");
        }
    }
}
