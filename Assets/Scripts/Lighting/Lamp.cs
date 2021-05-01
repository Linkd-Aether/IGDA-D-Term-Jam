using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Lighting 
{
    public class Lamp : Lightable
    {
        // Components & References
        private SpriteRenderer spriteRenderer;

        
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        #region Lighting State Changes
            public void ChangeLight() {
                if (isLit) LightOff();
                else LightOn();
            }

            public override void LightOn() {
                base.LightOn();
                spriteRenderer.color = Color.yellow;
            }

            public override void LightOff() {
                base.LightOff();
                spriteRenderer.color = Color.black;
            }
        #endregion
    }
}
