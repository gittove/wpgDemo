using Unity.Mathematics;
using Unity.Entities;
using UnityEngine;

public class HoverableAuthoring : MonoBehaviour
{
    public float3 HoverRadius;

    [BakingVersion("tove", 1)]
    private class HoverableBaker : Baker<HoverableAuthoring>
    {
        public override void Bake(HoverableAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            AddComponent(entity, new Hoverable()
            {
                Radius = authoring.HoverRadius,
                Rate = 0f,
                Offset = 0f,
            });
        }
    }
}
