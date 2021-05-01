using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


namespace Game.Lighting 
{
    [RequireComponent(typeof(Light2D))]
    public abstract class Lightable : MonoBehaviour
    {
        // Variables
        public bool isLit = false;
        
        // Components & References
        protected Light2D lightObj;


        private void Start()
        {
            lightObj = GetComponent<Light2D>();
        }

        public virtual void LightOn() {
            isLit = true;
        }

        public virtual void LightOff() {
            isLit = false;
        }
    }
}
