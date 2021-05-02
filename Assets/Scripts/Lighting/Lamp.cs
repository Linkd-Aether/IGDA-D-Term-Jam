using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;


namespace Game.Lighting 
{
    public class Lamp : Lightable
    {
        // Variables
        private float lightDistance = 1.5f;
        private bool proximity = false;

        // Components & References
        private static Lantern lantern;

        private LampTrigger lampTrigger;


        protected override void Awake() {
            lantern = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lantern>();
            base.Awake();

            lampTrigger = GetComponentInChildren<LampTrigger>();
        }

        protected void Start() {
            MAX_INTENSITY = 1f;
            MAX_RADIUS = 5f;
        }

        private void Update()
        {
            if (Vector2.Distance(lantern.transform.position, this.transform.position) < lightDistance) {
                if (lantern.isLit) LightOn();
            } else if (isLit && proximity) {
                LightOff();
            }
        }

        private void OnDrawGizmos() {
            Awake();
            Gizmos.DrawWireSphere(transform.position, lightDistance);
        }

        public void SetProximity(bool state, float distance) {
            proximity = state;
            lightDistance = distance;
        }

        public override void LightOn()
        {
            base.LightOn();
            if (lampTrigger != null) lampTrigger.OnLit();
        }
    }
}
