using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Test : MonoBehaviour {

    [Button("Test Button", "test")]
    public string id;

    [Test(null, "createCallback2")]
    public MonoBehaviour mb;
    [Test(typeof(MyAttribute), "createCallback")]
    public ScriptableObject so;

    public bool b;

#if UNITY_EDITOR
    public void test()
    {
        Debug.Log("test");
    }
    public void createCallback(System.Type t)
    {
        if (t != null)
        {
            //so = ScriptableObject.CreateInstance(t.ToString());
        }
    }
    public void createCallback2(System.Type t)
    {
        if (t != null)
        {
            mb = (MonoBehaviour)gameObject.AddComponent(t);
        }
    }


    [ContextMenu("Clear")]
    public void Clear()
    {
        ScriptableObject.DestroyImmediate(so);
    }
#endif

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
