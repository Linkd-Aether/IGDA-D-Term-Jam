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


        private void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            mover.UpdateMovement(input.normalized);


            if (Input.GetKeyDown(KeyCode.Q)) {
                lantern.ChangeLight();
            }
        }
    }
}
