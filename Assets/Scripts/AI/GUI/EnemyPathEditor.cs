using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Game.AI
{
    [CustomEditor(typeof(EnemyPath))]
    public class LevelScriptEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            EnemyPath enemyPath = (EnemyPath) target;

            DrawDefaultInspector();

            if(GUILayout.Button("Add Path Node")) enemyPath.AddNode();
            if(GUILayout.Button("Remove Path Node")) enemyPath.RemoveNode();
        }
    }
}

