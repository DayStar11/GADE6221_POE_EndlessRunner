using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public GameObject bossPrefab;

    public PlayerController player;

    public float spawnDistance = 500f;

    public bool bossSpawned = false;

    public TextMeshProUGUI bossWarningText;
    //boss fight loop mechanics - daiyaan
    public float respawnDistance = 500f;
    private float nextSpawnDistance;
    private BossController currentBoss;

    public HealthBar bossHealthBar;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        //boss fight loop mechanics - daiyaan
        nextSpawnDistance = spawnDistance;

        if (bossWarningText != null)
        {
            bossWarningText.gameObject.SetActive(false);
        }
        //dont display healthbar upon start
        if (bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(false);
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
        GameManagement gm = FindFirstObjectByType<GameManagement>();

        if (gm != null)
        {
            gm.BossSpawned();
        }

        AudioManager.Instance.PlaySFX(
    AudioManager.Instance.bossSpawnSound
);
        //only show players healthbar once boss has spawned
        player.playerHealthBar.gameObject.SetActive(true);


        Vector3 spawnPos = player.transform.position;
        spawnPos.z -= 5f;
        spawnPos.y = 1f;

        GameObject bossInstance = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        BossController bossScript = bossInstance.GetComponent<BossController>();
        bossScript.bossHealthBar = bossHealthBar;
        if (bossScript != null)
        {
            bossScript.player = player.transform;
            currentBoss = bossScript;
            player.bossFightActive = true;
        }

        //show boss healthbar
        bossHealthBar.gameObject.SetActive(true);

        if (bossWarningText != null)
        {
            StartCoroutine(BossWarning());
        }

        Debug.Log("Spawned " + bossInstance.name + " and successfully connected to Player tracking.");
    }

    public void BossDefeated() //daiyaan
    {
        GameManagement gm = FindFirstObjectByType<GameManagement>();

        if (gm != null)
        {
            gm.BossBeaten();
        }
        bossSpawned = false;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.bossDeathSound);

        player.playerHealthBar.gameObject.SetActive(false);
        if (bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(false);
        }

        player.bossFightActive = false;
        currentBoss = null;

        //reset health
        player.playerHealth = player.maxHealth;
        player.playerHealthBar.SetHealth(player.playerHealth, player.maxHealth);

        //loads scene 2 upon defeating the boss
        Debug.Log("Level 1 Boss Defeated! Loading Level 2 scene");
        SceneManager.LoadScene("LVL2");
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

