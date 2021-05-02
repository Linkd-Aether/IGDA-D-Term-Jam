using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Game.Events
{
    public abstract class Trigger : MonoBehaviour
    {
        // Components & References
        protected Event[] events;


        protected virtual void Start()
        {
            events = GetComponentsInChildren<Event>();    
        }

        public virtual void TriggerActivated() {
            foreach (Event scriptedEvent in events) {
                scriptedEvent.RunEvent();
            }
        }
    }
}