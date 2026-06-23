using System.Collections;
using UnityEngine;

public class Boss2Controller : MonoBehaviour
{
    public Transform player;

    private float followDistance = 3f;
    public float moveSpeed = 12f;
    private float startingFollowDistance;

    //public float attackDistance = 2f;

    public float attackCooldown = 15f;
    private float attackTimer;

    //boss attack mechanics - daiyaan
    public int bossHealth = 50;
    private Boss2Manager bossManager;
    private float sameLaneTimer = 0f;
    public float laneAttackDelay = 1f;
    public GameObject destroyEffect;

    public int damage = 10;

    //beamm attack mechanics
    public GameObject beamPrefab;
    public Transform firePoint;
    public float beamChargeTime = 0.8f;
    public float attackInterval = 1f;

    private bool chargingAttack = false;

    private bool retreating = false;

    //mechanics to throw off the bosses reaction time while switching lanes - daiyaan
    private float laneSwitchDelay = 0.2f;
    private float laneTimer = 0f;
    private float targetLaneX;
    private float delayedLaneX;
    //healthbar UI 
    public int maxHealth = 50;
    public HealthBar bossHealthBar;

    void Start()
    {
        Debug.Log(gameObject.name + " has spawned.");

        Debug.Log(bossHealthBar);

        if (bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(true);

            bossHealth = maxHealth;
            bossHealthBar.SetHealth(bossHealth, maxHealth);
        }
        else
        {
            Debug.LogError("No BossHealthBar found in the scene!");
        }

        bossHealth = maxHealth;
        if (bossHealthBar != null)
        {
            bossHealthBar.SetHealth(bossHealth, maxHealth);
        }
        else
        {
            Debug.LogWarning("Boss Health Bar script component could not be found in the scene hierarchy!");
        }

        if (player == null)
        {
            PlayerController foundPlayer = FindFirstObjectByType<PlayerController>();

            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
        }

        targetLaneX = player.position.x;
        delayedLaneX = player.position.x;

        startingFollowDistance = followDistance;
        bossManager = FindFirstObjectByType<Boss2Manager>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // detects which lane the player as moved to
        if (Mathf.Abs(player.position.x - targetLaneX) > 0.1f)
        {
            targetLaneX = player.position.x;
            laneTimer = laneSwitchDelay;
        }
        // waits before changing lane
        if (laneTimer > 0)
        {
            laneTimer -= Time.fixedDeltaTime;

            if (laneTimer <= 0)
            {
                delayedLaneX = targetLaneX;
            }
        }

        Vector3 targetPos = new Vector3(delayedLaneX, transform.position.y, player.position.z - startingFollowDistance);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);

        attackTimer -= Time.fixedDeltaTime;

        if (attackTimer <= 0f && !chargingAttack)
        {
            attackTimer = attackInterval;
            StartCoroutine(FireBeam());
        }

        float distance = Vector3.Distance(transform.position, player.position);
        //boss attack mechanics - daiyaan
        if (Mathf.Abs(player.position.x - transform.position.x) < 0.5f)
        {
            sameLaneTimer += Time.fixedDeltaTime;

            if (sameLaneTimer >= laneAttackDelay && attackTimer <= 0)
            {
                AttackPlayer();
                sameLaneTimer = 0;
            }
        }
        else
        {
            sameLaneTimer = 0;
        }
        //boss attacks only if you stay in the same lane as the boss 
    }

    void SpawnBeam(Vector3 pos)
    {
        GameObject beam = Instantiate(beamPrefab, pos, Quaternion.identity);

        beam.GetComponent<BossBeam>().Fire(pos + Vector3.forward * 10f, damage);
    }

    IEnumerator FireBeam()
    {
        chargingAttack = true;

        yield return new WaitForSeconds(beamChargeTime);

        Vector3 bossPos = firePoint.position;

        Vector3 leftLane = new Vector3(bossPos.x - 5f, bossPos.y, bossPos.z);
        Vector3 midLane = new Vector3(bossPos.x, bossPos.y, bossPos.z);
        Vector3 rightLane = new Vector3(bossPos.x + 5f, bossPos.y, bossPos.z);

        SpawnBeam(leftLane);
        SpawnBeam(midLane);
        SpawnBeam(rightLane);

        attackTimer = attackInterval;
        chargingAttack = false;
    }

    public void TakeDamage(int damageAmount)
    {
        bossHealth -= damageAmount;
        bossHealthBar.SetHealth(bossHealth, maxHealth); //decrease health on healthbar

        Debug.Log("Boss HP: " + bossHealth);

        if (bossHealth <= 0)
        {
            Retreat();
        }
    }

    void AttackPlayer()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.BossDamage(damage);
        }

        attackTimer = attackCooldown;
    }


    public void Retreat()
    {
        if (bossManager != null)
        {
            bossManager.BossDefeated();
        }

        if (bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) //boss destroys obstacles when he collides into them (otherwise it looks like boss is just phasing through them) - daiyaan
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Platform"))
        {
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, collision.contacts[0].point, Quaternion.identity
                );
            }
            Destroy(collision.gameObject);
        }
    }
}

