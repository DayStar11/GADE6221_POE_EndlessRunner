using Unity.Mathematics;
using UnityEngine;

public class Background : MonoBehaviour
{

    public GameObject backgroundTile;
    Vector3 nextGenPoint;

    public void GenerateBackground()
    {
        GameObject temp = Instantiate(backgroundTile, nextGenPoint, quaternion.identity);
        nextGenPoint = temp.transform.GetChild(1).transform.position;
    }

    void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            GenerateBackground();
        }
    }


}
