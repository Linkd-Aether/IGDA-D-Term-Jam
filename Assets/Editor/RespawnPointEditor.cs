using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Game.Gameplay
{
    [CustomEditor(typeof(RespawnPoint))]
    public class RespawnPointEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RespawnPoint respawn = (RespawnPoint) target;

            DrawDefaultInspector();

            if(GUILayout.Button("Add Lamp")) respawn.AddLamp();
            if(GUILayout.Button("Remove Lamp")) respawn.RemoveLamp();
        }
    }
}