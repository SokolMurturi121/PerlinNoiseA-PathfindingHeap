using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGen))]
public class MapGenEditor : Editor {

    public override void OnInspectorGUI()
    {
        MapGen mapGenerator = (MapGen)target;
        if (DrawDefaultInspector()){
            if (mapGenerator.autoUpdate)
            {
                mapGenerator.DrawMapInEditor();
            }
        }
        if (GUILayout.Button("Generate"))
        {
            mapGenerator.DrawMapInEditor();
        }    
        //base.OnInspectorGUI();
    }

}
