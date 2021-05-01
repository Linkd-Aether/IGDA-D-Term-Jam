using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Game.Gameplay
{
    [CustomEditor(typeof(Door))]
    public class DoorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Door door = (Door) target;

            DrawDefaultInspector();

            if(GUILayout.Button("Add Lamp")) door.AddLamp();
            if(GUILayout.Button("Remove Lamp")) door.RemoveLamp();
        }
    }
}

