using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] BGMSound, SFXSound;

    public AudioMixer mainAudioMixer;

    public AudioSource BGMSource, SFXSource;


    public void Awake()
    {
        LoadSavedVolumes();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    //----------------------- LOAD VOLUMES ----------------------//
    public void LoadSavedVolumes()
    {
        mainAudioMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVolPref", -6f));
        mainAudioMixer.SetFloat("BGMVol", PlayerPrefs.GetFloat("BGMVolPref", -6f));
        mainAudioMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVolPref", -6f));
    }


    //----------------------- PLAY BGM N SFX ----------------------//
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        string musicName = GetMusicNameFromScene(scene.name);
        if (!string.IsNullOrEmpty(musicName))
        {
            PlayMusic(musicName);
        }
        
    }

    private string GetMusicNameFromScene(string sceneName)
    {
        // Match your scene names here
        switch (sceneName)
        {
            case "MainMenu": return "BGM MainMenu";

            case "GameplayScene": return "BGM Gameplay";



            default: return "BGM MainMenu"; //fallback just in case
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(BGMSound, x => x.Name == name);
        if (s == null)
            Debug.Log("Sound not found.");
        else
        {
            BGMSource.clip = s.clip;
            BGMSource.loop = true;
            BGMSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(SFXSound, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound not found.");
        }
        else
        {
            SFXSource.PlayOneShot(s.clip);
        }
    }

}
