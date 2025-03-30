using Unity.Entities;
using Unity.Mathematics;

public struct Hoverable : IComponentData
{
    public float3 Radius;
    public float3 Offset;
    public float3 Rate;

    public float3 Origin;

    public float3 CalculateHover(float t) => new float3 
        (
            Radius.x * math.sin(Rate.x * t + Offset.x),
            Radius.y * math.sin(Rate.y * t + Offset.y),
            Radius.z * math.sin(Rate.z * t + Offset.z)
        );
}
