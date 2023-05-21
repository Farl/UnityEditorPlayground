using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS;

[MyAttribute]
public class TestScriptableObjectList: ScriptableObject {
    [ReferenceHelper]
    public List<ScriptableObject> list = new List<ScriptableObject>();
}
