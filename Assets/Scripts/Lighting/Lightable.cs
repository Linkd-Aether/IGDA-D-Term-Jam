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
        protected float MAX_RADIUS = 3f;
        protected float MIN_RADIUS = 1.5f;

        // Variables
        public bool isLit = false;
        
        // Components & References
        protected Light2D lightObj;


        protected virtual void Start()
        {
            lightObj = GetComponent<Light2D>();
            LightOff();
        }

        public virtual void LightOn() {
            isLit = true;
            lightObj.intensity = MAX_INTENSITY;
            lightObj.pointLightOuterRadius = MAX_RADIUS;
        }

        public virtual void LightOff() {
            isLit = false;
            lightObj.intensity = MIN_INTENSITY;
            lightObj.pointLightOuterRadius = MIN_RADIUS;
        }
    }
}
