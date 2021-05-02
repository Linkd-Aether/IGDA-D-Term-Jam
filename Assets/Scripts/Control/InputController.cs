using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Movement;
using Game.Lighting;

namespace Game.Control 
{
    [RequireComponent(typeof(Mover))]
    public class InputController : MonoBehaviour
    {
        private Mover mover;
        private Lantern lantern;


        private void Start()
        {
            mover = GetComponent<Mover>();
            lantern = GetComponentInChildren<Lantern>();
        }

        public void HandleInput()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            mover.UpdateMovement(input.normalized);

            if (Input.GetKeyDown(KeyCode.Q)) {
                lantern.ChangeLight();
            } else if (lantern.isLit && Input.GetKeyDown(KeyCode.E)) {
                lantern.LightableLampsLight();
            }
        }

        public void StartInput() 
        {
            mover.UpdateAnimator(true);
        }

        public void StopInput() 
        {
            mover.UpdateAnimator(false);
            mover.UpdateMovement(Vector2.zero);
        }
    }
}
