using Unity.Entities;
using UnityEngine;

public class HoverableAuthoring : MonoBehaviour
{
    [BakingVersion("tove", 1)]
    private class HoverableBaker : Baker<HoverableAuthoring>
    {
        public override void Bake(HoverableAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            AddComponent(entity, new Hoverable()
            {
                Radius = new Unity.Mathematics.float3(1f,1f,1f),
                Rate = new Unity.Mathematics.float3(
                    UnityEngine.Random.Range(0f, 5f),
                    UnityEngine.Random.Range(0f, 3f),
                    UnityEngine.Random.Range(0f, 2f)),
                Offset = new Unity.Mathematics.float3(
                    UnityEngine.Random.Range(0f, 1f),
                    UnityEngine.Random.Range(0f, 1f),
                    UnityEngine.Random.Range(0f, 1f)),
            });
        }
    }
}
