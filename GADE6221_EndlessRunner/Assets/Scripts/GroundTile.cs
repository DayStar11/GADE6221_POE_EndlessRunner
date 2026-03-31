using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    GroundGenerator groundGenerator;

    void Start()
    {
        groundGenerator = GameObject.FindObjectOfType<GroundGenerator>();
        Obstacle1Generator();
        Obstacle2Generator();
        Obstacle3Generator();

        GenerateCoins(); // kylin coin generator function
        GroundCoinGenerator(); //daiyaan - ground coin function
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
    //daiyaan:
    public GameObject obstaclePrefab;
    public GameObject obstacle2Prefab;
    public GameObject obstacle3Prefab;


    void Obstacle1Generator() //chooses a random empty position on the groundTile to spawn an obstacle
    {
        int obstacleGeneratorIndex = Random.Range(2, 5); //2,3,4 are the indexes of the obstacle EMPTY'S in the GroundPrefab
        Transform obstaclePosition = transform.GetChild(obstacleGeneratorIndex).transform; //returns the gameobject "ObstaclePrefab"
        Instantiate(obstaclePrefab, obstaclePosition.position, Quaternion.identity, transform); //Quarternion.identity ensures that the object does not rotate
    }
    void Obstacle2Generator() 
    {
        int obstacle2GeneratorIndex = Random.Range(2, 5); 
        Transform obstaclePosition = transform.GetChild(obstacle2GeneratorIndex).transform; 
        Instantiate(obstacle2Prefab, obstaclePosition.position, Quaternion.identity, transform); 
    }
    void Obstacle3Generator() 
    {
        int obstacle3GeneratorIndex = Random.Range(2, 5); 
        Transform obstaclePosition = transform.GetChild(obstacle3GeneratorIndex).transform; 
        Instantiate(obstacle3Prefab, obstaclePosition.position, Quaternion.identity, transform); 
    }

    //kylin:
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

    //daiyaan - ground coins
    public GameObject groundCoinPrefab;

    void GroundCoinGenerator()
    {
        int groundCoinGeneratorIndex = Random.Range(2, 5);
        Transform groundCoinPosition = transform.GetChild(groundCoinGeneratorIndex).transform;
        Vector3 spawnPos = groundCoinPosition.position + new Vector3(0, 0, 0);
        Instantiate(coinPrefab, groundCoinPosition.position, Quaternion.identity, transform);
    }

}
