using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

namespace SS
{


    [CustomPropertyDrawer(typeof(ReferenceHelperAttribute))]
    public class ReferenceHelperAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ReferenceHelperAttribute attr = attribute as ReferenceHelperAttribute;

            float h = EditorGUI.GetPropertyHeight(property, label, false);

            // button
            h += EditorGUIUtility.singleLineHeight;

            if (property.isExpanded)
            {

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
            }

            return h;
        }
        static float padding = EditorGUIUtility.singleLineHeight;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReferenceHelperAttribute attr = attribute as ReferenceHelperAttribute;
            
            // Draw background
            GUI.Box(position, "");

            // Init rect
            float y = position.y;

            // Draw fold out
            float foldoutWidth = EditorGUIUtility.singleLineHeight;
            Rect foldoutRect = new Rect(position.x, y, foldoutWidth, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, new GUIContent(""));

            // Draw original property
            Rect baseRect = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(baseRect, property, label, false);
            y += EditorGUIUtility.singleLineHeight;

            // Tool bar
            Rect buttonRect = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
            y += EditorGUIUtility.singleLineHeight;

            
            if (!string.IsNullOrEmpty(attr.callbackMethodName))
            {
                if (GUI.Button(buttonRect, attr.callbackMethodName))
                {
                    Type t = property.serializedObject.targetObject.GetType();
                    if (t.IsSubclassOf(typeof(UnityEngine.Object)))
                    {
                        ReferenceHelperWindow.CreateWindow(attr.filter, t, property, attr.callbackMethodName, fieldInfo);
                    }
                }
            }
            else
            {
                if (GUI.Button(buttonRect, "Create"))
                {
                    Type t = property.serializedObject.targetObject.GetType();
                    ReferenceHelperWindow.CreateWindow(attr.filter, t, property, attr.callbackMethodName, fieldInfo);
                }
            }

            if (property.isExpanded)
            {
                UnityEngine.Object obj = property.objectReferenceValue;
                if (obj != null)
                {
                    position.x += padding;
                    position.width -= padding;

                    SerializedObject so = new SerializedObject(obj);
                    if (so != null)
                    {
                        var isScriptProperty = typeof(SerializedProperty).GetProperty("isScript", BindingFlags.NonPublic | BindingFlags.Instance);

                        so.Update();
                        SerializedProperty sp = so.GetIterator();

                        if (sp.NextVisible(true))
                        {
                            do
                            {
                                float h = EditorGUI.GetPropertyHeight(sp, true);
                                Rect currRect = new Rect(position.x, y, position.width, h);
                                y += h;
                                // if sp.isScript == true (use reflection to get private field)
                                var isScript = (isScriptProperty != null)? (bool)isScriptProperty.GetValue(sp) : false;
                                if (isScript)
                                {
                                    GUI.enabled = false;
                                }

                                EditorGUI.PropertyField(currRect, sp, true);
                                if (isScript)
                                {
                                    GUI.enabled = true;
                                }
                            } while (sp.NextVisible(false));
                        }

                        so.ApplyModifiedProperties();
                    }
                }
            }
        }
    }

}