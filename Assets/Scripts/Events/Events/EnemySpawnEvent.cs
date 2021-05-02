using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Events 
{
    public class EnemySpawnEvent : Event
    {
        // Constants
        private float ANIM_LENGTH = 5f;

        // Variables
        private bool triggered = false;

        [Header("Cutscene Settings")]
        public float spawnDelay = 1f;
        
        // Components & References
        public EnemySpawner enemySpawner;
        

        public override void RunEvent() {
            if (!triggered) {
                triggered = true;
                StartCoroutine(EnemySpawnScene());
            }
        }

        private IEnumerator EnemySpawnScene() {
            yield return new WaitForSeconds(spawnDelay);
            enemySpawner.SpawnEnemy();
            yield return new WaitForSeconds(ANIM_LENGTH);
        }
    }
}
