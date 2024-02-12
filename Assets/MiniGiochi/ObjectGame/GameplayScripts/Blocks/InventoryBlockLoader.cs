using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(InventoryBlockLoader))]
public class InventoryBlockLoaderEditor : Editor
{
    SerializedProperty attributeprefabs;

    SerializedProperty attributeblockType;
    
    SerializedProperty attributeblockQuantity;

    SerializedProperty attributeIntTargetProp;
    SerializedProperty attributeStringTargetProp;
    SerializedProperty attributeBoolTargetProp;




    void OnEnable()
    {
        attributeprefabs = serializedObject.FindProperty("prefabs");

        attributeblockType = serializedObject.FindProperty("blockType");
        attributeblockQuantity = serializedObject.FindProperty("blockQuantity");

        attributeIntTargetProp = serializedObject.FindProperty("value_int");
        attributeStringTargetProp = serializedObject.FindProperty("value_char");
        attributeBoolTargetProp = serializedObject.FindProperty("value_bool");



    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        EditorGUILayout.PropertyField(attributeprefabs);

        EditorGUILayout.PropertyField(attributeblockType);

        EditorGUILayout.PropertyField(attributeblockQuantity);




        switch (attributeblockType.enumValueIndex)
        {
            case <= 2:
                break;

            case (<= 5):
                EditorGUILayout.PropertyField(attributeIntTargetProp);
                break;
            case <= 8:
                EditorGUILayout.PropertyField(attributeStringTargetProp);
                break;
            case <= 11:
                EditorGUILayout.PropertyField(attributeBoolTargetProp);
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }
}

#endif

public class InventoryBlockLoader : MonoBehaviour
{
    public GameObject[] prefabs;

    public enum InventoryBlockType
    {
        Forward,
        Left,
        Right,
        IntForward,
        IntLeft,
        IntRight,
        CharForward,
        CharLeft,
        CharRight,
        BoolForward,
        BoolLeft,
        BoolRight

    }

    public InventoryBlockType blockType;

    public int blockQuantity;

    public int value_int;
    public char value_char;
    public bool value_bool;


    private System.Collections.IEnumerator DestroyChildrenNextFrame()
    {
        // Attendere il frame successivo
        yield return null;

        // Distruggere tutti i figli
        DestroyAllChildren();

        GameObject selectedPrefab = prefabs[(int)blockType];
        GameObject prefabIstance = Instantiate(selectedPrefab, transform.position, Quaternion.identity, transform);

        InventorySelection inventorySelection = prefabIstance.GetComponentInChildren<InventorySelection>();
        if (inventorySelection != null)
        {
            inventorySelection.itemCount = blockQuantity;

            switch ((int)blockType)
            {
                case <= 2:
                    break;

                case (<= 5):
                    inventorySelection.intValue = value_int;
                    break;
                case <= 8:
                    inventorySelection.charValue = value_char;
                    break;
                case <= 11:
                    inventorySelection.boolValue = value_bool;
                    break;
            }

            inventorySelection.UpdateItemValue();
        }

    }

    private void DestroyAllChildren()
    {
        // Loop inverso per evitare problemi di indice dinamico
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(DestroyChildrenNextFrame());
            }

        }
#endif

    }
}
