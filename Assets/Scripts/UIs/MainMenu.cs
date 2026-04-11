using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }


    public void PlayGame()
    {
        //load gameplay scene
        SceneManager.LoadScene(1);
        
    }

    public void OpenSettingsPanel()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    public void OpenCreditsPanel()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void CloseAllPanels()
    {
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    
}
