using Unity.Entities;
using UnityEngine;

public struct PrefabHolder : IComponentData
{
    public Entity Prefab;
}
