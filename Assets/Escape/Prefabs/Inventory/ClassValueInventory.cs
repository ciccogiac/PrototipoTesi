using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClassValueInventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI attributeName;
    [SerializeField] TextMeshProUGUI attributeVisibility;
    [SerializeField] GameObject methodsGrid;

    [SerializeField] GameObject methodPrefab;


    public void SetClassValue(string name,bool visibility , List<Method> methods)
    {
        attributeName.text = name;
        attributeVisibility.text = visibility.ToString();


        foreach (var m in methods)
        {
            GameObject oggettoIstanziato = Instantiate(methodPrefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = m.methodName;
            oggettoIstanziato.transform.SetParent(methodsGrid.transform);
        }

    }
}
