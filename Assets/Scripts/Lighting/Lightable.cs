using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Game.Utils;


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

        protected float LIGHT_ON_TIME = .25f;
        protected float LIGHT_OFF_TIME = .5f;

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
            StartCoroutine(UtilFunctions.LerpCoroutine(LightSetting, 0, 1, LIGHT_ON_TIME));
        }

        public virtual void LightOff() {
            isLit = false;
            StartCoroutine(UtilFunctions.LerpCoroutine(LightSetting, 1, 0, LIGHT_OFF_TIME));
        }

        protected virtual void SetLightColor(Color color) {
            if (lightObj == null) Awake();
            lightObj.color = color;
        }

        protected void SetLightInstensity(float intensity) {
            lightObj.intensity = intensity;
        }

        protected void SetLightRadius(float innerRadius, float outerRadius) {
            lightObj.pointLightInnerRadius = innerRadius;
            lightObj.pointLightOuterRadius = outerRadius;
        }

        protected void LightSetting(float percent) {
            float intensity = MIN_INTENSITY + (MAX_INTENSITY - MIN_INTENSITY) * percent;
            float innerRadius = MIN_INNER_RADIUS + (MAX_INNER_RADIUS - MIN_INNER_RADIUS) * percent;
            float outerRadius = MIN_OUTER_RADIUS + (MAX_OUTER_RADIUS - MIN_OUTER_RADIUS) * percent;

            SetLightInstensity(intensity);
            SetLightRadius(innerRadius, outerRadius);                
        }
    }
}
