using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject settingsMenuUI;
    public GameObject pauseMenuUI;

    private bool isInGame;
    void Start()
    {
        isInGame = false;
        mainMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
    }
    void Update()
    {
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.App))
        {
            OnAppButtonClicked();
        }
    }
    // APP BUTTON CLICKED, pause the game
    private void OnAppButtonClicked()
    {
        Debug.Log("It's me again. The App Button");

        // show pause panel and pause game, set time to 0
        if (isInGame)
        {
            Debug.Log("Trying to pause");
            isInGame = false;
            mainMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            settingsMenuUI.SetActive(false);
        }
        else
        {
            Debug.Log("Still in game");
        }

    }
    // MAIN MENU PANEL
    public void OnStartGameClicked()
    {
        Debug.Log("This is OnStartGameClicked()");
        isInGame = true;
        mainMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
    }
    public void OnSettingsClicked()
    {
        mainMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
    // SETTINGS MENU PANEL
    public void OnSettingsBackClicked()
    {
        mainMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
    }
    // PAUSE MENU PANEL
    public void OnQuitGameClicked()
    {
        // handle the end of your game here
        mainMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
    }
    public void OnResumeGameClicked()
    {
        isInGame = true;
        mainMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
    }
    // Game Over triggered from the loaded scene
    public void OnGameOver()
    {
        Debug.Log("This is OnGameOver()");
        isInGame = false;
        mainMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
