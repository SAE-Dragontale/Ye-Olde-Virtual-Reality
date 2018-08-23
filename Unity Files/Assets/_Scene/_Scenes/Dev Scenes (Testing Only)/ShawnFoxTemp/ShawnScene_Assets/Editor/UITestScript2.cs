using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CatapultPowerUI))]
public class UITestScript2 : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CatapultPowerUI myTarget = (CatapultPowerUI)target;
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Show"))
            {
                myTarget.Show();
            }

            if (GUILayout.Button("Hide"))
            {
                myTarget.Hide();
            }
        }
        else
        {
            if (GUILayout.Button("Enable play mode"))
            {
                
            }

            if (GUILayout.Button("Enable play mode"))
            {
                
            }
        }
    }
}
