using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public GameObject gameOver;
    public TextMeshProUGUI finalCoinText;
    public TextMeshProUGUI finalDodgeText;
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        gameOver.SetActive(false); // hides the game over panel


    }

    public void GameOver()
    {
        // shows the game over panel when player dies
        gameOver.SetActive(true);
        finalDodgeText.gameObject.SetActive(true); // displays the players final points 
        finalCoinText.gameObject.SetActive(true); // displays the players final coin score

        // displays the final player score
        finalCoinText.text = "Coins Collected: " + player.coins;
        finalDodgeText.text = "Score Points: " + player.DodgePoints;
        
        /// stops the game when the player dies
        Time.timeScale = 0f;
    }

       // allows player to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}