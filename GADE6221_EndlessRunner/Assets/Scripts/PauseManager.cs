using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject optionsPanel;

    public Slider brightnessSlider;
    public Slider volumeSlider;


    private bool paused = false;


    void Start()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);

        Time.timeScale = 1f;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {

            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }

    }



    public void Pause()
    {
        paused = true;

        pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }



    public void Resume()
    {
        paused = false;

        pausePanel.SetActive(false);

        optionsPanel.SetActive(false);

        Time.timeScale = 1f;
    }



    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }



    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }



    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Main_Menu");
    }



    public void ChangeBrightness(float value)
    {
        RenderSettings.ambientIntensity = value;
    }



    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }

}