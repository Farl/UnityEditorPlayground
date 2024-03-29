﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

namespace SS
{
    public class ReferenceHelperWindow : EditorWindow
    {

        public static Type type;
        public static Type attributeFilter;
        public static UnityEngine.Object obj;
        public static string methodName;
        public static SerializedProperty property;
        public static System.Reflection.FieldInfo fieldInfo;
        public static void CreateWindow(Type attribute, Type t, SerializedProperty prop, string method, System.Reflection.FieldInfo field)
        {
            ReferenceHelperWindow tw = EditorWindow.GetWindow<ReferenceHelperWindow>("test");
            type = t;
            attributeFilter = attribute;
            property = prop;
            obj = prop.serializedObject.targetObject;
            methodName = method;
            fieldInfo = field;
            Search();
        }

        static Vector2 scrollVec = Vector2.zero;
        static List<Type> typeList = new List<Type>();

        static void Search()
        {

            typeList.Clear();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in assembly.GetTypes())
                {
                    if (t.IsSubclassOf(fieldInfo.FieldType))
                    {
                        object[] attributeArray = (attributeFilter != null) ? t.GetCustomAttributes(attributeFilter, false) : t.GetCustomAttributes(false);

                        if (attributeArray != null && attributeArray.Length > 0)
                        {
                            foreach (var a in attributeArray)
                            {
                                if (attributeFilter == null || a.GetType() == attributeFilter)
                                {
                                    typeList.Add(t);
                                }
                            }
                        }
                        else if (attributeFilter == null)
                        {
                            typeList.Add(t);
                        }
                    }
                }
            }
        }
        void OnGUI()
        {
            scrollVec = EditorGUILayout.BeginScrollView(scrollVec);
            foreach (Type t in typeList)
            {
                if (GUILayout.Button(t.ToString()))
                {
                    if (obj != null)
                    {
                        if (property.propertyType == SerializedPropertyType.ObjectReference)
                        {
                            if (fieldInfo.FieldType.Equals(typeof(MonoBehaviour)) || fieldInfo.FieldType.IsSubclassOf(typeof(MonoBehaviour)))
                            {
                                var objs = property.serializedObject.targetObjects;
                                foreach (var o in objs)
                                {
                                    property.objectReferenceValue = (o as MonoBehaviour).gameObject.AddComponent(t);
                                }
                            }
                            else if (fieldInfo.FieldType.Equals(typeof(ScriptableObject)) || fieldInfo.FieldType.IsSubclassOf(typeof(ScriptableObject)))
                            {
                                //fieldInfo.SetValue(obj, ScriptableObject.CreateInstance(t));
                                property.objectReferenceValue = ScriptableObject.CreateInstance(t);
                            }
                        }

                        if (!string.IsNullOrEmpty(methodName))
                        {
                            Type objType = obj.GetType();

                            System.Reflection.MethodInfo mi = objType.GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                            if (mi != null)
                            {
                                try
                                {
                                    mi.Invoke(obj, new object[] { t });
                                }
                                catch (Exception)
                                {

                                }
                                property.serializedObject.ApplyModifiedProperties();
                                //EditorUtility.SetDirty(obj);
                            }
                        }
                    }
                    EditorGUILayout.EndScrollView();
                    this.Close();
                    return;
                }
            }
            EditorGUILayout.EndScrollView();
        }

        public static List<Type> GetSubTypes<T>() where T : class
        {
            var types = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.StartsWith("Mono.Cecil"))
                    continue;

                if (assembly.FullName.StartsWith("UnityScript"))
                    continue;

                if (assembly.FullName.StartsWith("Boo.Lan"))
                    continue;

                if (assembly.FullName.StartsWith("System"))
                    continue;

                if (assembly.FullName.StartsWith("I18N"))
                    continue;

                if (assembly.FullName.StartsWith("UnityEngine"))
                    continue;

                if (assembly.FullName.StartsWith("UnityEditor"))
                    continue;

                if (assembly.FullName.StartsWith("mscorlib"))
                    continue;

                foreach (Type type in assembly.GetTypes())
                {
                    if (!type.IsClass)
                        continue;

                    if (type.IsAbstract)
                        continue;

                    if (!type.IsSubclassOf(typeof(T)))
                        continue;

                    types.Add(type);
                }
            }

            return types;
        }
    }
}