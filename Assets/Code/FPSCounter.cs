using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI FPSText;
    public TextMeshProUGUI SpawnedText;

    private float recalcTime = 1f;
    private float recalcTimestamp = 0f;

    void Update()
    {
        if ((Time.time - recalcTimestamp) < recalcTime)
        {
            return;
        }

        recalcTimestamp = Time.time;
        
        int spawnedAmount = Spawner.spawned;
        float fps = 1 / Time.unscaledDeltaTime;

        FPSText.text = $"FPS: {math.ceil(fps)}";
        SpawnedText.text = $"Spawned: {spawnedAmount}";
    }
}
