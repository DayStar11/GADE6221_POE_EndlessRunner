using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    [Header("Music")]
    public AudioSource musicSource;

    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;


    [Header("SFX")]
    public AudioSource sfxSource;

    public AudioClip jumpSound;
    public AudioClip shieldSound;
    public AudioClip magnetSound;
    public AudioClip superJumpSound;
    public AudioClip permeateSound;
    public AudioClip bossKillerSound;
    public AudioClip deathSound;
    public AudioClip bossSpawnSound;
    public AudioClip bossDeathSound;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void PlayMainMenuMusic()
    {
        if (musicSource.clip == mainMenuMusic)
            return;


        musicSource.clip = mainMenuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }



    public void PlayGameMusic()
    {
        if (musicSource.clip == gameMusic)
            return;


        musicSource.clip = gameMusic;
        musicSource.loop = true;
        musicSource.Play();
    }



    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;

        PlayerPrefs.SetFloat("Volume", value);
    }

}