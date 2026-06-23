using UnityEngine;
using TMPro;


public class HighscoreManager : MonoBehaviour
{

    public TMP_Text text;


    public void ShowHighscore()
    {

        text.text =
        "Player: "
        +
        DatabaseManager.Instance.player.playerName
        +
        "\n\nHighscore: "
        +
        DatabaseManager.Instance.player.highScore;


    }

}

