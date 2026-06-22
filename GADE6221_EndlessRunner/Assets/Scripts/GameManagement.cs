using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    // existing death UI
    public GameObject gameOver;

    public TextMeshProUGUI finalCoinText;
    public TextMeshProUGUI finalDodgeText;

    public Button continueButton;

    private PlayerController player;

    public GameObject confirmPanel;
    public TextMeshProUGUI confirmText;

    // revive cost
    private int reviveCost = 10;

    // EVENT SCORE SYSTEM

   
    public int pickupsActivated = 0;
    public int levelsBeaten = 0;


    // events
    public event System.Action OnPickupActivated;
    public event System.Action OnBossSpawned;
    public event System.Action OnBossBeaten;


    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        gameOver.SetActive(false);

        Time.timeScale = 1f;
    }


    public void GameOver()
    {
        gameOver.SetActive(true);

        finalDodgeText.gameObject.SetActive(true);
        finalCoinText.gameObject.SetActive(true);


        UpdateScore(); // update the score text with the player's current stats


        Time.timeScale = 0f;
    }



    void UpdateScore()
    {
        finalCoinText.text =
        "Coins Collected: " + player.coins;


        finalDodgeText.text =
        "Score Points: " + player.DodgePoints;
    }



    // restart the level
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }



    // spend coins to continue running
    public void ContinueRun()
    {
        confirmPanel.SetActive(true);

        confirmText.text =
        "Revive costs " + reviveCost + " coins.\n\nAre you sure?";
    }




    // return to main menu
    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Main_Menu");
    }
    public void ConfirmContinue()
    {
        if (player.coins >= reviveCost)
        {
            player.coins -= reviveCost;
            player.UpdateUI();

            reviveCost += 10;

            player.playerHealth = player.maxHealth;

            // reset movement state
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // move player slightly forward so they don't "re-trigger death zone"
            player.transform.position += Vector3.forward * 2f;

            confirmPanel.SetActive(false);
            gameOver.SetActive(false);

            Time.timeScale = 1f;
        }
        else
        {
            confirmText.text = "Not enough coins!";
        }
    }
    public void CancelContinue()
    {
        confirmPanel.SetActive(false);
    }
  



    public void PickupActivated()
    {
        pickupsActivated++;

        OnPickupActivated?.Invoke();

        Debug.Log("Pickups activated: " + pickupsActivated);
    }



    public void BossSpawned()
    {
        OnBossSpawned?.Invoke();

        Debug.Log("Boss Spawn Event");
    }



    public void BossBeaten()
    {
        levelsBeaten++;

        OnBossBeaten?.Invoke();

        Debug.Log("Levels beaten: " + levelsBeaten);
    }

}