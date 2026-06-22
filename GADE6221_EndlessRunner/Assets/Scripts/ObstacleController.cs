using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ObstacleController : MonoBehaviour
{
    //daiyaan:
    PlayerController playerMovement;


    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerController>();
    }

    // put this in notepad its the hitting obstacles multi

    //kylin:
    void Update()
    {
        if (playerMovement != null && playerMovement.transform.position.z > transform.position.z + 1f && !counted)
        {
            playerMovement.AddDodgePoint();
            counted = true;
        }
    }
    private bool counted = false;

}



