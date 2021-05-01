using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Events
{
    public class ColliderTrigger : Trigger
    {
        // Variables
        public bool inactiveTrigger;
        
        
        protected override void Start()
        {
            base.Start();
            if (inactiveTrigger) this.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Player") {
                TriggerActivated();
            }
        }
    }
}
