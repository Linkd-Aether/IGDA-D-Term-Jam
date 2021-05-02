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
        protected float MIN_INTENSITY = 0f;
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
            lightObj.intensity = MAX_INTENSITY;
            lightObj.pointLightOuterRadius = MAX_OUTER_RADIUS;
            lightObj.pointLightInnerRadius = MAX_INNER_RADIUS;
        }

        public virtual void LightOff() {
            isLit = false;
            lightObj.intensity = MIN_INTENSITY;
            lightObj.pointLightOuterRadius = MIN_OUTER_RADIUS;
            lightObj.pointLightInnerRadius = MIN_INNER_RADIUS;
        }

        public virtual void SetLightColor(Color color) {
            if (lightObj == null) Awake();
            lightObj.color = color;
        }
    }
}
