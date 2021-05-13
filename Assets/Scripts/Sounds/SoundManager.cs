using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Dictionary<string, AudioClip> soundList = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> musicList = new Dictionary<string, AudioClip>();

    public List<AudioMixerSnapshot> snapshots = new List<AudioMixerSnapshot>();
    public AudioMixer mixer;

    AudioListener listener;

    private AudioSource src;

    void Awake()
    {
        soundList = LoadSoundsFromStorage("Sounds");
        musicList = LoadSoundsFromStorage("Music");

        src = GetComponent<AudioSource>();
        listener = Camera.main.GetComponent<AudioListener>();

        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0.75f));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.75f));
    }

    public void SetAudioSnapshot(int select)
    {
        if (snapshots.Count > 0)
            snapshots[select].TransitionTo(0f);
        else
            Debug.Log("No snapshots loaded");
    }

    Dictionary<string, AudioClip> LoadSoundsFromStorage(string pathInResources)
    {
        AudioClip[] tempAudios = Resources.LoadAll<AudioClip>(pathInResources);
        Dictionary<string, AudioClip> tempDictionary = new Dictionary<string, AudioClip>();

        foreach (var item in tempAudios)
        {
            tempDictionary.Add(item.name, item);
        }

        return tempDictionary;
    }

    public AudioClip GetSound(string sound)
    {
        string[] soundSelection = sound.Split('_');


        if (soundSelection[0] == "m")
            return musicList[sound];
        else
            return soundList[sound];
    }

    public void ClickSound(string name)
    {
        src.PlayOneShot(GetSound(name));
    }

    public void SetSFXVolume(float val)
    {
        mixer.SetFloat("sfxVol", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("SFXVolume", val);
    }
    public void SetMusicVolume(float val)
    {
        mixer.SetFloat("musicVol", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("MusicVolume", val);
    }
}
