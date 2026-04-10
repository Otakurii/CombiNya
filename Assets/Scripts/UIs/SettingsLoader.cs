using UnityEngine;
using UnityEngine.Audio;

public class SettingsLoader : MonoBehaviour
{
    public AudioMixer mainAudioMixer;

    void Start()
    {
        LoadAndApplySettings();

        //Force mixer refresh
        AudioListener.pause = true;
        AudioListener.pause = false;
    }

    void LoadAndApplySettings()
    {
        //Debug.Log("Loading settings from settingsLoader rn");

        // ---------- AUDIO ----------
        float masterDb = PlayerPrefs.GetFloat("MasterVolPref", -6f);
        float bgmDb = PlayerPrefs.GetFloat("BGMVolPref", -6f);
        float sfxDb = PlayerPrefs.GetFloat("SFXVolPref", -6f);

        mainAudioMixer.SetFloat("MasterVol", masterDb);
        mainAudioMixer.SetFloat("BGMVol", bgmDb);
        mainAudioMixer.SetFloat("SFXVol", sfxDb);

        //Debug.Log("masterDb is: " + masterDb + ", but MasterVolPref is" + PlayerPrefs.GetFloat("MasterVolPref"));

        //mainAudioMixer.GetFloat("MasterVol", out float check);
        //Debug.Log("Mixer MasterVol after apply = " + check);

        /*
        // ---------- VISUAL ----------
        Screen.fullScreen =
            PlayerPrefs.GetInt("FullscreenPref", 1) == 1;

        QualitySettings.SetQualityLevel(
            PlayerPrefs.GetInt("QualityPref",
            QualitySettings.GetQualityLevel())
        );

        // ---------- RESOLUTION ----------
        Resolution[] resolutions = Screen.resolutions;
        int resIndex = PlayerPrefs.GetInt("ResolutionIndexPref", -1);

        if (resIndex >= 0 && resIndex < resolutions.Length)
        {
            Resolution r = resolutions[resIndex];
            Screen.SetResolution(
                r.width,
                r.height,
                Screen.fullScreenMode,
                r.refreshRateRatio
            );
        }
        */
        //Debug.Log("Settings loaded successfully");

    }
}
