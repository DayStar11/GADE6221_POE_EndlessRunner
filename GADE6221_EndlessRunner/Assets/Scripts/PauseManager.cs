using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject optionsPanel;

    public Slider brightnessSlider;
    public Image brightnessOverlay;
    public Slider volumeSlider;


    private bool paused = false;


    void Start()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);

        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);

        brightnessSlider.value = savedBrightness;
        volumeSlider.value = savedVolume;

        //ChangeBrightness(savedBrightness);
        ChangeVolume(savedVolume);

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



    //public void ChangeBrightness(float value)
    //{
    //    Color colour = brightnessOverlay.color;
    //    colour.a = 1f - value;
    //    brightnessOverlay.color = colour;
    //}




    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }

}