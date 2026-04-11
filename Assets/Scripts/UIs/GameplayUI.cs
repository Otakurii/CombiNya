using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameplayUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject PauseMenuPanel;
    public GameObject SettingsPanel;
    public GameObject CargoPanel;

    [Header("Input (assign InputActionReferences)")]
    public InputActionReference escButton;             //mouse's delta cursor


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;

        PauseMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CargoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (escButton?.action != null && escButton.action.IsPressed())
        {
            OpenPauseMenu();
        }
    }

    //enables input actions when script is active
    //.? is when actions isnt assign, it does nothing
    void OnEnable()
    {
        escButton?.action?.Enable();
    }

    //disables input actions when script is inactive
    void OnDisable()
    {
        escButton?.action?.Disable();
    }



    public void OpenPauseMenu()
    {
        Time.timeScale = 0.0f;
        PauseMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        CargoPanel.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        PauseMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        CargoPanel.SetActive(false);
    }

    public void OpenCargoPanel()
    {
        PauseMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CargoPanel.SetActive(true);
    }

    public void CloseAllMenu()
    {
        Time.timeScale = 1.0f;
        //Debug.Log("Close all menus");
        PauseMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CargoPanel.SetActive(false);
    }

    //public void GoToMainMenu()
    //{
    //    Time.timeScale = 1.0f;
    //    SceneManager.LoadScene(0);
    //}

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

}
