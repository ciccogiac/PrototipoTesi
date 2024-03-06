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

    public (string,string) Ruota(char verso)
    {
        //transform.eulerAngles = new Vector3(0f , transform.eulerAngles.y + 90f, 0f);
        if (verso == '+')
            //transform.Rotate(new Vector3(0f, 0f, transform.rotation.z - 90f));
            transform.Rotate(new Vector3(0f, transform.rotation.y - 90f, 0f));
        else if(verso == '-')
            //transform.Rotate(new Vector3(0f, 0f, transform.rotation.z + 90f));
            transform.Rotate(new Vector3(0f, transform.rotation.y + 90f, 0f));

        OggettoEscape o = GetComponentInParent<OggettoEscape>();
        string s = o.oggettoEscapeValue.attributes.Find(x => x.attributeName == "Valore").attributeValue;

        int i=0;
        if (verso == '+')
             i = int.Parse(s) + 1;
        else if (verso == '-')
             i = int.Parse(s) - 1;

        if (i == 10)
            i = 0;
        if (i == -1)
            i = 9;

        s = i.ToString();
        o.oggettoEscapeValue.attributes.Find(x => x.attributeName == "Valore").attributeValue = s;

        string t = o.oggettoEscapeValue.attributes.Find(x => x.attributeName == "Colore").attributeValue;

        return (s,t);
    }
}
