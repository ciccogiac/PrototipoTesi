using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BlockLoader))]
public class BlockLoaderEditor : Editor
{
    SerializedProperty attributeprefabs;

    SerializedProperty attributeblockType;

    SerializedProperty attributeIntTargetProp;
    SerializedProperty attributeStringTargetProp;
    SerializedProperty attributeBoolTargetProp;




    void OnEnable()
    {
        attributeprefabs = serializedObject.FindProperty("prefabs");

        attributeblockType = serializedObject.FindProperty("blockType");

        attributeIntTargetProp = serializedObject.FindProperty("value_int");
        attributeStringTargetProp = serializedObject.FindProperty("value_char");
        attributeBoolTargetProp = serializedObject.FindProperty("value_bool");

        

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        EditorGUILayout.PropertyField(attributeprefabs);

        EditorGUILayout.PropertyField(attributeblockType);




        switch (attributeblockType.enumValueIndex)
        {
            case <= 2:
                break;

            case (<=5):
                EditorGUILayout.PropertyField(attributeIntTargetProp);
                break;
            case <=8:
                EditorGUILayout.PropertyField(attributeStringTargetProp);
                break;
            case <=11:
                EditorGUILayout.PropertyField(attributeBoolTargetProp);
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }
}
public class BlockLoader : MonoBehaviour
{
    public GameObject[] prefabs;

    public enum BlockType
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
        BoolRight,
        StartBlock,
        EndBlock,
        EmptyBlock,
        Wall

    }

    public BlockType blockType;

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
        GameObject prefabIstance= Instantiate(selectedPrefab, transform.position, Quaternion.identity, transform);

        if((int) blockType < 12) { prefabIstance.GetComponent<GridBlock>().isStationary = true; }

        switch ((int) blockType)
        {
            case <= 2:
                break;

            case (<= 5):
                prefabIstance.GetComponent<IntBlock>().SetIntValue (value_int);
                break;
            case <= 8:
                prefabIstance.GetComponent<CharBlock>().SetCharValue(value_char);
                break;
            case <= 11:
                prefabIstance.GetComponent<BoolBlock>().SetBoolValue(value_bool);
                break;
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


