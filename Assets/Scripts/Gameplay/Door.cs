using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lighting;

namespace Game.Gameplay 
{
    public class Door : LampMechanic
    {
        // Variables
        private bool closed = true;

        // Components & References
        private Collider2D doorCollider;

        
        protected override void Awake() 
        {
            doorCollider = GetComponent<Collider2D>();
            base.Awake();
        }

        // Update is called once per frame
        void Update()
        {
            if (closed && AllLampsLit()) OpenDoor();
        }

        #region Opening Functionality
            private void OpenDoor() {
                closed = false;
                doorCollider.enabled = false;

                GetComponent<SpriteRenderer>().color = Color.gray;
            }
        #endregion
    }
}

