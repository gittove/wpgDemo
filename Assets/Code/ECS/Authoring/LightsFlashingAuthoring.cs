using Unity.Entities;
using UnityEngine;

public class LightsFlashingAuthoring : MonoBehaviour
{
    [SerializeField] private Color aColor = Color.blue;
    [SerializeField] private Color bColor = Color.red;

    [SerializeField] private float rate = 1000f;

    public float Rate => rate;

    public Color ColorA => aColor;
    public Color ColorB => bColor;

    [BakingVersion("tove", 2)]
    private class LightsFlashingBaker : Baker<LightsFlashingAuthoring>
    {
        public override void Bake(LightsFlashingAuthoring authoring)
        {
            DependsOn(authoring);

            var lightComp = authoring.GetComponent<Light>();
            var entity = GetEntity(authoring, TransformUsageFlags.WorldSpace);


            AddComponent(entity, new LightFlasherComponent
            {
                //Light = lightComp,
                initialIntensity = lightComp.intensity,
                Rate = authoring.rate,
                ColorA = authoring.aColor,
                ColorB = authoring.bColor
            });
        }
    }
}
