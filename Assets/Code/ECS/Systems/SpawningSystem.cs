using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

[BurstCompile]
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

        //if (!SystemAPI.TryGetSingleton<PrefabHolder>(out PrefabHolder prefabHolder))
        //{
        //    return;
        //}

        var spawnJob = new SpawnJob()
        {
            entity = e,
            //PrefabHolder = prefabHolder,
            SpawnerDataLookup = SystemAPI.GetComponentLookup<SpawnerData>(),
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
    public partial struct SpawnJob : IJob
    {
        public Entity entity;

        //public PrefabHolder PrefabHolder;

        public ComponentLookup<SpawnerData> SpawnerDataLookup;

        public ComponentLookup<Hoverable> HoverableLookup;

        public EntityCommandBuffer Ecb;

        public void Execute()
        {
            var spawnerData = SpawnerDataLookup[entity];
            Random mathRandom = new Random(100);

            float distance = 5f;
            int spawnCount = 0;
            for(float i = -(spawnerData.SpawnRange.x * 0.5f); i < spawnerData.SpawnRange.x * 0.5f; i += distance)
            {
                for(float k = -(spawnerData.SpawnRange.y * 0.5f); k < spawnerData.SpawnRange.y * 0.5f; k += distance)
                {
                    var e = Ecb.Instantiate(spawnerData.Prefab);
                    var hoverable = HoverableLookup[spawnerData.Prefab];
                    hoverable.Radius = new float3(1f,1f,1f);
                    hoverable.Rate = new float3(mathRandom.NextFloat(0f,5f), mathRandom.NextFloat(0f,3f), mathRandom.NextFloat(0f,2f));
                    hoverable.Offset =  new float3(mathRandom.NextFloat(0f,1f), mathRandom.NextFloat(0f,1f), mathRandom.NextFloat(0f,1f));
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
