using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ECSCameraBehavior : MonoBehaviour
{
    Vector3 _targetZoomPosition;

    Transform _cameraTransform;
    
    const float padding = 2f;

    void Start()
    {
        _cameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery query = EntityManager.CreateEntityQuery(new ComponentType[] { typeof(SpawnerData) });

        if (!query.TryGetSingleton<SpawnerData>(out var spawner))
        {
            return;
        }

        var view = spawner.SpawnRange;
        float averageSize = math.max(view.x * 0.5f + padding, view.y * 0.5f + padding);
        _targetZoomPosition = new Vector3(0f, 0f, -averageSize);

        _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _targetZoomPosition, 0.5f);
    }
}
