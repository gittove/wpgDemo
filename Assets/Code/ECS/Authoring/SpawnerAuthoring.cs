using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public float3 SpawnRange;
    public GameObject Prefab;

    [BakingVersion("tove", 1)]
    private class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.None);
            AddComponent(entity, new SpawnerData 
            {
                SpawnRange = authoring.SpawnRange,
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnedCount = 0
            });
        }
    }
}
