using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MyAttribute]
public class TestScriptableObject : ScriptableObject {

    [System.Serializable]
    public class MyClass
    {
        public string stringData;
    }
    public bool boolean;
    public int integer;
    public MyClass myClass;
}
