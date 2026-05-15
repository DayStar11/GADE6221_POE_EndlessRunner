using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;

    public float followDistance = 3f;
    public float moveSpeed = 12f;

    public float attackDistance = 2f;

    public float attackCooldown = 15f;
    private float attackTimer;

    public int damage = 10;

    private bool retreating = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 targetPos = player.position - player.forward * followDistance;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            moveSpeed * Time.fixedDeltaTime
        );

        attackTimer -= Time.fixedDeltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackDistance && attackTimer <= 0)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        PlayerController playerController =
            player.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.BossDamage(damage);
        }

        attackTimer = attackCooldown;

        followDistance = 10f;

        Invoke(nameof(ReturnCloser), 2f);
    }

    void ReturnCloser()
    {
        followDistance = 6f;
    }

    public void Retreat()
    {
        retreating = true;

        Destroy(gameObject, 2f);
    }
}
