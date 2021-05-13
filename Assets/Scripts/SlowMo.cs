using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour
{
    public float slowDownFactor, slowDownLenght, currentTime, timeToGetHability;
    public bool activeSlowMo;
    [SerializeField] GameObject rewindUi;

    private void Awake()
    {
        rewindUi.SetActive(false);
        slowDownFactor = 0.1f;
        slowDownLenght = 8;
        currentTime = 0;
    }

    void Update()
    {
        CheckKeys();
        TimeTravel();
        CheckTime();
        ActiveSloMoFromUi();
    }

    public void TimeTravel()
    {
        Time.timeScale += (1f / slowDownLenght) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void ActiveSloMoFromUi()
    {
        if (activeSlowMo && currentTime <= 0)
        {
            rewindUi.SetActive(true);
        }
        else
        {
            rewindUi.SetActive(false);
        }
    }

    public void RewindFromUi()
    {
        if (currentTime <= 0)
        {
            Rewind();
            currentTime = 5;
        }
    }

    public void CheckKeys()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && currentTime <= 0)
        {
            Rewind();
            currentTime = 5;
        }
    }

    public void Rewind()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void CheckTime()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;

        }
    }

}
