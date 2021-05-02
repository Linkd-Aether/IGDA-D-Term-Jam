using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
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

        [Header("Puzzle Variables")] 
        public bool lampless = false;
        public bool proximityDoorPuzzle = false;
        public float lightingDistance = 1.5f;

        // Components & References
        private Collider2D doorCollider;

        
        protected override void Awake() 
        {
            doorCollider = GetComponent<Collider2D>();
            base.Awake();

            SetProximityLamps(proximityDoorPuzzle, lightingDistance);
            if (lightingDistance == 0) Debug.LogWarning("Make sure Lighting Distance isn't set to 0!");
            if (proximityDoorPuzzle) {
                ChangeLampColors(PROXIMITY_LAMP_COLOR);
            }    
        }

        void Update()
        {
            if (!lampless && closed && AllLampsLit()) {
                OpenDoor();
            }
        }

        #region Opening Functionality
        private void OpenDoor()
        {
            closed = false;
            doorCollider.enabled = false;

            GetComponent<SpriteRenderer>().color = Color.gray;
            GetComponent<ShadowCaster2D>().castsShadows = false;
        }

        private void CloseDoor()
        {
            closed = true;
            doorCollider.enabled = true;

            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<ShadowCaster2D>().castsShadows = true;
        }

        public void ChangeDoor(bool state)
        {
            if (state) OpenDoor();
            else CloseDoor();
        }
        #endregion
    }
}

