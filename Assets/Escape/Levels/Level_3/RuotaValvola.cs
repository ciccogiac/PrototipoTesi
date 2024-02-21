using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuotaValvola : MonoBehaviour
{

   [SerializeField] Color color;

    private void OnValidate()
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
    public string Ruota()
    {
        //transform.eulerAngles = new Vector3(0f , transform.eulerAngles.y + 90f, 0f);
        transform.Rotate(new Vector3(0f, 0f, transform.rotation.z + 90f));

        OggettoEscape o = GetComponentInParent<OggettoEscape>();
        string s = o.oggettoEscapeValue.attributes.Find(x => x.attributeName == "Valore").attributeValue;
        int i = int.Parse(s) + 1;
        if (i == 10)
            i = 0;
        s = i.ToString();
        o.oggettoEscapeValue.attributes.Find(x => x.attributeName == "Valore").attributeValue = s;

        return s;
    }
}
