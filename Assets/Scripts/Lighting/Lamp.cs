using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Game.Lighting 
{
    public class Lamp : Lightable
    {
        // Components & References
        private Light2D lightObj;

        private void Start()
        {
            lightObj = GetComponent<Light2D>();
        }

        #region Lighting State Changes
            public void ChangeLight() {
                if (isLit) LightOff();
                else LightOn();
            }

            public override void LightOn() {
                base.LightOn();
                lightObj.intensity = 3;
            }

            public override void LightOff() {
                base.LightOff();
                lightObj.intensity = 0;
            }
        #endregion

    }
}
