using Unity.Entities;
using UnityEngine;

public struct LightFlasherComponent : IComponentData
{
    public float Rate;

    public float initialIntensity;

    //public Light Light;

    public Color ColorA;
    public Color ColorB; 
}
