using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
[UpdateAfter(typeof(CleanupSystem))]
partial struct SpawningSystem : ISystem
{
    public EntityQuery spawnerDataQuery;

    public void OnCreate(ref SystemState state)
    {
        spawnerDataQuery = state.GetEntityQuery(ComponentType.ReadWrite<SpawnerData>(), ComponentType.Exclude<SpawnerData.Finished>());
        state.RequireForUpdate(spawnerDataQuery);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var singleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        EntityCommandBuffer ecb = singleton.CreateCommandBuffer(state.WorldUnmanaged);

        if (!spawnerDataQuery.TryGetSingletonEntity<SpawnerData>(out var e))
        {
            return;
        }

        if (!SystemAPI.TryGetSingleton<PrefabHolder>(out PrefabHolder prefabHolder))
        {
           return;
        }

        var spawnJob = new SpawnJob()
        {
            entity = e,
            PrefabHolder = prefabHolder,
            SpawnerDataLookup = SystemAPI.GetComponentLookup<SpawnerData>(isReadOnly: true),
            SpawnerRangeLookup = SystemAPI.GetComponentLookup<SpawnerRange>(isReadOnly: true),
            HoverableLookup = SystemAPI.GetComponentLookup<Hoverable>(),
            Ecb = ecb
        };

        state.Dependency = spawnJob.Schedule(state.Dependency);
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    [WithChangeFilter(typeof(SpawnerData))]
    public partial struct SpawnJob : IJob
    {
        public Entity entity;

        public PrefabHolder PrefabHolder;

        [ReadOnly] public ComponentLookup<SpawnerData> SpawnerDataLookup;

        [ReadOnly] public ComponentLookup<SpawnerRange> SpawnerRangeLookup;

        public ComponentLookup<Hoverable> HoverableLookup;

        public EntityCommandBuffer Ecb;

        public void Execute()
        {
            var spawnerData = SpawnerDataLookup[entity];
            var SpawnerRange = SpawnerRangeLookup[entity];
            Random mathRandom = new Random(100);

            float distance = 5f;
            int spawnCount = 0;
            for (float i = -(SpawnerRange.SpawnRange.x * 0.5f); i < SpawnerRange.SpawnRange.x * 0.5f; i += distance)
            {
                for (float k = -(SpawnerRange.SpawnRange.y * 0.5f); k < SpawnerRange.SpawnRange.y * 0.5f; k += distance)
                {
                    var e = Ecb.Instantiate(PrefabHolder.Prefab);
                    var hoverable = HoverableLookup[PrefabHolder.Prefab];
                    hoverable.Radius = new float3(1f, 1f, 1f);
                    hoverable.Rate = new float3(mathRandom.NextFloat(0f, 5f), mathRandom.NextFloat(0f, 3f), mathRandom.NextFloat(0f, 2f));
                    hoverable.Offset = new float3(mathRandom.NextFloat(0f, 1f), mathRandom.NextFloat(0f, 1f), mathRandom.NextFloat(0f, 1f));
                    hoverable.Origin = new float3(i, k, 0f);
                    Ecb.SetComponent(e, hoverable);
                    spawnCount++;
                }
            }

            spawnerData.SpawnedCount = spawnCount;
            Ecb.SetComponent(entity, spawnerData);
            Ecb.AddComponent<SpawnerData.Finished>(entity);
        }
    }
}
