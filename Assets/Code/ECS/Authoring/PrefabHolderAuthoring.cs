using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PrefabHolderAuthoring : MonoBehaviour
{
    public GameObject Prefab;

    [BakingVersion("tove", 1)]
    private class PrefabHolderBaker : Baker<PrefabHolderAuthoring>
    {
        public override void Bake(PrefabHolderAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.WorldSpace);
            AddComponent(entity, new PrefabHolder 
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            });
        }
    }
}
