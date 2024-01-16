using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ClassValue
{
    public string className;
    public List<AttributeValue> attributes;

}

[Serializable]
public class AttributeValue
{
    public string attribute;
    public bool visibility;
    public List<Method> methods;

}

[Serializable]
public class Method
{
    public enum MethodType
    {
        getter,
        setter,
        interactor,
        pickUp,
        destroy

    }

    public string methodName;
    public MethodType methodType;

    public Method(string method, MethodType type)
    {
        this.methodName = method;
        this.methodType = type;
    }
}
