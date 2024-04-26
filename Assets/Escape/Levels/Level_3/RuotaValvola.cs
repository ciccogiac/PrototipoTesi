using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuotaValvola : OggettoEscapeChild
{

   [SerializeField] Color[] color;

    
    private void OnValidate()
    {
        //GetComponent<MeshRenderer>().material.color = color;
        OggettoEscape o = GetComponentInParent<OggettoEscape>();
        if (o != null)
        {
            string colore = o.oggettoEscapeValue.attributes.Find(x => x.attributeName == "Colore").attributeValue;
            MeshRenderer m = GetComponent<MeshRenderer>();
            if (m != null)
            {
                switch (colore)
                {
                    case "Rosso":
                        m.material.color = color[0];
                        break;

                    case "Giallo":
                        m.material.color = color[1];
                        break;

                    case "Blu":
                        m.material.color = color[2];
                        break;

                    default:
                        m.material.color = color[0];
                        break;

                }
            }
        }
    }

    public override void AssigneObject()
    {
        OggettoEscape o = GetComponentInParent<OggettoEscape>();
        if (o != null)
        {
            string colore = o.oggettoEscapeValue.attributes.Find(x => x.attributeName == "Colore").attributeValue;

            switch (colore)
            {
                case "Rosso":
                    GetComponent<MeshRenderer>().material.color = color[0];
                    break;

                case "Giallo":
                    GetComponent<MeshRenderer>().material.color = color[1];
                    break;

                case "Blu":
                    GetComponent<MeshRenderer>().material.color = color[2];
                    break;

                default:
                    GetComponent<MeshRenderer>().material.color = color[0];
                    break;

            }
        }
    }

    public (string,string) Ruota(char verso, int quantity)
    {
        //transform.eulerAngles = new Vector3(0f , transform.eulerAngles.y + 90f, 0f);
        if (verso == '+')
            //transform.Rotate(new Vector3(0f, 0f, transform.rotation.z - 90f));
            transform.Rotate(new Vector3(0f, transform.rotation.y - 90f * quantity, 0f));
        else if(verso == '-')
            //transform.Rotate(new Vector3(0f, 0f, transform.rotation.z + 90f));
            transform.Rotate(new Vector3(0f, transform.rotation.y + 90f * quantity, 0f));
        OggettoEscape o = GetComponentInParent<OggettoEscape>();
        string s = o.oggettoEscapeValue.attributes.Find(x => x.attributeName == "Valore").attributeValue;

        var i = int.Parse(s) + quantity;

        i %= 10;
        if (i < 0)
            i += 10;

        s = i.ToString();
        o.oggettoEscapeValue.attributes.Find(x => x.attributeName == "Valore").attributeValue = s;

        string t = o.oggettoEscapeValue.attributes.Find(x => x.attributeName == "Colore").attributeValue;

        return (s,t);
    }
}
