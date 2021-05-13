using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    LayoutElement[] lifeFrames;
    public float fillValue = 1;
    float lastFillValue;

    public Image lifeBarContainer;

    [Header("Colors")]
    public Color filledColor;
    public Color halfColor;
    public Color criticalColor;
    public Color damagingColor;

    private void Awake()
    {
        lifeFrames = GetComponentsInChildren<LayoutElement>();
        lastFillValue = fillValue;
        //childrenCount = lifeFrames.Length;
    }

    private void Update()
    {
        int frames = Calculation(fillValue);

        for (int i = 0; i < lifeFrames.Length; i++)
        {
            if(i < frames)
            {
                lifeFrames[i].gameObject.SetActive(true);
            }
            else
            {
                lifeFrames[i].gameObject.SetActive(false);
            }
        }

        if (lastFillValue > fillValue)
        {
            ChangeColorTo(damagingColor);
        }
        else
        {
            ChangeColorByFrames(frames);
        }

        lastFillValue = fillValue;
    }

    int Calculation(float value)
    {
        double divisionResult = (double)value / ((double)1 / (double)lifeFrames.Length);
        int result = Mathf.CeilToInt((float)divisionResult);
        return result;
    }

    void ChangeColorByFrames(int frames)
    {
        Color _currentColor;

        if(frames < lifeFrames.Length / 2 && frames > 2)
        {
            _currentColor = halfColor;
        }
        else if(frames <= 2)
        {
            _currentColor = criticalColor;
        }
        else
        {
            _currentColor = filledColor;

        }

        ColorLoop(_currentColor);
    }
    void ChangeColorTo(Color c)
    {
        ColorLoop(damagingColor);
    }

    void ColorLoop(Color _currentColor)
    {
        for (int i = 0; i < lifeFrames.Length; i++)
        {
            lifeFrames[i].GetComponent<Image>().color = _currentColor;
        }

        lifeBarContainer.color = _currentColor;
    }
}
