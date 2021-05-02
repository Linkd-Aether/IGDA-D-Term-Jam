using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Events
{
    public class TriggerStatesEvent : Event
    {
        // Variable
        public bool state = true;
        
        // Components & References
        public ColliderTrigger[] changeTriggers;


        public override void RunEvent() {
            foreach (ColliderTrigger trigger in changeTriggers) {
                trigger.gameObject.SetActive(state);
                trigger.inactiveTrigger = !state;
            }
        }
    }
}