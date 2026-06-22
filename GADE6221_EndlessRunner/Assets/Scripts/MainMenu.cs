using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    public Slider brightnessSlider;
    public Slider volumeSlider;


    void Start()
    {
        optionsPanel.SetActive(false);

        brightnessSlider.value = 1;
        volumeSlider.value = 1;
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
        RenderSettings.ambientIntensity = value;
    }


    public void ChangeVolume(float value)
    {
        // ready for audio later

        AudioListener.volume = value;
    }
}