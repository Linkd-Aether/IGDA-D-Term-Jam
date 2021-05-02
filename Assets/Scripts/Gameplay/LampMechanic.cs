using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lighting;

namespace Game.Gameplay 
{
    public abstract class LampMechanic : MonoBehaviour
    {
        // Constants
        private static string LAMP_PREFAB = "Prefabs/Interactables/Lamp";
        private static Color DEFAULT_LAMP_COLOR = Color.white;

        // Components & References
        protected Transform[] lamps;


        protected virtual void Awake() 
        {
            lamps = new Transform[transform.childCount];
            
            int childNum = 0;
            foreach (Transform child in transform) {
                lamps[childNum] = child;
                childNum++;
            }
            // ChangeLampColors(DEFAULT_LAMP_COLOR);
        }
        
        private void OnDrawGizmos() {
            Awake();
            foreach (Transform lamp in lamps) {
                Gizmos.DrawLine(lamp.position, transform.position);
            }
        }

        #region Lamp Functions
            protected bool AllLampsLit() {
                foreach (Transform lamp in lamps) {
                    Lamp lampRef = lamp.GetComponent<Lamp>();
                    if (!lampRef.isLit) return false;
                }
                return true;
            }

            protected bool AnyLampsLit() {
                foreach (Transform lamp in lamps) {
                    Lamp lampRef = lamp.GetComponent<Lamp>();
                    if (lampRef.isLit) return true;
                }
                return false;
            }

            protected void UnlightAllLamps() {
                foreach (Transform lamp in lamps) {
                    Lamp lampRef = lamp.GetComponent<Lamp>();
                    lampRef.LightOff();
                }
            }

            protected void ChangeLampColors(Color color) {
                foreach (Transform lamp in lamps) {
                    Lamp lampRef = lamp.GetComponent<Lamp>();
                    lampRef.SetLightColor(color);
                }
            }

            protected void SetProximityLamps(bool state, float distance) {
                foreach (Transform lamp in lamps) {
                    Lamp lampRef = lamp.GetComponent<Lamp>();
                    lampRef.SetProximity(state, distance);
                }
            }
        #endregion

        #region Unity Editor Functionality
            public void AddLamp()
            {
                Awake();
                GameObject lamp = Instantiate(Resources.Load<GameObject>(LAMP_PREFAB), transform.position, Quaternion.identity);
                lamp.name = $"Lamp {lamps.Length}";
                lamp.transform.parent = this.transform;
            }

            public void RemoveLamp()
            {
                if (transform.childCount > 0) {
                    DestroyImmediate(transform.GetChild(transform.childCount - 1).gameObject);
                }
            }
        #endregion
    }
}
