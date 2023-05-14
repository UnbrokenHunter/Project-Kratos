using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ProjectKratos
{

    [Serializable, VolumeComponentMenuForRenderPipeline("Custom/Ben Day Bloom", typeof(UniversalRenderPipeline))]
    public class BenDayBloomEffectComponent : VolumeComponent, IPostProcessComponent 
    {
        
        [Header("Settings")]
        public FloatParameter threshold = new FloatParameter(0.9f, true);
        
        public FloatParameter intensity = new FloatParameter(1f, true);
        
        public ClampedFloatParameter scatter = new ClampedFloatParameter(0.7f, 0f, 1f, true);

        public IntParameter clamp = new IntParameter(65472, true);

        public ClampedFloatParameter maxIterations = new ClampedFloatParameter(6f, 0f, 10f);

        public NoInterpColorParameter tintColor = new NoInterpColorParameter(Color.white);
        
        
        [Header("BenDay")]
        public IntParameter dotsDensity = new IntParameter(10, true);
        
        public ClampedFloatParameter dotsCutoff = new ClampedFloatParameter(0.4f, 0f, 1f, true);
        
        public Vector2Parameter scrollDirection = new Vector2Parameter(new Vector2());

        public bool IsActive()
        {
            return true;
        }

        public bool IsTileCompatible()
        {
            return false;
        }
    }
}
