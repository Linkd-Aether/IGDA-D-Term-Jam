using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lighting;

namespace Game.Gameplay 
{
    public class Door : LampMechanic
    {
        // Constants
        private static Color PROXIMITY_LAMP_COLOR = Color.red;

        // Variables
        [Header("Door Status")]
        public bool closed = true;

        [Header("Puzzle Type")] 
        public bool proximityDoorPuzzle;
        public float proximityDistance;

        // Components & References
        private Collider2D doorCollider;

        
        protected override void Awake() 
        {
            doorCollider = GetComponent<Collider2D>();
            base.Awake();

            SetProximityLamps(proximityDoorPuzzle, proximityDistance);
            if (proximityDoorPuzzle) {
                ChangeLampColors(PROXIMITY_LAMP_COLOR);
            }    
        }

        void Update()
        {
            if (closed && AllLampsLit()) OpenDoor();
        }

        #region Opening Functionality
            private void OpenDoor() {
                closed = false;
                doorCollider.enabled = false;

                GetComponent<SpriteRenderer>().color = Color.gray;
            }
        #endregion
    }
}

