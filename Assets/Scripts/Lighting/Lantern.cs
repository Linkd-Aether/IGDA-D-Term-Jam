using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental;
using UnityEngine;


namespace Game.Lighting 
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Lantern : Lightable
    {
        protected override void Awake() {
            MIN_INTENSITY = .75f;
            MAX_OUTER_RADIUS = 4f;
            MIN_OUTER_RADIUS = 2f;
            MAX_INNER_RADIUS = 1f;
            MIN_INNER_RADIUS = 0f;
            base.Awake();
        }

        #region Lighting State Changes
            public void ChangeLight() {
                if (isLit) LightOff();
                else LightOn();
            }

            public void SetLightFraction(float fraction) {
                float currentIntensity, currentRadius;
                if (isLit) {
                    currentIntensity = MAX_INTENSITY;
                    currentRadius = MAX_OUTER_RADIUS;
                } else {
                    currentIntensity = MIN_INTENSITY;
                    currentRadius = MIN_OUTER_RADIUS;
                }
                
                lightObj.intensity = currentIntensity * fraction;
                lightObj.pointLightOuterRadius = currentRadius * fraction;
            }
        #endregion
    }
}
