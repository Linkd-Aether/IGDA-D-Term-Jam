using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(GridShadowCastersGenerator))]
public class GridShadowCastersGeneratorEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate")) {
            var generator = (GridShadowCastersGenerator)target;

            Undo.RecordObject(generator.shadowCastersContainer, "GridShadowCastersGenerator.generate"); // this does not work :(

            var casters = generator.Generate();

            // as a hack to make the editor save the shadowcaster instances, we rename them now instead of when theyre generated.

            Undo.RecordObjects(casters, "GridShadowCastersGenerator name prefab instances");

            for (var i = 0; i < casters.Length; i++) {
                casters[i].name += "_" + i.ToString();
            }
        }
    }
}
