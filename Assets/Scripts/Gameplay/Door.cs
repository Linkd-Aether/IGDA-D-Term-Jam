using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lighting;

namespace Game.Gameplay 
{
    public class Door : MonoBehaviour
    {
        // Constants
        private static string LAMP_PREFAB = "Prefabs/Interactables/Lamp";

        // Variables
        private bool closed = true;

        // Components & References
        private Collider2D doorCollider;

        private Transform[] lamps;

        
        private void Awake() 
        {
            doorCollider = GetComponent<Collider2D>();
            lamps = new Transform[transform.childCount];
            
            int childNum = 0;
            foreach (Transform child in transform) {
                lamps[childNum] = child;
                childNum++;
            }
        }

        private void OnDrawGizmos() {
            Awake();
            foreach (Transform lamp in lamps) {
                Gizmos.DrawLine(lamp.position, transform.position);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (closed && AllLampsLit()) OpenDoor();
        }

        #region Opening Functionality
        private bool AllLampsLit() {
            foreach (Transform lamp in lamps) {
                Lamp lampRef = lamp.GetComponent<Lamp>();
                if (!lampRef.isLit) return false;
            }
            return true;
        }

        private void OpenDoor() {
            closed = false;
            doorCollider.enabled = false;

            GetComponent<SpriteRenderer>().color = Color.gray;
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

