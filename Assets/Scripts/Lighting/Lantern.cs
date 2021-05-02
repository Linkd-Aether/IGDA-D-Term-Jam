using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental;
using UnityEngine;


namespace Game.Lighting 
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Lantern : Lightable
    {
        // Variables
        private List<Lamp> lightableLamps = new List<Lamp>();


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
                float outerRadius, innerRadius;
                if (isLit) {
                    outerRadius = MAX_OUTER_RADIUS;
                    innerRadius = MAX_INNER_RADIUS;
                } else {
                    outerRadius = MIN_OUTER_RADIUS;
                    innerRadius = MIN_INNER_RADIUS;
                }
                
                lightObj.intensity = currentIntensity * fraction;
                lightObj.pointLightOuterRadius = outerRadius * fraction;
                lightObj.pointLightInnerRadius = innerRadius * fraction;
            }
        #endregion

        #region Lamp Interaction
            public void LightableLampsUpdate(Lamp lamp, bool state) {
                if (state) {
                    if (!lightableLamps.Contains(lamp)) lightableLamps.Add(lamp);
                } else {
                    lightableLamps.Remove(lamp);
                }
            }

            public void LightableLampsLight() 
            {
                foreach (Lamp lamp in lightableLamps) {
                    if (!lamp.isLit)
                    {
                        Vector2 rayDirection = lamp.transform.position - transform.parent.position;
                        
                        RaycastHit2D hit2D = Physics2D.Raycast(transform.parent.position, rayDirection, lamp.lightDistance);
                        if (hit2D && hit2D.collider.gameObject == lamp.gameObject) {
                            lamp.LightOn();
                        }
                    }
                }
            }
        #endregion
    }
}
