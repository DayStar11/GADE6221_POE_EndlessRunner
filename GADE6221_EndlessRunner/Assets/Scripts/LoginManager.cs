using UnityEngine;
using TMPro;


public class LoginManager : MonoBehaviour
{

    public TMP_InputField nameInput;
    public TMP_InputField passwordInput;

    public GameObject loginPanel;



    public void SaveLogin()
    {

        DatabaseManager.Instance.player.playerName =
        nameInput.text;


        DatabaseManager.Instance.player.password =
        passwordInput.text;


        DatabaseManager.Instance.SavePlayer();


        loginPanel.SetActive(false);


    }



}

