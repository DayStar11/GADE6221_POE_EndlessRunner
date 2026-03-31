using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    PlayerController playerMovement;


    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision) // this function is necessary for our player to die after colliding with the an obstacle. however, i plan on reworking this using a for loop so that player can bump and obstacle multiple times before dying (lives) - daiyaan
    {
        if (collision.gameObject.name == "Player" ) //if the player collided with an obstacle
        {
            playerMovement.PlayerDeath();
        }
    }


    void Update()
    {
        // function for player to earn points after passing an opstacle. checks if player has passed the obstacle
        if (playerMovement != null && playerMovement.transform.position.z > transform.position.z + 1f && !counted)
        {
            playerMovement.AddDodgePoint();
            counted = true;
        }
    }

    private bool counted = false;
}
