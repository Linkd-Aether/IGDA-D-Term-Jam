using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement 
{
    [RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Animator))]
    public class Mover : MonoBehaviour
    {
        // Variables
        [SerializeField] private float moveSpd = 7.5f;
        private bool moving;
        private Vector2 moveDirection = Vector2.zero;

        // Components & References
        private Rigidbody2D rb;
        private Animator animator;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate() 
        {
            Move();
        }

        private void Move() 
        {
            if (moving) {
                rb.MovePosition(rb.position + moveDirection * moveSpd * Time.fixedDeltaTime);
                transform.up = moveDirection;
            }
        }

        public void UpdateMovement(Vector2 direction) {
            moveDirection = direction;
            moving = (direction != Vector2.zero);
            animator.SetBool("Moving", moving);
        }
    }
}
