using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    public Slider brightnessSlider;
    public Image brightnessOverlay;
    public Slider volumeSlider;


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

        PlayerPrefs.SetFloat("Brightness", value);
    }


    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;

        PlayerPrefs.SetFloat("Volume", value);
    }



}