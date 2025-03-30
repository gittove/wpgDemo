using Unity.Entities;
using Unity.Mathematics;

public struct SpawnerData : IComponentData
{
    public int SpawnedCount;
    
    public float3 SpawnRange;

    public Entity Prefab;
    
    public partial struct Finished : IComponentData {}
}

