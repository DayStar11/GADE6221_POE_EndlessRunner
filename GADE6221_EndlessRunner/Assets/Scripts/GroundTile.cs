using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    GroundGenerator groundGenerator;

    PlayerController player;
    public GameObject obstaclePrefab;   // jump obstacle
    public GameObject obstacle2Prefab;  // platform/mantle obstacle
    public GameObject obstacle3Prefab;  // hard avoid obstacle

    public GameObject coinPrefab;
    public GameObject groundCoinPrefab;
    public GameObject permeatePrefab;

    public GameObject shieldPrefab;
    public GameObject bossKillerPrefab;

    // powerups
    public GameObject coinMagnetPrefab;
    public GameObject superJumpPrefab;

    private int[] lanes = new int[] { 2, 3, 4 };

    private bool jumpObstacleSpawned = false;
    private int jumpLaneIndex = -1;

    private static int previousSafeLane = -1;
    private static int repeatCount = 2;

    void Start()
    {
        groundGenerator = GameObject.FindObjectOfType<GroundGenerator>();

        player = GameObject.FindObjectOfType<PlayerController>();

        SpawnTile();
    }
    void SpawnBossFightPickups()
    {
        int lane;
        Vector3 spawnPos;

        // spawn above jump/platform obstacles if possible
        if (jumpObstacleSpawned && Random.value > 0.35f)
        {
            lane = jumpLaneIndex;

            Transform pos = transform.GetChild(lanes[lane]).transform;

            spawnPos = pos.position + Vector3.up * 2.5f;

            // check if obstacle is a platform
            Collider[] hits = Physics.OverlapSphere(pos.position, 1f);

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Platform"))
                {
                    spawnPos = pos.position + Vector3.up * 3f;
                    break;
                }
            }
        }
        else
        {
            lane = Random.Range(0, 3);

            Transform pos = transform.GetChild(lanes[lane]).transform;

            spawnPos = pos.position + Vector3.up * 0.8f;
        }

        int type = Random.Range(0, 100);

        if (type < 75)
        {
            Instantiate(shieldPrefab, spawnPos, Quaternion.identity, transform);
        }
        else
        {
            Instantiate(bossKillerPrefab, spawnPos, Quaternion.identity, transform);
        }
    }

    void SpawnTile()
    {
        SpawnPattern();
        GenerateCoins();
        GroundCoinGenerator();
        GeneratePowerUps();

        if (player != null && player.bossFightActive)
        {
            SpawnBossFightPickups();
        }

    }


    private void OnTriggerExit(Collider other)
    {
        groundGenerator.GenerateTile();
        Destroy(gameObject, 2f);
    }

    void SpawnPattern()
    {
        jumpObstacleSpawned = false;
        jumpLaneIndex = -1;

        int safeLane = Random.Range(0, 3);

        if (safeLane == previousSafeLane) // tracks how often the same lane is safe to prevent patterns from forming
        {
            repeatCount++;
        }
        else
        {
            repeatCount = 0;
        }

        if (repeatCount >= 2)
        {
            safeLane = (safeLane + Random.Range(1, 3)) % 3;
            repeatCount = 0;
        }

        previousSafeLane = safeLane;

        int jumpLane = (safeLane + 1) % 3;
        int dangerLane = (safeLane + 2) % 3;

        for (int i = 0; i < lanes.Length; i++) // creates obstacles based on the different lane types above
        {
            Transform pos = transform.GetChild(lanes[i]).transform;

            if (i == safeLane)
                continue;

            if (i == jumpLane)
            {
                int type = Random.Range(0, 2);

                if (type == 0)
                    Instantiate(obstaclePrefab, pos.position, Quaternion.identity, transform);
                else
                    Instantiate(obstacle2Prefab, pos.position, Quaternion.identity, transform);

                jumpObstacleSpawned = true;
                jumpLaneIndex = i;
            }

            if (i == dangerLane)
            {
                Instantiate(obstacle3Prefab, pos.position, Quaternion.identity, transform);
            }
        }
    }
    void GenerateCoins()
    {
        int coinChance = Random.Range(0, 100);

        // slightly more coins
        if (coinChance > 56) return;

        int lane;
        Vector3 spawnPos;

        // air coins only if jump object exists
        if (jumpObstacleSpawned && Random.value > 0.35f)
        {
            lane = jumpLaneIndex;
            Transform pos = transform.GetChild(lanes[lane]).transform;
            spawnPos = pos.position + Vector3.up * 2.5f;
        }
        else
        {
            lane = Random.Range(0, 3);
            Transform pos = transform.GetChild(lanes[lane]).transform;
            spawnPos = pos.position + Vector3.up * 0.2f;
        }

        Instantiate(coinPrefab, spawnPos, Quaternion.identity, transform);
    }

    void GroundCoinGenerator()
    {
        int chance = Random.Range(0, 100);

        if (chance > 68) return;

        int lane = Random.Range(0, 3);
        Transform pos = transform.GetChild(lanes[lane]).transform;

        Vector3 spawnPos = pos.position + Vector3.up * 0.2f;

        Instantiate(groundCoinPrefab, spawnPos, Quaternion.identity, transform);
    }

    void GeneratePowerUps()
    {
        int spawnChance = Random.Range(0, 100);

        if (spawnChance > 40) return;

        int lane;
        Vector3 spawnPos;

        // spawn above jump/mantle if jump lane exists
        if (jumpObstacleSpawned && Random.value > 0.35f)
        {
            lane = jumpLaneIndex;
            Transform pos = transform.GetChild(lanes[lane]).transform;

            spawnPos = pos.position + Vector3.up * 2.5f;

            // check if it's the mantle obstacle instead
            Collider[] hits = Physics.OverlapSphere(pos.position, 1f);

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Platform"))
                {
                    spawnPos = pos.position + Vector3.up * 3f;
                    break;
                }
            }
        }
        else
        {
            lane = Random.Range(0, 3);
            Transform pos = transform.GetChild(lanes[lane]).transform;
            spawnPos = pos.position + Vector3.up * 0.8f;
        }

        int powerType = Random.Range(0, 100);

        if (powerType < 20)
        {
            Instantiate(coinMagnetPrefab, spawnPos, Quaternion.identity, transform);
            Debug.Log("Coin Magnet spawned");
        }
        else if (powerType < 60)
        {
            Instantiate(superJumpPrefab, spawnPos, Quaternion.identity, transform);
            Debug.Log("Super Jump spawned");
        }
        else
        {
            Instantiate(permeatePrefab, spawnPos, Quaternion.identity, transform);
            Debug.Log("Permeate spawned");
        }
    }

  
}

//daiyaan - ground coins
// public GameObject groundCoinPrefab;

//    void GroundCoinGenerator()
//  
//    int groundCoinGeneratorIndex = Random.Range(2, 5);
//  Transform groundCoinPosition = transform.GetChild(groundCoinGeneratorIndex).transform;
//Vector3 spawnPos = groundCoinPosition.position + new Vector3(0, 0, 0);
//Instantiate(coinPrefab, groundCoinPosition.position, Quaternion.identity, transform);
// 




