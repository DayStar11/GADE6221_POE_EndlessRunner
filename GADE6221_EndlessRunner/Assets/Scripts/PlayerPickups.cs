using UnityEngine;

public class PlayerPickups : MonoBehaviour

{

    public enum PickUpType
    {
        CoinMagnet,
        SuperJump,
        Permeate,
        BossKiller,
        Shield
    }

    public PickUpType pickUpType;


    private void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && player.transform.position.z > transform.position.z + 2f)
        {
            Destroy(gameObject);
        }
    }

}