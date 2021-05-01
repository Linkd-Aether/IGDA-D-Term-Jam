using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Events 
{
    public class EnemySpawnEvent : Event
    {
        // Variables
        [Header("Cutscene Settings")]
        public float cameraMoveToSpd;
        public float cameraMoveReturnSpd;
        public float spawnDelay;
        
        // Components & References
        public EnemySpawner enemySpawner;
        
        
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public override void RunEvent() {

        }
    }
}
