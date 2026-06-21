using TMPro;
using UnityEngine;
using System.Collections;

public class BossManager : MonoBehaviour
{
    public GameObject bossPrefab;

    public PlayerController player;

    public float spawnDistance = 20f;

    public bool bossSpawned = false;

    public TextMeshProUGUI bossWarningText;
    //boss fight loop mechanics - daiyaan
    public float respawnDistance = 500f;
    private float nextSpawnDistance;
    private BossController currentBoss;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        //boss fight loop mechanics - daiyaan
        nextSpawnDistance = spawnDistance;

        if (bossWarningText != null)
        {
            bossWarningText.gameObject.SetActive(false);
        }


    }

    void FixedUpdate()
    {
        if (!bossSpawned && player != null && player.transform.position.z >= nextSpawnDistance)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        bossSpawned = true;
        //only show players healthbar once boss has spawned
        player.playerHealthBar.gameObject.SetActive(true);

        Vector3 spawnPos = player.transform.position;
        spawnPos.z -= 5f; 
        spawnPos.y = 1f;

        GameObject bossInstance = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        BossController bossScript = bossInstance.GetComponent<BossController>();
        if (bossScript != null)
        {
            bossScript.player = player.transform;
            currentBoss = bossScript;
            player.bossFightActive = true;
        }

        if (bossWarningText != null)
        {
            StartCoroutine(BossWarning());
        }

        Debug.Log("Spawned " + bossInstance.name + " and successfully connected to Player tracking.");
    }

    public void BossDefeated() //daiyaan
    {
        bossSpawned = false;

        //remove players healthbar
        player.playerHealthBar.gameObject.SetActive(false);

        player.bossFightActive = false;

        nextSpawnDistance = player.transform.position.z + respawnDistance;

        currentBoss = null;

        player.playerHealth = 50;
    }

    IEnumerator BossWarning()
    {
        bossWarningText.gameObject.SetActive(true);

        for (int i = 0; i < 6; i++)
        {
            bossWarningText.enabled = !bossWarningText.enabled;

            yield return new WaitForSeconds(0.4f);
        }

        bossWarningText.gameObject.SetActive(false);
    }
}

