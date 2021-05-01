using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Events
{
    public class Trigger : MonoBehaviour
    {
        // Components & References
        private Event[] events;


        private void Start()
        {
            events = GetComponentsInChildren<Event>();    
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Player") {
                foreach (Event scriptedEvent in events) {
                    scriptedEvent.RunEvent();
                }
            }
        }
    }
}
