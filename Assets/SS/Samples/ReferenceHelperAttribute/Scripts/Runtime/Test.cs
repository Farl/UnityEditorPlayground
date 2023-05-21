using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Test : MonoBehaviour {

    public string id;

    // Monobehaviour filter with nothing and create with callback
    [ReferenceHelper(null, "createCallback2")]
    public MonoBehaviour mb;

    // ScriptableObject filter with custom attribute and create with callback
    [ReferenceHelper(typeof(MyAttribute), "createCallback")]
    public ScriptableObject so;


    // Scriptable Object filter with custom attribute and create Without callback
    [ReferenceHelper(typeof(MyAttribute), null)]
    public ScriptableObject soWithoutCallback;

    public bool b;

#if UNITY_EDITOR
    public void createCallback(System.Type t)
    {
        Debug.Log("createCallback");
    }
    public void createCallback2(System.Type t)
    {
        Debug.Log("createCallback2");
    }


    [ContextMenu("Clear")]
    public void Clear()
    {
        ScriptableObject.DestroyImmediate(so);
    }
#endif
}
