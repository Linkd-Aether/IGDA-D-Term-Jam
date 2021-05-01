using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Control;
using Game.Lighting;
using Game.Utils;
using Game.Movement;


namespace Game.Gameplay
{
    [RequireComponent(typeof(InputController)), RequireComponent(typeof(Collider2D))]
    public class Player : MonoBehaviour
    {
        // Constants
        private float DEATH_TIME = 2f;

        // Variables
        private bool alive = true;

        // Components & References
        private InputController controller;
        private Collider2D playerCollider;
        private Lantern lantern;


        private void Awake() 
        {
            controller = GetComponent<InputController>();
            playerCollider = GetComponent<Collider2D>();
            lantern = GetComponentInChildren<Lantern>();
        }
        
        private void Update() 
        {
            if (alive) controller.HandleInput();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.tag == "Enemy") {
                PlayerDeath();
            }
        }        

        private void PlayerDeath() 
        {
            controller.StopInput();

            alive = false;
            playerCollider.enabled = false;
            
            StartCoroutine(UtilFunctions.LerpCoroutine(DeathFade, 1, 0, DEATH_TIME));
        }

        private void DeathFade(float lerpValue) {
            lantern.SetLightFraction(lerpValue);
            
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>()) {
                Color color = sprite.color;
                color.a = lerpValue;
                sprite.color = color;
            }
        }
    }
}
