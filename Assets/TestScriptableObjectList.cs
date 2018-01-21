using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MyAttribute]
public class TestScriptableObjectList: ScriptableObject {
    [Test]
    public List<ScriptableObject> list = new List<ScriptableObject>();
}
