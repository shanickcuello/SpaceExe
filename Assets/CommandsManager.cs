using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandsManager : MonoBehaviour
{
    AV[] avs;

    void Start()
    {
        avs = FindObjectsOfType<AV>();
        Console.instance.RegisterCommand("killvirusenemy", "Elimina todos los virus enemigos", KillAllVirus);
        Console.instance.RegisterCommand("nextScene", "Te lleva al proximo nivel, de ser el último, vuelve al menu", NextScene);
    }

    private void NextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCount > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    private void KillAllVirus()
    {
        foreach (var item in avs)
        {
            if (item != null)
            {
                Destroy(item.gameObject);
            }
        }
    }

}
