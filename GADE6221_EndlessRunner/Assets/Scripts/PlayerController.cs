using UnityEngine;
using UnityEngine.SceneManagement; //restart mechanics
using TMPro;// UI elements for coin and dodge point systems - kylin 

public class PlayerController : MonoBehaviour
{    //kylin player coin and point system
    public int coins = 0; // coins collected
    public int DodgePoints = 0; // obstacle dodge points
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI dodgeText;

    public float playerSpeed = 15.0f;
    public float turningSpeed = 15.0f;
    //jump mechanics:
    public float jumpForce = 5.0f;
    private bool onGround;
    private Rigidbody rb;
    //boundaries:
    public float rightLimit = 5.0f;
    public float leftLimit = -5.0f;
    //player life mechanics:
    bool isDead = false;



    public float xInput;

    void Start()
    {
        //for jump physics
        rb = GetComponent<Rigidbody>();
        onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        //player life mechanics:
        if (isDead != false)
        {
            return;
        }
       

        //if player falls off the platform:
        if (transform.position.y < -5)
        {
            PlayerDeath(); //calls the PlayerDeath function if player falls off of the platform. checks their vertical(y) position. if players y position is < -5, PlayerDeath() is triggered
        }

        //creates upward force (jump)
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }

        //'time.deltaTime' makes players movement relative to the games speed, 'Space.World' makes players movement relative to the world around the player
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //turn left
        {
            if (this.gameObject.transform.position.x > leftLimit)
            {
                transform.Translate(Vector3.left * Time.deltaTime * turningSpeed);
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //turn right
        {
            if (this.gameObject.transform.position.x < rightLimit)
            {
                transform.Translate(Vector3.right * Time.deltaTime * turningSpeed);
            }
        }
    }

    public void PlayerDeath()  //player life mechanics:
    {
        isDead = true;
       
        //got rid of scene manager 

        // gives the player the option to restart kylin
        GameManagement gc = GameObject.FindObjectOfType<GameManagement>();
        if (gc != null)
        {
            gc.GameOver(); // displays the game over panel
        }
    }

    //void RestartGame() //this method will be used to eventually give the player the option to restart or quit. method will most likely be moved to an Empty containing a GameController.cs. We have not learnt how to create menues yet
    //{

    //}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    void UpdateUI() //kylin
    {    // updates the UI text to record their current coin and dodge points
        if (coinText != null)
        { coinText.text = "Coins: " + coins; }
        if (dodgeText != null)
        { dodgeText.text = "Score " + DodgePoints; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            Destroy(other.gameObject);
            UpdateUI();
        }
    }

    // called when player successfully dodges and obstacle
    public void AddDodgePoint()
    {
        DodgePoints++;
        UpdateUI();
    }
}