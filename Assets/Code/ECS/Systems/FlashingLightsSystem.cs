using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class FlashingLightsSystem : SystemBase
{
    protected override void OnCreate()
    {
        //RequireForUpdate<Light>();
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
    }

    protected override void OnUpdate()
    {
        double time = SystemAPI.Time.ElapsedTime;

        // since i'm reading/writing from the managed type Light, i have to run this foreach using .WithoutBurst().Run();
        // aka, no scheduling allowed!

        Entities.ForEach(
            (Light light, in LightFlasherComponent comp) =>
        {
            float a = (float)math.sin(comp.Rate * time);
            light.intensity = comp.initialIntensity * math.abs(a); // = Color.Lerp(comp.ColorA, comp.ColorB, math.abs(a));
            light.color = Color.Lerp(comp.ColorA, comp.ColorB, math.abs(a));
        }).WithoutBurst().Run();
    }

    protected override void OnStopRunning()
    {
        base.OnStopRunning();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
