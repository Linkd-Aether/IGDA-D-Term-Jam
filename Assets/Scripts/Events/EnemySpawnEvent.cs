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
        private EnemySpawner enemySpawner;
        
        
        private void Start()
        {
            enemySpawner = GetComponentInChildren<EnemySpawner>();
        }

        public override void RunEvent() {
            StartCoroutine(EnemySpawnScene());
        }

        private IEnumerator EnemySpawnScene() {
            yield return null;
            enemySpawner.SpawnEnemy();
            yield return null;
        }
    }
}
