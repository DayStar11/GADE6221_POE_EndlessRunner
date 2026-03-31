using UnityEngine;
using UnityEngine.SceneManagement; //restart mechanics
using TMPro;// UI elements for coin and dodge point systems - kylin 

public class PlayerController : MonoBehaviour
{    //kylin player coin and point system
    public int coins = 0; // coins collected
    public int DodgePoints = 0; // obstacle dodge points
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI dodgeText;

    //daiyaan:
    public float playerSpeed = 15.0f;
    public float turningSpeed = 20.0f;
    //jump mechanics:
    public float jumpForce = 6.0f;
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

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * jumpForce *2f * Time.deltaTime;
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


    void OnCollisionEnter(Collision collision)
    {
        //daiyaan: this allows player to land on top of obstacle2, without dying. However, if player bumps its sides, they'll still die
        if (collision.gameObject.CompareTag("Platform")) //obstacle 2's tag is set to platform so that this logic only applies to it
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                //makes sure that the player landed on top of the obstacle
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
                {
                    onGround = true;
                    return;
                }
            }
            //if player did not land on top of it, calls playerdeath()
            PlayerDeath();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
     //kylin:
    void UpdateUI() 
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