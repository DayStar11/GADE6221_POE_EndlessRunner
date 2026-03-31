using UnityEngine;

public class Coin : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {
        //destroys coin once player has passed through it
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && player.transform.position.z > transform.position.z + 2f)
        {
            Destroy(gameObject);
        }
    }
}
