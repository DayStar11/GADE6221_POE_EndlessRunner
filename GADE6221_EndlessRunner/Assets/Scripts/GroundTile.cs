using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    GroundGenerator groundGenerator;

    void Start()
    {
        groundGenerator = GameObject.FindObjectOfType<GroundGenerator>();
        ObstacleGenerator();
        GenerateCoins(); // kylin coin generator function
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

    public GameObject coinPrefab; // Kylin coin prefab
    public int maxCoinsPerTile = 2; // max num of coins that can spawn per tile

    void GenerateCoins()
    {
        
        {
            int coinsToSpawn = Random.Range(0, 2); // 
            if (coinsToSpawn == 0) return; // skips certain tiles to create coins

            // pick a random empty child index to place the coin
            int coinIndex = Random.Range(2, 5); // uses the same empty child as the obstacle generator
            Transform coinPos = transform.GetChild(coinIndex).transform;
            // adjusted spwan position for coins
            Vector3 spawnPos = coinPos.position + new Vector3(0, 2.5f, 0);

            Instantiate(coinPrefab, spawnPos, Quaternion.identity, transform);
        }
    }



}
