using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    //daiyaan:
    PlayerController playerMovement;


    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerController>();
    }

  // put this in notepad its the hitting obstacles multi

    //kylin:
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
