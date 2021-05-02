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
        private static AudioClip TURN_ON_SOUND;
        private static AudioClip TURN_OFF_SOUND;

        private List<Lamp> lightableLamps = new List<Lamp>();

        // Components & References
        private AudioSource audioSource;


        protected override void Awake() {
            MIN_INTENSITY = .75f;
            MAX_OUTER_RADIUS = 4f;
            MIN_OUTER_RADIUS = 2f;
            MAX_INNER_RADIUS = 1f;
            MIN_INNER_RADIUS = 0f;
            base.Awake();

            audioSource = GetComponent<AudioSource>();

            TURN_ON_SOUND = Resources.Load<AudioClip>("Audio/SFX/Light_Lanturn");
            TURN_OFF_SOUND = Resources.Load<AudioClip>("Audio/SFX/Extinguish Lanturn");
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

            public override void LightOn() {
                audioSource.PlayOneShot(TURN_ON_SOUND);
                base.LightOn();
            }

            public override void LightOff() {
                audioSource.PlayOneShot(TURN_OFF_SOUND);
                base.LightOff();
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
                        if (CheckViewToLamp(lamp)) lamp.LightOn();
                    }
                }
            }

            public bool CheckViewToLamp(Lamp lamp) {
                // Clear View
                Vector2 rayDirectionToPivot = lamp.transform.position - transform.parent.position;
                RaycastHit2D hit2D = Physics2D.Raycast(transform.parent.position, rayDirectionToPivot, lamp.lightDistance);
                if (hit2D && hit2D.collider.gameObject == lamp.gameObject) {
                    return true;    
                }

                // More Angled View
                BoxCollider2D boxCollider2D = lamp.GetComponent<BoxCollider2D>();

                // Vertical Scanning
                float lampHeight = boxCollider2D.size.y;
                Vector2 top = (Vector2) lamp.transform.position + (Vector2.up * lampHeight/2);
                float interpolation = 0;
                while (interpolation < 1) {
                    Vector2 rayDir = (top - (Vector2.up * lampHeight * interpolation)) - (Vector2) transform.parent.position;
                    hit2D = Physics2D.Raycast(transform.parent.position, rayDir, lamp.lightDistance);

                    if (hit2D && hit2D.collider.gameObject == lamp.gameObject) {
                        return true;
                    }
                    interpolation += .25f;
                }

                // Horizontal Scanning
                float lampWidth = boxCollider2D.size.x;
                Vector2 right = (Vector2) lamp.transform.position + (Vector2.up * lampWidth/2);
                interpolation = 0;
                while (interpolation < 1) {
                    Vector2 rayDir = (right - (Vector2.up * lampWidth * interpolation)) - (Vector2) transform.parent.position;
                    hit2D = Physics2D.Raycast(transform.parent.position, rayDir, lamp.lightDistance);

                    if (hit2D && hit2D.collider.gameObject == lamp.gameObject) {
                        return true;
                    }
                    interpolation += .25f;
                }


                return false;
            }
        #endregion
    }
}
