using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttribute : PropertyAttribute {
    public delegate void CreateDelegate(System.Type t);

    public System.Type filter;
    public string callbackMethodName;
    public TestAttribute(System.Type attrFilter, string callbackName)
    {
        filter = attrFilter;
        callbackMethodName = callbackName;
    }

    public TestAttribute()
    {

    }
}
