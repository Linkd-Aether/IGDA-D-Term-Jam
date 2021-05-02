using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lighting;
using Game.Utils;


namespace Game.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class RespawnPoint : LampMechanic
    {
        // Constants
        private static Color RESPAWN_LAMP_COLOR = Color.blue;
        private static Color DEACTIVE_SPAWN_COLOR = new Color(0,0,0,50f/255f);
        private static Color ACTIVE_SPAWN_COLOR = new Color(100f/255f, 100f/255f, 1, 150f/255f);
        private static float COLOR_CHANGE_TIME = 0.5f;

        // Variables
        public bool initialSpawn = false;
        public bool hiddenSpawn = false;
        private bool isActive = false;

        // Components & References
        private static Player player;
        
        private SpriteRenderer spriteRenderer;


        protected override void Awake() 
        {
            base.Awake();

            if (!initialSpawn && lamps.Length == 0) {
                Debug.LogError("If a respawn position is not the initial spawn, it must have a lamp childed to it.");
            }

            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start() {
            ChangeLampColors(RESPAWN_LAMP_COLOR);    
        
            if (hiddenSpawn) spriteRenderer.color = new Color(1, 1, 1, 0);
            else SetRespawnColor(isActive);
            
            player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isActive && AnyLampsLit()) UpdateRespawn();
        }

        #region Respawn Functionality
            public static RespawnPoint GetInitRespawn() 
            {
                RespawnPoint respawn = null;
                int initRespawns = 0;

                foreach (RespawnPoint point in FindObjectsOfType<RespawnPoint>()) {
                    if (point.initialSpawn) {
                        respawn = point;
                        initRespawns++;
                    }
                }

                if (initRespawns == 0) {
                    Debug.LogWarning("No initial respawn point detected.");
                } else if (initRespawns > 1) {
                    Debug.LogWarning("More than one initial respawn point detected.");
                }

                return respawn;
            }

            private void UpdateRespawn() 
            {
                player.respawn.RemoveRespawn();
                isActive = true;
                player.respawn = this;
                SetRespawnColor(true);
            }

            public void RemoveRespawn() 
            {
                isActive = false;
                UnlightAllLamps();
                SetRespawnColor(false);
            }
        #endregion
    
        #region Color Change
            private void SetRespawnColor(bool state) {
                if (!hiddenSpawn) {
                    int[] values = {0, 1};
                    if (!state) Array.Reverse(values);

                    StartCoroutine(UtilFunctions.LerpCoroutine(ColorChange, values[0], values[1], COLOR_CHANGE_TIME));
                }
            }

            private void ColorChange(float lerpValue) {
                // lerpValue = 0 -> DEACTIVE_RESPAWN_COLOR
                // lerpValue = 1 -> ACTIVE_RESPAWN_COLOR

                Color color = DEACTIVE_SPAWN_COLOR;
                Color colorDiff = ACTIVE_SPAWN_COLOR - DEACTIVE_SPAWN_COLOR;
                color.r += colorDiff.r * lerpValue;
                color.g += colorDiff.g * lerpValue;
                color.b += colorDiff.b * lerpValue;
                color.a += colorDiff.a * lerpValue;

                spriteRenderer.color = color;
            }
        #endregion
    }
}
