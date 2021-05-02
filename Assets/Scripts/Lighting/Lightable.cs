using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


namespace Game.Lighting 
{
    [RequireComponent(typeof(Light2D))]
    public abstract class Lightable : MonoBehaviour
    {
        // Constants
        protected float MAX_INTENSITY = 2f;
        protected float MIN_INTENSITY = 0.5f;
        protected float MAX_OUTER_RADIUS = 3f;
        protected float MIN_OUTER_RADIUS = 1.5f;
        protected float MAX_INNER_RADIUS = 0f;
        protected float MIN_INNER_RADIUS = 0f;

        // Variables
        public bool isLit = false;
        
        // Components & References
        protected Light2D lightObj;


        protected virtual void Awake()
        {
            lightObj = GetComponent<Light2D>();
        }

        public virtual void LightOn() {
            isLit = true;
            SetLightInstensity(MAX_INTENSITY);
            SetLightRadius(MAX_INNER_RADIUS, MAX_OUTER_RADIUS);
        }

        public virtual void LightOff() {
            isLit = false;
            SetLightInstensity(MIN_INTENSITY);
            SetLightRadius(MIN_INNER_RADIUS, MIN_OUTER_RADIUS);
        }

        protected virtual void SetLightColor(Color color) {
            if (lightObj == null) Awake();
            lightObj.color = color;
        }

        protected virtual void SetLightInstensity(float intensity) {
            // if (lightObj == null) Awake();
            lightObj.intensity = intensity;
        }

        protected virtual void SetLightRadius(float innerRadius, float outerRadius) {
            // if (lightObj == null) Awake();
            lightObj.pointLightInnerRadius = innerRadius;
            lightObj.pointLightOuterRadius = outerRadius;
        }
    }
}
