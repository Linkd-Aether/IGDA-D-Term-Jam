using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Movement;

namespace Game.Control 
{
    [RequireComponent(typeof(Mover))]
    public class InputController : MonoBehaviour
    {
        private Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }


        private void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (input != Vector2.zero) mover.Move(input.normalized);
        }
    }
}
