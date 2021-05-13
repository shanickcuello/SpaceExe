using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer instance;
    public SoundManager soundManager;
    AudioSource src;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        src = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio)
    {
        src.clip = audio;
        src.Play();
    }

    public void PlaySound(string audioName)
    {
        src.clip = soundManager.GetSound(audioName);
        src.Play();
    }

    public AudioClip GetSound(string name)
    {
        return soundManager.GetSound(name);
    }
}
