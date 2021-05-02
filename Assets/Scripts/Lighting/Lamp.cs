using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;
using Game.Gameplay;
using Game.Utils;


namespace Game.Lighting 
{
    public class Lamp : Lightable
    {
        // Constants
        private static Color DEFAULT_LAMP_COLOR = Color.white;
        private static Color PROXIMITY_LAMP_COLOR = Color.red;
        private static Color RESPAWN_LAMP_COLOR = Color.blue;
        
        private enum LampType { NORMAL, PROXIMITY, RESPAWN };

        private static float DELIGHT_TIME = .5f;
        private static float PULSE_TIME = .25f;
        private static float PULSE_PROPORTION = .5f;

        // Variables
        [Tooltip("Set the radius from which the lamp can be lit.")]
        public float lightDistance = 5f;
        private bool fixedOn = false;

        private LampType lampType = LampType.NORMAL;

        // Components & References
        private static Lantern lantern;

        private LampTrigger lampTrigger;
        private CircleCollider2D trigger;


        protected override void Awake() {
            MAX_INTENSITY = 1f;
            MAX_OUTER_RADIUS = 5f;
            base.Awake();

            lantern = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lantern>();
            lampTrigger = GetComponentInChildren<LampTrigger>();
            trigger = GetComponent<CircleCollider2D>();
            trigger.radius = lightDistance;
        }

        private void OnDrawGizmos() {
            Awake();
        }

        #region Set Lamp Type
            public void SetNormalLamp() {
                lampType = LampType.NORMAL;
                SetLightColor(DEFAULT_LAMP_COLOR);
            }

            public void SetProximityLamp() {
                lampType = LampType.PROXIMITY;
                SetLightColor(PROXIMITY_LAMP_COLOR);
            }

            public void SetRespawnLamp() {
                lampType = LampType.RESPAWN;
                SetLightColor(RESPAWN_LAMP_COLOR);
            }
        #endregion

        #region Lamp Light
            private void OnTriggerEnter2D(Collider2D other) {
                if (other.gameObject.tag == "Player") {
                    lantern.LightableLampsUpdate(this, true);
                }
            }

            private void OnTriggerExit2D(Collider2D other) {
                if (other.gameObject.tag == "Player")
                {
                    lantern.LightableLampsUpdate(this, false);
                    if (lampType == LampType.PROXIMITY && !fixedOn && isLit)
                    {
                        isLit = false;
                        StartCoroutine(UtilFunctions.LerpCoroutine(LightSetting, 1, 0, DELIGHT_TIME));
                    }
                }
            }

            public override void LightOn()
            {
                if (lampType == LampType.PROXIMITY) {
                    StartCoroutine(PulseLight());
                    return;
                } else if (lampType == LampType.RESPAWN) {
                    RespawnPoint respawn = transform.parent.GetComponent<RespawnPoint>();
                    respawn.UpdateRespawn();
                }

                base.LightOn();
                if (lampTrigger != null) lampTrigger.OnLit();
            }

            public void SetFixed(bool state) {
                fixedOn = state;
            }

            private IEnumerator PulseLight() {
                isLit = true;
                yield return StartCoroutine(UtilFunctions.LerpCoroutine(LightSetting, 0, PULSE_PROPORTION, PULSE_TIME));
                if (fixedOn) {
                    yield return StartCoroutine(UtilFunctions.LerpCoroutine(LightSetting, PULSE_PROPORTION, 1, PULSE_TIME));
                } else {
                    isLit = false;
                    yield return StartCoroutine(UtilFunctions.LerpCoroutine(LightSetting, PULSE_PROPORTION, 0, PULSE_TIME));
                }
            }
        #endregion
    }
}
