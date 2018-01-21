using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Test))]
[CanEditMultipleObjects]
public class TestEditor : Editor {
    public override void OnInspectorGUI()
    {
        Test thisClass = (Test)target;
        if (GUILayout.Button("Call Editor"))
        {
        }
        base.OnInspectorGUI();
    }
}
