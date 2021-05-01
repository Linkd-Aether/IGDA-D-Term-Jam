﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Control;
using Game.Lighting;
using Game.Utils;
using Game.AI;


namespace Game.Gameplay
{
    [RequireComponent(typeof(InputController)), RequireComponent(typeof(Collider2D))]
    public class Player : MonoBehaviour
    {
        // Constants
        private float DEATH_TIME = 2f;
        private float RESPAWN_WAIT = 2f;
        private float RESPAWN_TIME = 1f;

        // Variables
        private bool alive = true;

        // Components & References
        private InputController controller;
        private Collider2D playerCollider;
        private Lantern lantern;

        public RespawnPoint respawn;


        private void Awake() 
        {
            controller = GetComponent<InputController>();
            playerCollider = GetComponent<Collider2D>();
            lantern = GetComponentInChildren<Lantern>();

            if (respawn == null) respawn = RespawnPoint.GetInitRespawn();
        }
        
        private void Update() 
        {
            if (alive) controller.HandleInput();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.tag == "Enemy") {
                Transform enemy = collision.collider.transform;

                StartCoroutine(PlayerDeath());
                StartCoroutine(enemy.GetComponent<EnemyAI>().KilledPlayer(DEATH_TIME));
                enemy.up = (transform.position - enemy.position);
            }
        }        

        private IEnumerator PlayerDeath() 
        {
            controller.StopInput();
            alive = false;
            playerCollider.enabled = false;
            yield return StartCoroutine(UtilFunctions.LerpCoroutine(PlayerFade, 1, 0, DEATH_TIME));
            
            lantern.LightOff();
            yield return new WaitForSeconds(RESPAWN_WAIT);

            transform.position = respawn.transform.position;
            yield return StartCoroutine(UtilFunctions.LerpCoroutine(PlayerFade, 0, 1, RESPAWN_TIME));

            playerCollider.enabled = true;
            controller.StartInput();
            alive = true;    
        }

        private void PlayerFade(float lerpValue) {
            lantern.SetLightFraction(lerpValue);
            
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>()) {
                Color color = sprite.color;
                color.a = lerpValue;
                sprite.color = color;
            }
        }
    }
}