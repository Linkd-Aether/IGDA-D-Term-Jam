using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lighting;


namespace Game.Gameplay
{
    public class RespawnPoint : LampMechanic
    {
        // Constants
        private static Color RESPAWN_LAMP_COLOR = Color.blue;

        // Variables
        public bool initialSpawn = false;
        private bool isActive;

        // Components & References
        private static Player player;


        protected override void Awake() 
        {
            player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Player>();
            
            base.Awake();

            if (!initialSpawn && lamps.Length == 0) {
                Debug.LogError("If a respawn position is not the initial spawn, it must have a lamp childed to it.");
            }
        }

        private void Start() {
            ChangeLampColors(RESPAWN_LAMP_COLOR);    
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
            }

            public void RemoveRespawn() 
            {
                isActive = false;
                UnlightAllLamps();
            }
        #endregion
    }

}
