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

    void Start()
    {
        bossWarningText.gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
        Debug.Log("BossManager Running");

        Debug.Log(player.transform.position.z);

        if (!bossSpawned &&
            player.transform.position.z >= spawnDistance)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        Debug.Log("BOSS SPAWNED");

        bossSpawned = true;

        Vector3 spawnPos = player.transform.position;

        spawnPos.z -= 5f;
        spawnPos.y = 1f;

        Instantiate(bossPrefab, spawnPos, Quaternion.identity);
    }
    IEnumerator BossWarning()
    {
        bossWarningText.gameObject.SetActive(true);

        for (int i = 0; i < 6; i++)
        {
            bossWarningText.enabled =
                !bossWarningText.enabled;

            yield return new WaitForSeconds(0.4f);
        }

        bossWarningText.gameObject.SetActive(false);
    }
}

