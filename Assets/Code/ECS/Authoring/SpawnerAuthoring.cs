using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public float3 SpawnRange;

    [BakingVersion("tove", 1)]
    private class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.None);
            AddComponent(entity, new SpawnerData 
            {
                SpawnRange = authoring.SpawnRange,
                SpawnedCount = 0
            });
        }
    }
}
