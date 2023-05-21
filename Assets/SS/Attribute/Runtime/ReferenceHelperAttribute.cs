using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    public class ReferenceHelperAttribute : PropertyAttribute
    {
        public System.Type filter;
        public string callbackMethodName;
        public ReferenceHelperAttribute(System.Type attrFilter, string callbackName)
        {
            filter = attrFilter;
            callbackMethodName = callbackName;
        }
        public ReferenceHelperAttribute()
        {

        }
    }

}
