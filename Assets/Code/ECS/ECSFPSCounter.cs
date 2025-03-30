using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using TMPro;

public class ECSFPSCounter : MonoBehaviour
{
    public TextMeshProUGUI FPSText;
    public TextMeshProUGUI SpawnedText;

    private float recalcTime = 1f;
    private float recalcTimestamp = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - recalcTimestamp) < recalcTime)
        {
            return;
        }

        var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery query = EntityManager.CreateEntityQuery(new ComponentType[] { typeof(SpawnerData) });

        if (!query.TryGetSingleton<SpawnerData>(out var spawner))
        {
            return;
        }

        recalcTimestamp = Time.time;
        
        float fps = 1 / Time.unscaledDeltaTime;

        FPSText.text = $"FPS: {math.ceil(fps)}";
        SpawnedText.text = $"Spawned: {spawner.SpawnedCount}";
    }
}
