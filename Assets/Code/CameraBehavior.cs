using Unity.Mathematics;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    Vector3 _targetZoomPosition;

    Transform _cameraTransform;
    Spawner _spawner;
    
    const float padding = 2f;

    void Start()
    {
        _cameraTransform = GetComponent<Transform>();
        _spawner = FindAnyObjectByType<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawner == null)
        {
            return;
        }

        var view = _spawner.SpawnRange;
        float averageSize = math.max(view.x * 0.5f + padding, view.y * 0.5f + padding);
        _targetZoomPosition = new Vector3(0f, 0f, -averageSize);

        _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _targetZoomPosition, 0.5f);
    }

    private void ResetFocus()
    {
        
    }
}
