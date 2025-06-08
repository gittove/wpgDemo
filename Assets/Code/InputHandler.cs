using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    void Update()
    {
        var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery query = EntityManager.CreateEntityQuery(new ComponentType[] { typeof(SpawnerRange) });

        if (!query.TryGetSingleton<SpawnerRange>(out var spawner) || !query.TryGetSingletonEntity<SpawnerRange>(out var entity))
        {
            return;
        }

        if (Input.GetKey("1"))
        {
            spawner.SpawnRange = new float3(500f, 500f, 0f);
            EntityManager.SetComponentData(entity, spawner);
            EntityManager.RemoveComponent<SpawnerData.Finished>(entity);
        }
        else if (Input.GetKey("2"))
        {
            spawner.SpawnRange = new float3(1000f, 1000f, 0f);
            EntityManager.SetComponentData(entity, spawner);
            EntityManager.RemoveComponent<SpawnerData.Finished>(entity);
        }
        else if (Input.GetKey("3"))
        {
            spawner.SpawnRange = new float3(1500f, 1500f, 0f);
            EntityManager.SetComponentData(entity, spawner);
            EntityManager.RemoveComponent<SpawnerData.Finished>(entity);
        }

    }
}
