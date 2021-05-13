using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;

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

    public void ChangeMusic(AudioClip audio)
    {
        src.clip = audio;
        src.Play();
    }
}
