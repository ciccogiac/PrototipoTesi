using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AttributeGrid))]
public class AttributeGridEditor : Editor
{
    SerializedProperty attributeTypeProp;
    SerializedProperty attributeIntTargetProp;
    SerializedProperty attributeBoolTargetProp;
    SerializedProperty attributeStringTargetProp;

    SerializedProperty attributeNameProp;


    void OnEnable()
    {
        attributeTypeProp = serializedObject.FindProperty("attributeType");
        attributeIntTargetProp = serializedObject.FindProperty("AttributeIntTarget");
        attributeBoolTargetProp = serializedObject.FindProperty("AttributeBoolTarget");
        attributeStringTargetProp = serializedObject.FindProperty("AttributeStringTarget");

        attributeNameProp = serializedObject.FindProperty("attributeName");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(attributeNameProp);


        EditorGUILayout.PropertyField(attributeTypeProp);


        switch ((GameManager_ObjectGame.AttributeType)attributeTypeProp.enumValueIndex)
        {
            case GameManager_ObjectGame.AttributeType.intero:
                EditorGUILayout.PropertyField(attributeIntTargetProp);
                break;
            case GameManager_ObjectGame.AttributeType.booleano:
                EditorGUILayout.PropertyField(attributeBoolTargetProp);
                break;
            case GameManager_ObjectGame.AttributeType.stringa:
                EditorGUILayout.PropertyField(attributeStringTargetProp);
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }
}

public class AttributeGrid : MonoBehaviour
{
    public string attributeName;
    public GameManager_ObjectGame.AttributeType attributeType;


    public int AttributeIntTarget = 0;
    public bool AttributeBoolTarget = false;
    public string AttributeStringTarget = "";

}
