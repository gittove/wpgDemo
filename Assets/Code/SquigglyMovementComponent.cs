using System;
using Unity.Mathematics;
using UnityEngine;

public class SquigglyMovementComponent : MonoBehaviour
{
    [SerializeField] private Vector3 _radius;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _rate;

    private Vector3 _origin;

    private Transform _transform;

    void Start()
    {
        _transform = this.transform;
        _origin = _transform.position;

        _rate = new Vector3(
            UnityEngine.Random.Range(0f, 5f),
            UnityEngine.Random.Range(0f, 3f),
            UnityEngine.Random.Range(0f, 2f));

        _radius = new Vector3(1,1,1);

        _offset = new Vector3(
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f));
    }

    void Update()
    {
        _transform.position = _origin + Hover();
    }

    private Vector3 Hover()
    {
        return new Vector3 
        (
            _radius.x * math.sin(_rate.x * Time.time + _offset.x),
            _radius.y * math.sin(_rate.y * Time.time + _offset.y),
            _radius.z * math.sin(_rate.z * Time.time + _offset.z)
        );
    }
}
