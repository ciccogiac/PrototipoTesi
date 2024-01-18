using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetterMethod : MonoBehaviour
{
    [SerializeField] GameObject prefab_AttributeSetter;
    [SerializeField] GameObject box_AttribbuteSetter;
    [SerializeField] ObjectCallMethods objectCallMethods;
   

    public void CreateAttributeSetter(string name, string value)
    {
            GameObject oggettoIstanziato = Instantiate(prefab_AttributeSetter, transform.position, Quaternion.identity);
            oggettoIstanziato.transform.SetParent(box_AttribbuteSetter.transform);
            oggettoIstanziato.GetComponent<AttributeInitializer>().SetAttributeValue(name,value);

    }

    public void ConfirmSetMethod()
    {
        foreach (Transform figlio in box_AttribbuteSetter.gameObject.transform)
        {

            if(objectCallMethods.objectInteraction.oggetto.attributes.Find(x => x.Item1 == figlio.gameObject.GetComponent<AttributeInitializer>().GetAttributeName()) != (null,null))
            {
                string inputString = figlio.gameObject.GetComponentInChildren<TMP_InputField>().text;
                if (inputString  != "")
                {

                    objectCallMethods.objectInteraction.oggetto.attributes.RemoveAll(t => t.Item1 == figlio.gameObject.GetComponent<AttributeInitializer>().GetAttributeName());
                    (string, string) nuovaTupla = (figlio.gameObject.GetComponent<AttributeInitializer>().GetAttributeName(), inputString);
                    objectCallMethods.objectInteraction.oggetto.attributes.Add(nuovaTupla);
                }
            }


        }


        CloseInterface();
    }

    public void CloseInterface()
    {
        foreach (Transform figlio in box_AttribbuteSetter.gameObject.transform)
        {
            // Elimina il figlio corrente
            Destroy(figlio.gameObject);
        }

        gameObject.SetActive(false);
        objectCallMethods.CloseInterface();
    }
}
