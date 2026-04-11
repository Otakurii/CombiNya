using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class SettingsMenu : MonoBehaviour
{
    //public static SettingsMenu Instance;  

    [Header("Tabs")]
    //public GameObject AudioTab;
    //public GameObject VisualTab;

    [Header("Music Settings")]
    [SerializeField] TextMeshProUGUI masterValueText;
    [SerializeField] TextMeshProUGUI bgmValueText;
    [SerializeField] TextMeshProUGUI sfxValueText;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public AudioMixer mainAudioMixer;
    public AudioSource MasterSource, BGMSource, SFXSource;


    //[Header("Resolutions")]
    //[SerializeField] private TMP_Dropdown resolutionDropdown;
    //[SerializeField] private TMP_Dropdown refreshRateDropdown;

    //private Resolution[] resolutions;       //list of all resolutions
    //private List<double> refreshRates = new List<double>();

    //private int currentResolutionIndex = 0;



    //reference to other scripts
    //[SerializeField] private RotateBook rotatebook;


    void Start()
    {
        //----- Panels/Tabs -----//
        //AudioTab.SetActive(true);
        /*
        VisualTab.SetActive(false);

        //----- Resolutions -----//
        SetupResolutions();
        SetupRefreshRate();
        */
        LoadSettings();
    }

    
    /*
    public void OpenAudioTab()
    {
        AudioTab.SetActive(true);
        //VisualTab.SetActive(false);
    }
    public void OpenVisualTab()
    {
        AudioTab.SetActive(false);
        //VisualTab.SetActive(true);
    }
    */

    //----------------------- Audio ----------------------//
    public void MasterVolume()
    {
        float value = Mathf.Clamp(masterSlider.value, 0.0001f, 1f); // avoid log(0)
        float dB = Mathf.Log10(value) * 20f; // convert 1 to -80 dB

        mainAudioMixer.SetFloat("MasterVol", dB);
        FindFirstObjectByType<AudioManager>()?.LoadSavedVolumes();

        PlayerPrefs.SetFloat("MasterVolPref", dB);   //save into PlayerPrefs
        PlayerPrefs.Save();
        //Debug.Log("Master Playerprefs saved as " + PlayerPrefs.GetFloat("MasterVolPref"));

        //update percentage for players
        masterValueText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void BGMVolume()
    {
        float value = Mathf.Clamp(bgmSlider.value, 0.0001f, 1f); // avoid log(0)
        float dB = Mathf.Log10(value) * 20f; // convert 1 to -80 dB

        mainAudioMixer.SetFloat("BGMVol", dB);
        FindFirstObjectByType<AudioManager>()?.LoadSavedVolumes();

        PlayerPrefs.SetFloat("BGMVolPref", dB);   //save into PlayerPrefs
        PlayerPrefs.Save();
        //Debug.Log("BGM Playerprefs saved as " + PlayerPrefs.GetFloat("BGMVolPref"));

        //update percentage for players
        bgmValueText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void SFXVolume()
    {
        float value = Mathf.Clamp(sfxSlider.value, 0.0001f, 1f); // avoid log(0)
        float dB = Mathf.Log10(value) * 20f; // convert 1 to -80 dB

        mainAudioMixer.SetFloat("SFXVol", dB);
        FindFirstObjectByType<AudioManager>()?.LoadSavedVolumes();

        PlayerPrefs.SetFloat("SFXVolPref", dB);   //save into PlayerPrefs
        PlayerPrefs.Save();
        //Debug.Log("SFX Playerprefs saved as " + PlayerPrefs.GetFloat("SFXVolPref"));

        //update percentage for players
        sfxValueText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    /*
    //mute/unmute every audio type
    public void ToggleMaster()
    {
        bool newState = !BGMSource.mute;

        BGMSource.mute = newState;
        SFXSource.mute = newState;
    }

    public void ToggleBGM()
    {
        BGMSource.mute = !BGMSource.mute;
    }

    public void ToogleSFX()
    {
        SFXSource.mute = !SFXSource.mute;
    }
    */



    /*
    //----------------------- Visual ----------------------//
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode,resolution.refreshRateRatio);

        PlayerPrefs.SetInt("ResolutionIndexPref", resolutionIndex);
        PlayerPrefs.Save();
    }

    public void SetRefreshRate(int refreshRateIndex)
    {
        double selectedRate = refreshRates[refreshRateIndex];

        Resolution currentRes = Screen.currentResolution;

        Screen.SetResolution(
            currentRes.width,
            currentRes.height,
            Screen.fullScreenMode,
            new RefreshRate { numerator = (uint)selectedRate, denominator = 1 }
        );

        PlayerPrefs.SetInt("RefreshRateIndexPref", refreshRateIndex);
        PlayerPrefs.Save();
    }

    void SetupResolutions()
    {
        //----- Resolutions -----//
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(resolutionOption);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        int savedIndex = PlayerPrefs.GetInt("ResolutionIndexPref", currentResolutionIndex);

        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetupRefreshRate()
    {
        refreshRates.Clear();
        refreshRateDropdown.ClearOptions();

        foreach (Resolution res in resolutions)
        {
            double rate = res.refreshRateRatio.value;

            if (!refreshRates.Contains(rate))
            {
                refreshRates.Add(rate);
            }
        }

        List<string> options = new List<string>();
        foreach (double rate in refreshRates)
        {
            options.Add(rate.ToString("0") + " Hz");
        }

        refreshRateDropdown.AddOptions(options);
        refreshRateDropdown.RefreshShownValue();
    }

    //fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        //Debug.Log("Full screen is" + isFullscreen);
        Screen.fullScreen = isFullscreen;

        PlayerPrefs.SetInt("FullscreenPref", isFullscreen ? 1 : 0);
    }


    //graphics quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        PlayerPrefs.SetInt("QualityPref", qualityIndex);
    }
    */



    //----------------------- SAVE / LOAD SETTINGS ----------------------//
    void LoadSettings()
    {
        //Debug.Log("Loading real settings() rn");


        // ---------- AUDIO ----------
        //set slider values to half at default
        float masterDb = PlayerPrefs.GetFloat("MasterVolPref", -6f);
        float bgmDb = PlayerPrefs.GetFloat("BGMVolPref", -6f);
        float sfxDb = PlayerPrefs.GetFloat("SFXVolPref", -6f);

        masterSlider.value = Mathf.Pow(10f, masterDb / 20f);
        bgmSlider.value = Mathf.Pow(10f, bgmDb / 20f);
        sfxSlider.value = Mathf.Pow(10f, sfxDb / 20f);

        // Apply to audio mixer
        mainAudioMixer.SetFloat("MasterVol", masterDb);
        mainAudioMixer.SetFloat("BGMVol", bgmDb);
        mainAudioMixer.SetFloat("SFXVol", sfxDb);

        // Update text displays
        masterValueText.text = Mathf.RoundToInt(Mathf.Pow(10f, masterDb / 20f) * 100) + "%";
        bgmValueText.text = Mathf.RoundToInt(Mathf.Pow(10f, bgmDb / 20f) * 100) + "%";
        sfxValueText.text = Mathf.RoundToInt(Mathf.Pow(10f, sfxDb / 20f) * 100) + "%";

        //Debug.Log("masterDb is: " + masterDb + ", but MasterVolPref is" + PlayerPrefs.GetFloat("MasterVolPref"));

        /*
        // ---------- VISUAL ----------
        // Load and apply fullscreen
        bool isFullscreen = PlayerPrefs.GetInt("FullscreenPref", 1) == 1;
        Screen.fullScreen = isFullscreen;

        // Load and apply quality
        int qualityIndex = PlayerPrefs.GetInt("QualityPref", QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(qualityIndex);

        // Load and apply resolution
        int savedResIndex = PlayerPrefs.GetInt("ResolutionIndexPref", currentResolutionIndex);
        resolutionDropdown.SetValueWithoutNotify(savedResIndex);
        resolutionDropdown.RefreshShownValue();

        // Apply the saved resolution
        if (savedResIndex >= 0 && savedResIndex < resolutions.Length)
        {
            Resolution savedRes = resolutions[savedResIndex];
            Screen.SetResolution(savedRes.width, savedRes.height, Screen.fullScreenMode, savedRes.refreshRateRatio);
        }

        // Load and apply refresh rate
        int savedRefreshIndex = PlayerPrefs.GetInt("RefreshRateIndexPref", 0);
        if (savedRefreshIndex >= 0 && savedRefreshIndex < refreshRates.Count)
        {
            refreshRateDropdown.SetValueWithoutNotify(savedRefreshIndex);
            refreshRateDropdown.RefreshShownValue();
        }
        */

        //Debug.Log("Settings loaded successfully");
    }
}

