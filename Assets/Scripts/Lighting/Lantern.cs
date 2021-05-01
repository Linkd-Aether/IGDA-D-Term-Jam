using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental;
using UnityEngine;


namespace Game.Lighting 
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Lantern : Lightable
    {
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
