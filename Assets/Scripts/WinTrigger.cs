using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    GameObject player;
    GameManager gameManager;
    public int sceneToLoad;
    [SerializeField] GameObject goalsPanel;
    [SerializeField] Text goalsDescriptions;

    private void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1)
        {
            GoalsManager.instance.timeRunning = false;
            StartCoroutine("ShowGoals");
        }
    }

    private IEnumerator ShowGoals()
    {
        goalsPanel.SetActive(true);
        foreach (var item in GoalsManager.instance.GetWinGoals())
        {
            goalsDescriptions.text += item + "\n";
        }
        yield return new WaitForSeconds(8);
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
