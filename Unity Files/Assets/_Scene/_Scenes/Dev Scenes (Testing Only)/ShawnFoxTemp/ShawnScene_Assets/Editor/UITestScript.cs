using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenericUIWindowScript))]
public class UITestScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GenericUIWindowScript myTarget = (GenericUIWindowScript)target;
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
