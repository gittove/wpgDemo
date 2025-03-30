using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
partial struct HoveringSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Hoverable>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float time = Time.time;

        var hoverJob = new HoverJob() { Time = time };

        state.Dependency = hoverJob.ScheduleParallel(state.Dependency);
        //ecb.Playback(state.EntityManager); 
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public partial struct HoverJob : IJobEntity
    {
        public float Time;

        private void Execute(Entity entity, in Hoverable hoverable, ref LocalTransform ltw)
        {
            ltw.Position = hoverable.Origin + hoverable.CalculateHover(Time);
        }
    }
}
