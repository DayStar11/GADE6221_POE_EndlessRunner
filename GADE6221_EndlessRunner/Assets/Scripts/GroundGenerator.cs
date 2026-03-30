using Unity.Mathematics;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextGenPoint;

    public void GenerateTile()
    {
        GameObject temp = Instantiate(groundTile, nextGenPoint, quaternion.identity);
        nextGenPoint = temp.transform.GetChild(1).transform.position;
    }

    void Start()
    {
        for (int i = 0; i < 5; i++) 
        {
            GenerateTile();
        }
    }


}
