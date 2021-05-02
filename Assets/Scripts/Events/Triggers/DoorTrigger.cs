using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Events
{
    public class DoorTrigger : MonoBehaviour
    {
        // Components & References
        [Tooltip("Events to run on DoorOpen")]
        public Event[] openEvents;
        [Tooltip("Events to run on DoorClose")]
        public Event[] closeEvents;


        public void RunEvents(Event[] events) {
            foreach (Event scriptedEvent in events) {
                scriptedEvent.RunEvent();
            }
        }
    }
}