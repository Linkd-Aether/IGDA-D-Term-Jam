using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Game.Lighting;
using Game.Events;

namespace Game.Gameplay 
{
    [RequireComponent(typeof(DoorTrigger))]
    public class Door : LampMechanic
    {
        // Variables
        [Header("Door Status")]
        public bool closed = true;

        [Header("Puzzle Variables")]
        public bool proximityDoorPuzzle = false;
        private bool lampless = false;

        // Components & References
        private Collider2D doorCollider;
        private DoorTrigger trigger;

        
        protected override void Awake() 
        {
            doorCollider = GetComponent<Collider2D>();
            trigger = GetComponent<DoorTrigger>();

            base.Awake();

            if (proximityDoorPuzzle) SetProximityLamps();
            else SetNormalLamps();
            lampless = (lamps.Length == 0);
        }

        void Update()
        {
            if (!lampless && closed && AllLampsLit()) {
                OpenDoor();
                if (proximityDoorPuzzle) FixLampsOn();
            }
        }

        #region Opening Functionality
        private void OpenDoor()
        {
            closed = false;
            doorCollider.enabled = false;

            trigger.RunEvents(trigger.openEvents);

            GetComponent<SpriteRenderer>().color = Color.gray;
            GetComponent<ShadowCaster2D>().castsShadows = false;
        }

        private void CloseDoor()
        {
            closed = true;
            doorCollider.enabled = true;

            trigger.RunEvents(trigger.closeEvents);

            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<ShadowCaster2D>().castsShadows = true;
        }

        public void ChangeDoor(bool state)
        {
            if (state == closed) {
                if (state) OpenDoor();
                else CloseDoor();
            }
        }
        #endregion

        #region Configure Lamps
            private void SetProximityLamps() {
                foreach (Lamp lamp in lamps) {
                    lamp.SetProximityLamp();
                }
            }

            private void SetNormalLamps() {
                foreach (Lamp lamp in lamps) {
                    lamp.SetNormalLamp();
                }
            }
        #endregion
    }
}

