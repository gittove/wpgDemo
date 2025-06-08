using Unity.Entities;
using Unity.Mathematics;

public struct SpawnerData : IComponentData
{
    public int SpawnedCount;


    public partial struct Finished : IComponentData { }
}

public struct SpawnerRange : IComponentData
{
    public float3 SpawnRange;
}

