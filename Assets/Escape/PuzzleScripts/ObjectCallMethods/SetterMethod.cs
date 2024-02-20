using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetterMethod : MonoBehaviour
{
    [SerializeField] GameObject prefab_AttributeSetter;
    [SerializeField] GameObject box_AttribbuteSetter;
    [SerializeField] GameObject box_Caller;
    [SerializeField] ObjectCallMethods objectCallMethods;
   

    public void CreateAttributeSetter(string name, string value)
    {
            GameObject oggettoIstanziato = Instantiate(prefab_AttributeSetter, transform.position, Quaternion.identity);
            oggettoIstanziato.transform.SetParent(box_AttribbuteSetter.transform);
            oggettoIstanziato.transform.localScale = Vector3.one;
            oggettoIstanziato.GetComponent<AttributeInitializer>().SetAttributeValue(name,value);

    }

    public void ConfirmSetMethod()
    {
        foreach (Transform figlio in box_AttribbuteSetter.gameObject.transform)
        {

            if(objectCallMethods.objectInteraction.oggetto.oggettoEscapeValue.attributes.Find(x => x.attributeName == figlio.gameObject.GetComponent<AttributeInitializer>().GetAttributeName()) != null)
            {
                string inputString = figlio.gameObject.GetComponentInChildren<TMP_InputField>().text;
                if (inputString  != "")
                {

                    objectCallMethods.objectInteraction.oggetto.oggettoEscapeValue.attributes.RemoveAll(t => t.attributeName == figlio.gameObject.GetComponent<AttributeInitializer>().GetAttributeName());
                    Attribute nuovaTupla = new Attribute(figlio.gameObject.GetComponent<AttributeInitializer>().GetAttributeName(), inputString);
                    objectCallMethods.objectInteraction.oggetto.oggettoEscapeValue.attributes.Add(nuovaTupla);

                    objectCallMethods.objectInteraction.methodListener.ChangeTubeColor("Connected");
                }
            }


        }

        objectCallMethods.ReloadCallerCanvas();
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
        //objectCallMethods.ReloadCallerCanvas();
        box_Caller.SetActive(true);
        //objectCallMethods.CloseInterface();
    }
}
