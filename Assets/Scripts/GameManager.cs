using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject playerGO;
    Player playerSCR;

    public GameObject textLifeGO;

    public Text lifeText;

    [Header("Damage Screen")]
    public DamageScreen damageScreen;
    bool damageState;
    bool lastDamageState;

    [Header("LifeBar")]
    public LifeBar lifeBar;
    public float life;
    //bool firstAttack;
    //bool lastAttack;

    [Header("CheckPoints")]
    public CheckPoint currCheckPoint;

    private void Awake()
    {
        playerSCR = FindObjectOfType<Player>();
    }

    void Start()
    {
        Time.timeScale = 1;
        lastDamageState = damageState = false;
    }

    void Update()
    {
        //Condicion que detecta primer y último ataque recibido por el jugador
        if (lastDamageState != damageState)
        {
            //Deteccion primer ataque
            if (!lastDamageState)
            {
                VisualDamage(true);
            }
            else //Deteccion último ataque
            {
                VisualDamage(false);
            }
        }
        lastDamageState = damageState;
        damageState = false;
    }


    public void Lose(bool lose)
    {
        if (lose == true)
        {
            LoadCheckPoint();
        }

    }

    //Setea damageState siempre que está atacando. En el Update la misma variable se setea a false
    public void BeingAttacked()
    {
        damageState = true;
    }

    void VisualDamage(bool b)
    {
        if (damageScreen != null)
        {
            damageScreen.FadeInFadeOut(b);
        }
        else
        {
            Debug.LogWarning("Falta agregar la referencia al DamageScreen");
        }
    }

    void LoadCheckPoint()
    {
        if (currCheckPoint != null)
        {
            GoalsManager.instance.playerUseCheckPoint = true;
            currCheckPoint.LoadCheckPoint();
            playerSCR.life = 60;
        }
        else
        {
            ReloadScene();
        }

    }

    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
