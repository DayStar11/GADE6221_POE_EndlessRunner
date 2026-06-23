using UnityEngine;

public class BossBeam : MonoBehaviour
{
    private Vector3 direction;
    public float speed = 20f;
    public int damage = 10;

    public LayerMask playerLayer;

    public void Fire(Vector3 target, int beamDamage)
    {
        damage = beamDamage;
        direction = (target - transform.position).normalized;

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) == 0)
            return;

        PlayerController pc = other.GetComponent<PlayerController>();

        if (pc != null)
        {
            pc.BossDamage(damage);
        }

        Destroy(gameObject);
    }

}
