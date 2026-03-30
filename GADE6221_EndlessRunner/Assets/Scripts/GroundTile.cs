using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    GroundGenerator groundGenerator;

    void Start()
    {
        groundGenerator = GameObject.FindObjectOfType<GroundGenerator>();
        ObstacleGenerator();
    }

    private void OnTriggerExit(Collider other)
    {
        groundGenerator.GenerateTile();
        Destroy(gameObject, 2); //destroys groundtile prefabs once player has left them
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject obstaclePrefab;

    void ObstacleGenerator() //chooses a random empty position on the groundTile to spawn an obstacle
    {
        int obstacleGeneratorIndex = Random.Range(2, 5); //2,3,4 are the indexes of the obstacle EMPTY'S in the GroundPrefab
        Transform obstaclePosition = transform.GetChild(obstacleGeneratorIndex).transform; //returns the gameobject "ObstaclePrefab"
        Instantiate(obstaclePrefab, obstaclePosition.position, Quaternion.identity, transform); //Quarternion.identity ensures that the object does not rotate
    }
}
