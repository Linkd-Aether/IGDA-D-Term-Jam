using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay;


namespace Game.Events
{
    public class DoorEvent : Event
    {
        // Variables
        private bool triggered = false;

        public Door door;
        [Header("Mode")]
        [Tooltip("The door will close on trigger if set to false.")]
        public bool openDoor = true;


        public override void RunEvent() {
            if (!triggered) {
                triggered = true;
                door.ChangeDoor(openDoor);
            }
        }
    }
}
