using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lighting 
{
    public class Lamp : Lightable
    {
        // Constants
        private float DISTANCE_TO_LIGHT = 1.5f;
        
        // Components & References
        private static Lantern lantern;


        private void Awake() {
            lantern = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lantern>();
        }

        protected override void Start() {
            MAX_INTENSITY = 1f;
            MAX_RADIUS = 5f;
            base.Start();
        }

        private void Update()
        {
            if (Vector2.Distance(lantern.transform.position, this.transform.position) < DISTANCE_TO_LIGHT) {
                if (lantern.isLit) LightOn();
            }
        }
    }
}
