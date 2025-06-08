using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
partial struct CleanupSystem : ISystem
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

        var cleanupJob = new CleanupJob()
        {
            Ecb = ecb
        };

        state.Dependency = cleanupJob.Schedule(state.Dependency); 
    } 

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public partial struct CleanupJob : IJobEntity
    {
        public EntityCommandBuffer Ecb;

        public void Execute(Entity entity, Hoverable hoverable)
        {
            Ecb.DestroyEntity(entity);
        }
    }
}
