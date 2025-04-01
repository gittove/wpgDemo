using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Vector3 SpawnRange;
    public GameObject Prefab;

    public static int spawned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int distance = 5;
        spawned = 0;

        for(float i = -(SpawnRange.x * 0.5f); i < SpawnRange.x * 0.5f; i += distance)
        {
            for(float k = -(SpawnRange.y * 0.5f); k < SpawnRange.y * 0.5f; k += distance)
            {
                Instantiate(Prefab, new Vector3((float)i, (float)k, 0f), quaternion.identity);
                spawned++;
            }
        }
    }
}
