using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

[CustomPropertyDrawer(typeof(TestAttribute))]
public class TestAttributeDrawer : PropertyDrawer {
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        TestAttribute attr = attribute as TestAttribute;

        float h = EditorGUI.GetPropertyHeight(property, label, false);

        // button
        h += EditorGUIUtility.singleLineHeight;

        UnityEngine.Object obj = property.objectReferenceValue;
        if (obj != null)
        {

            SerializedObject so = new SerializedObject(obj);
            if (so != null)
            {
                SerializedProperty sp = so.GetIterator();

                if (sp.NextVisible(true))
                {
                    do
                    {
                        h += EditorGUI.GetPropertyHeight(sp, true);
                    } while (sp.NextVisible(false));
                }
            }
        }

        return h;
    }
    static float padding = EditorGUIUtility.singleLineHeight;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        TestAttribute attr = attribute as TestAttribute;
        GUI.Box(position, "");

        float y = position.y;
        Rect baseRect = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
        y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(baseRect, property, false);

        Rect buttonRect = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
        y += EditorGUIUtility.singleLineHeight;
        if (!string.IsNullOrEmpty(attr.callbackMethodName))
        {
            if (GUI.Button(buttonRect, attr.callbackMethodName))
            {
                Type t = property.serializedObject.targetObject.GetType();
                if (t.IsSubclassOf(typeof(UnityEngine.Object)))
                {
                    TestWindow.CreateWindow(attr.filter, t, property, attr.callbackMethodName, fieldInfo);
                }
            }
        }
        else
        {
            if (GUI.Button(buttonRect, "Create"))
            {
                Type t = property.serializedObject.targetObject.GetType();
                TestWindow.CreateWindow(attr.filter, t, property, attr.callbackMethodName, fieldInfo);
            }
        }

        UnityEngine.Object obj = property.objectReferenceValue;
        if (obj != null)
        {


            position.x += padding;
            position.width -= padding;

            SerializedObject so = new SerializedObject(obj);
            if (so != null)
            {
                so.Update();
                SerializedProperty sp = so.GetIterator();
                
                if (sp.NextVisible(true))
                {
                    do
                    {
                        float h = EditorGUI.GetPropertyHeight(sp, true);
                        Rect currRect = new Rect(position.x, y, position.width, h);
                        y += h;
                        EditorGUI.PropertyField(currRect, sp, true);
                    } while (sp.NextVisible(false));
                }

                so.ApplyModifiedProperties();
            }
        }
    }
}
