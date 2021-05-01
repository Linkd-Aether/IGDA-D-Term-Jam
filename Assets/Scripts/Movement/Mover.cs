using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement 
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField] private float moveSpd = 7.5f;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction) 
        {
            // rb.AddForce(moveSpd * direction);
            // transform.rotation = Quaternion.LookRotation(direction);
            rb.MovePosition(rb.position + direction * moveSpd * Time.fixedDeltaTime);
        }
    }
}
