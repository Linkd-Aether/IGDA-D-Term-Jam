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

        // Components & References
        protected Lamp[] lamps;


        protected virtual void Awake() 
        {
            lamps = new Lamp[transform.childCount];
            
            int childNum = 0;
            foreach (Transform child in transform) {
                Lamp lamp = child.GetComponent<Lamp>();
                lamps[childNum] = lamp;
                childNum++;
            }
        }
        
        #region Lamp Functions
            protected bool AllLampsLit() {
                foreach (Lamp lamp in lamps) {
                    if (!lamp.isLit) return false;
                }
                return true;
            }

            protected bool AnyLampsLit() {
                foreach (Lamp lamp in lamps) {
                    if (lamp.isLit) return true;
                }
                return false;
            }

            protected void UnlightAllLamps() {
                foreach (Lamp lamp in lamps) {
                    lamp.LightOff();
                }
            }
            
            protected void FixLampsOn() {
                foreach (Lamp lamp in lamps) {
                    lamp.SetFixed(true);
                }
            }
        #endregion

        #region Unity Editor Functionality
            private void OnDrawGizmos() {
                Awake();
                foreach (Lamp lamp in lamps) {
                    Gizmos.DrawLine(lamp.transform.position, transform.position);
                }
            }

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
