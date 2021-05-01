using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental;
using UnityEngine;


namespace Game.Lighting 
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Lantern : Lightable
    {
        protected override void Start() {
            MIN_INTENSITY = .5f;
            base.Start();
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
                    currentRadius = MAX_RADIUS;
                } else {
                    currentIntensity = MIN_INTENSITY;
                    currentRadius = MIN_RADIUS;
                }
                
                lightObj.intensity = currentIntensity * fraction;
                lightObj.pointLightOuterRadius = currentRadius * fraction;
            }
        #endregion
    }
}
