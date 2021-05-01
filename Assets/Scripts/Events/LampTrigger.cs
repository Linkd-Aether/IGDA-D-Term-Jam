using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Events
{
    public class LampTrigger : Trigger
    {
        private bool triggered = false;

        public void OnLit() {
            if (!triggered) {
                triggered = true;
                TriggerActivated();
            }
        }
    }
}
