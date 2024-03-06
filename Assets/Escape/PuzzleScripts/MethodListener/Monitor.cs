using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : MethodListener
{
    [SerializeField] GameObject attributeValueMonitor_prefab;
    [SerializeField] GameObject attributeBox;

    [SerializeField] TextMeshProUGUI text_ClassName;
    [SerializeField] GameObject classBox;

    [SerializeField] Image monitorImage;
    [SerializeField] Color offColor;
    [SerializeField] Color onColor;

    [SerializeField] TextMeshProUGUI text_Error;


    public override void Getter(List<(string, string)> objectValue)
    {
        base.Getter(objectValue);

        foreach (Transform figlio in attributeBox.transform)
        {
            Destroy(figlio.gameObject);
        }

        classBox.SetActive(true);
        attributeBox.SetActive(true);

        monitorImage.color = onColor;
        text_ClassName.text = className;

        foreach (var attribute in objectAttributeValue)
        {
            GameObject oggettoIstanziato = Instantiate(attributeValueMonitor_prefab, attributeBox.transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponent<AttributeValueMonitor>().SetAttributeValueMonitor(attribute.Item1,attribute.Item2);
            oggettoIstanziato.transform.SetParent(attributeBox.transform);
            oggettoIstanziato.transform.rotation = new Quaternion(0f,0f,0f,0f);
            oggettoIstanziato.transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    public override void SetClass(string name)
    {
        base.SetClass(name);

        classBox.SetActive(true);
        attributeBox.SetActive(true);
        text_Error.gameObject.SetActive(false);

        monitorImage.color = onColor;
        text_ClassName.text = className;

    }

    public void SetError(string error)
    {
        foreach (Transform figlio in attributeBox.transform)
        {
            Destroy(figlio.gameObject);
        }

        classBox.SetActive(true);
        text_Error.gameObject.SetActive(true);
        text_Error.text = error;
    }

    public override void RemoveObject()
    {
        base.RemoveObject();

        foreach (Transform figlio in attributeBox.transform)
        {
            Destroy(figlio.gameObject);
        }

        monitorImage.color = offColor;
        text_ClassName.text = "";
        classBox.SetActive(false);
        attributeBox.SetActive(false);
    }
}
