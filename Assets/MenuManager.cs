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
            isInGame = false;
            pauseMenuUI.SetActive(true);
        }
    }
    // MAIN MENU PANEL
    public void OnStartGameClicked()
    {
        isInGame = true;
        mainMenuUI.SetActive(false);
    }
    public void OnSettingsClicked()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
    // SETTINGS MENU PANEL
    public void OnSettingsBackClicked()
    {
        mainMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }
    // PAUSE MENU PANEL
    public void OnQuitGameClicked()
    {
        // handle the end of your game here
        mainMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    public void OnResumeGameClicked()
    {
        isInGame = true;
        pauseMenuUI.SetActive(false);
    }
    // Game Over triggered from the loaded scene
    public void OnGameOver()
    {
        isInGame = false;
        mainMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
}
