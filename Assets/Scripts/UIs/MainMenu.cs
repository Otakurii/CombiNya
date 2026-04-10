using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void CloseSettingsPanel()
    {
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    
}
