using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Lighting 
{
    public abstract class Lightable : MonoBehaviour
    {
        // Variables
        public bool isLit = false;
        

        public virtual void LightOn() {
            isLit = true;
        }

        public virtual void LightOff() {
            isLit = false;
        }
    }
}
