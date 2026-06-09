using Unity.Mathematics;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    //daiyaan:
    public GameObject groundTile;
    Vector3 nextGenPoint;

    private float tileLength = 20f;

    public void GenerateTile()
    {
        GameObject temp = Instantiate(groundTile, nextGenPoint, Quaternion.identity);

       
        if (nextGenPoint.z < 40f) // removes obstacles for the first few tiles to create a safe starting point for players Kylin
        {
            NewMonoBehaviourScript spawner = temp.GetComponent<NewMonoBehaviourScript>(); //gets the new monobehavior script that sppawns the obsticles and destroys them so the first tiles wont kill players Kylin
            if (spawner != null)
            {
                Destroy(spawner);
            }
        }

        nextGenPoint += Vector3.forward * tileLength;
    }

    void Start()
    {
        for (int i = 0; i < 7; i++)   // gives player more starting room
        {
            GenerateTile();
        }
    }

}

