using TMPro;// UI elements for coin and dodge point systems - kylin 
using UnityEngine;
using UnityEngine.SceneManagement; //restart mechanics

public class PlayerController : MonoBehaviour
{    //kylin player coin and point system
    public int coins = 0; // coins collected
    public int DodgePoints = 0; // obstacle dodge points
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI dodgeText;
    public TextMeshProUGUI pickUpText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI healthText;

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
    // creating a bool to check if a player already has a pick up kylin
    public bool hasPickUp = false;
    public bool magnetActive = false;
    public bool superJumpActive = false;
    public bool permeateActive = false;
    [Header("Boss Fight")]
    public int playerHealth = 50;
    public bool shieldActive = false;
    public bool bossKillerActive = false;
    private bool pickupLock = false;
    private bool powerUpActive = false;
    // variable to store pickups so they can be activated later
    private PlayerPickups.PickUpType storedPowerUp;
    private float pickUpTimer = 0f;



    private float normalJumpForce;
    private float normalSpeed;

    public bool bossFightActive = false;


    public float xInput;

    void Start()
    {
        //for jump physics
        rb = GetComponent<Rigidbody>();
        onGround = true;

        normalJumpForce = jumpForce;// stores normal jump settings so it can be restored after pickup has been used kylin
        normalSpeed = playerSpeed; // stores normal speed settings so it can be restored after pickup has been used kylin

        if (healthText != null)
        {
            healthText.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()

    {
        Debug.Log("Update running");
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
            rb.linearVelocity += Vector3.up * Physics.gravity.y * jumpForce * 2f * Time.deltaTime;
        }

        //'time.deltaTime' makes players movement relative to the games speed, 'Space.World' makes players movement relative to the world around the player
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);



        if (hasPickUp && Input.GetKeyDown(KeyCode.E)) // player presses e to activate pickup
        {
            ActivateStoredPickUp();
        }

        if (pickUpTimer > 0)
        {
            pickUpTimer -= Time.deltaTime;

            if (timerText != null)
            {
                timerText.gameObject.SetActive(true);
                timerText.text = storedPowerUp + ":" + Mathf.Ceil(pickUpTimer).ToString();
            }

            if (pickUpTimer <= 0)
            {
                DeactivatePickUp();


                hasPickUp = false;
                magnetActive = false;
                superJumpActive = false;
                pickUpTimer = 0f;
                permeateActive = false;
            }

        }

        if (magnetActive)
        {
            MagnetCoins();
        }

        bool blockedSideways = false;

        if (permeateActive)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 0.6f);

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Obstacle") || hit.CompareTag("Platform"))
                {
                    blockedSideways = true;
                    break;
                }
            }
        }

        if (!blockedSideways)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (transform.position.x > leftLimit)
                    transform.Translate(Vector3.left * Time.deltaTime * turningSpeed);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.position.x < rightLimit)
                    transform.Translate(Vector3.right * Time.deltaTime * turningSpeed);
            }
        }
        if (permeateActive)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 0.8f);

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Obstacle") || hit.CompareTag("Platform"))
                {

                    transform.position += Vector3.forward * Time.deltaTime * playerSpeed;
                }
            }
        }

        if (bossKillerActive && Input.GetKeyDown(KeyCode.E))
        {
            BossController boss =
                GameObject.FindObjectOfType<BossController>();

            if (boss != null)
            {
                boss.Retreat();

                bossKillerActive = false;

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
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            return;
        }
        //daiyaan: this allows player to land on top of obstacle2, without dying. However, if player bumps its sides, they'll still die
        if (collision.gameObject.CompareTag("Platform")) //obstacle 2's tag is set to platform so that this logic only applies to it
        {
            if (permeateActive)
                return;

            foreach (ContactPoint contact in collision.contacts)
            {

                //makes sure that the player landed on top of the obstacle
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
                {
                    onGround = true;
                    return;
                }
            }

            PlayerDeath();
            return;
        }


        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (permeateActive)
                return;

            PlayerDeath();
        }




        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayerDeath();
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
            return;
        }

        if (hasPickUp || powerUpActive) return;

        PlayerPickups pickup = other.GetComponent<PlayerPickups>();
        if (pickup != null)
        {
            TryPickupPickUp(pickup.pickUpType, other.gameObject);
        }
    }

    // called when player successfully dodges and obstacle
    public void AddDodgePoint()
    {
        DodgePoints++;
        UpdateUI();
    }

    public void TryPickupPickUp(PlayerPickups.PickUpType type, GameObject pickup)
    {


        if (hasPickUp) return;

        hasPickUp = true;
        storedPowerUp = type;

        Destroy(pickup);

        if (pickUpText != null)
        {

            pickUpText.gameObject.SetActive(true);
            pickUpText.text = "Press E";
        }
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
    }

    void ActivateStoredPickUp()
    {
        hasPickUp = false;
        powerUpActive = true;
        pickUpTimer = 10f;

        if (pickUpText != null)
            pickUpText.text = "";

        if (storedPowerUp == PlayerPickups.PickUpType.CoinMagnet)
        {
            magnetActive = true;
        }

        if (storedPowerUp == PlayerPickups.PickUpType.SuperJump)
        {
            superJumpActive = true;
            jumpForce = normalJumpForce + 6f;
            playerSpeed = normalSpeed + 3f;
        }

        if (storedPowerUp == PlayerPickups.PickUpType.Permeate) // allows players to pass through obstacles 
        {
            permeateActive = true;
        }

        if (storedPowerUp == PlayerPickups.PickUpType.Shield) // allows players to take a hit without losing health
        {
            shieldActive = true;
        }
        if (storedPowerUp == PlayerPickups.PickUpType.BossKiller) // allows players to scare away the boss
        {
            bossKillerActive = true;
        }
    }

    void DeactivatePickUp()
    {
        powerUpActive = false;
        magnetActive = false;
        superJumpActive = false;
        shieldActive = false;
        bossKillerActive = false;

        jumpForce = normalJumpForce;
        playerSpeed = normalSpeed;

        if (timerText != null)
            timerText.text = "";

        if (permeateActive) // if permeate is active players can pass through obstacles but also prevents them from earning points 
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 1.2f);

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Obstacle") || hit.CompareTag("Platform"))
                {
                    PlayerDeath();
                    break;
                }
            }
        }
    }

    void MagnetCoins()
    {
        GameObject[] allCoins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in allCoins)
        {
            float distance = Vector3.Distance(transform.position, coin.transform.position);

            if (distance < 5f)
            {
                coin.transform.position = Vector3.MoveTowards(
                    coin.transform.position,
                    transform.position,
                    10f * Time.deltaTime
                );
            }
        }
    }

    public void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP:" + playerHealth;

        }


    }
    public void BossDamage(int damage)
    {
        if (shieldActive)
        {
            shieldActive = false;
            Debug.Log("Attack blocked");
            return;
        }

        playerHealth -= damage;
        UpdateHealthUI();
        if (playerHealth <= 0)
        {
            PlayerDeath();
        }

    }


}