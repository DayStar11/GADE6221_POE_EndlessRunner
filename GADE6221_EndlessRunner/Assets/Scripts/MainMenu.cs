using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    public Slider brightnessSlider;
    public Image brightnessOverlay;
    public Slider volumeSlider;

    public GameObject loginPanel;
    public GameObject highscorePanel;

    public Button startButton;
    public Button loginButton;


    void Start()
    {
        optionsPanel.SetActive(false);

        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);

        brightnessSlider.value = savedBrightness;
        volumeSlider.value = savedVolume;

        ChangeBrightness(savedBrightness);
        ChangeVolume(savedVolume);

        AudioManager.Instance.PlayMainMenuMusic();
        loginPanel.SetActive(false);
        highscorePanel.SetActive(false);

        if (DatabaseManager.Instance.player.playerName != "")
        {

            startButton.interactable = true;

            loginButton.gameObject.SetActive(true);

        }

        else
        {

            startButton.interactable = false;

        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GADE6221_POE_COIN_SCENE");
    }


    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }


    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }


    public void QuitGame()
    {
        Debug.Log("Game Closed");

        Application.Quit();
    }


    public void ChangeBrightness(float value)
    {
        Color colour = brightnessOverlay.color;
        colour.a = 1f - value;
        brightnessOverlay.color = colour;
    }

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;

        PlayerPrefs.SetFloat("Volume", value);
    }

    public void OpenLogin()
    {

        loginPanel.SetActive(true);

    }

    public void CloseLogin()
    {
        loginPanel.SetActive(false);
    }
    public void OpenHighScore()
    {

        highscorePanel.SetActive(true);

    }

    public void CloseHighScore()
    {
        highscorePanel.SetActive(false);
    }

}