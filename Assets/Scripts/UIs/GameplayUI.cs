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
    public GameObject GameOverPanel;
    public GameObject EndGamePanel;
    public GameObject GuidePanel;
    public GameObject IntroStoryPanel;

    [Header("IntroStory Panning")]
    [SerializeField] public float speed = 3f;
    public float targetY = 500f;
    private bool isPanning = false;

    [Header("Input (assign InputActionReferences)")]
    public InputActionReference escButton;             //mouse's delta cursor

    [Header("SFX")]
    public string portWavesSFX;
    public string endGameSFX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;

        OpenIntroStoryPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (escButton?.action != null && escButton.action.IsPressed())
        {
            OpenPauseMenu();
        }

        //update the intro story panel
        if (!isPanning) return;
        Transform t = IntroStoryPanel.transform;
        if (t.position.y < targetY)
        {
            t.position += new Vector3(0, speed * Time.deltaTime, 0);
            //Debug.Log("position now at " + t.position.y);
        }
        else
        {
            isPanning = false; // stop moving
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

        GameOverPanel.SetActive(false);
        EndGamePanel.SetActive(false);
        GuidePanel.SetActive(false);
        IntroStoryPanel.SetActive(false);
    }


    public void OpenSettingsMenu()
    {
        PauseMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        CargoPanel.SetActive(false);

        GameOverPanel.SetActive(false);
        EndGamePanel.SetActive(false);
        GuidePanel.SetActive(false);
        IntroStoryPanel.SetActive(false);
    }

    public void OpenCargoPanel()
    {
        PauseMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CargoPanel.SetActive(true);

        GameOverPanel.SetActive(false);
        EndGamePanel.SetActive(false);
        GuidePanel.SetActive(false);
        IntroStoryPanel.SetActive(false);

        AudioManager.Instance.PlaySFX(portWavesSFX);
    }
    public void OpenEndGamePanel()
    {
        PauseMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CargoPanel.SetActive(false);

        GameOverPanel.SetActive(false);
        EndGamePanel.SetActive(true);
        GuidePanel.SetActive(false);
        IntroStoryPanel.SetActive(false);

        AudioManager.Instance.PlaySFX(endGameSFX);
    }
    public void OpenGuidePanel()
    {
        PauseMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CargoPanel.SetActive(false);

        GameOverPanel.SetActive(false);
        EndGamePanel.SetActive(false);
        GuidePanel.SetActive(true);
        IntroStoryPanel.SetActive(false);
    }
    public void OpenIntroStoryPanel()
    {
        isPanning = true;
        PauseMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CargoPanel.SetActive(false);

        GameOverPanel.SetActive(false);
        EndGamePanel.SetActive(false);
        GuidePanel.SetActive(false);
        IntroStoryPanel.SetActive(true);
    }

    public void CloseAllMenu()
    {
        Time.timeScale = 1.0f;
        //Debug.Log("Close all menus");
        PauseMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CargoPanel.SetActive(false);

        GameOverPanel.SetActive(false);
        EndGamePanel.SetActive(false);
        GuidePanel.SetActive(false);
        IntroStoryPanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

}
