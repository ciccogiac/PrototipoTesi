using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OggettoEscape : Interactable
{
   
    public MethodListener methodListener;

   


    private InventoryLoad inventoryLoad;

    public ObjectCallMethods ObjectCallCanvas;

    public OggettoEscapeValue oggettoEscapeValue;


    private void OnValidate()
    {
        GetComponent<Clue>().clueName = oggettoEscapeValue.objectName;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryLoad = FindObjectOfType<InventoryLoad>();
        if (oggettoEscapeValue.mesh == null)
        {
            oggettoEscapeValue.mesh = GetComponent<MeshFilter>().mesh;
            oggettoEscapeValue.material = GetComponent<MeshRenderer>().materials;
        }

        if (!oggettoEscapeValue.isMadeByPrinter)
        {
            //SetOggettoEscapeNotPrinted();
        }



    }

    public void SetOggettoEscapeValue(OggettoEscapeValue oggetto)
    {
        oggettoEscapeValue = oggetto;
        SetOggettoEscapeNotPrinted();
    }

    public void SetOggettoEscapeNotPrinted()
    {
        Methos m = new Methos(new Method("PickUpObject", Method.MethodType.pickUp), new List<string> { });
        if(oggettoEscapeValue.methods.Find(x => x.method.methodType == Method.MethodType.pickUp) == null)
            oggettoEscapeValue.methods.Add(m); 
       
    }


    override public void Interact()
    {
        if (isActive)
        {
            gameObject.SetActive(false);
            isActive = false;

            //Inventario.istanza.PickUpObject(CopyObject());
            Inventario.istanza.PickUpObject(oggettoEscapeValue);
            Destroy(gameObject);
        }
    }

    public void CallMethod(string m)
    {
        //(Method, List<string>) method = oggettoEscapeValue.methods.Find(x => x.Item1.methodName == m);
        Methos method = oggettoEscapeValue.methods.Find(x => x.method.methodName == m);
        Method.MethodType methodType = method.method.methodType;
        List<string> attributi = method.attributes;
        switch (methodType)
        {
            case Method.MethodType.getter:
                List<(string, string)> attributesWithValue = new List<(string, string)>();
                foreach (var attributo in attributi)
                {
                    (string, string) tupla = (attributo, oggettoEscapeValue.attributes.Find(x => x.attributeName == attributo).attributeValue);
                    attributesWithValue.Add(tupla);
                }
                methodListener.Getter(attributesWithValue);
                ObjectCallCanvas.CloseInterface();
                break;

            case Method.MethodType.setter:
                ObjectCallCanvas.CallerCanvas.SetActive(false);
                ObjectCallCanvas.SetterCanvas.SetActive(true);

                foreach(var attributo in attributi)
                {
                    ObjectCallCanvas.setterMethod.CreateAttributeSetter( attributo, oggettoEscapeValue.attributes.Find(x => x.attributeName == attributo).attributeValue);
                }

                break;

            case Method.MethodType.interactor:

                List<(string, string)> attributesWithValues = new List<(string, string)>();
                foreach (var attributo in attributi)
                {
                    (string,string) tupla= (attributo, oggettoEscapeValue.attributes.Find(x => x.attributeName == attributo).attributeValue);
                    attributesWithValues.Add(tupla);
                }

                methodListener.Method(attributesWithValues);
                ObjectCallCanvas.CloseInterface();
                break;

            case Method.MethodType.pickUp:
                //aggiungere all'inventario
                //Inventario.istanza.PickUpObject(CopyObject());
                oggettoEscapeValue.ObjectInteractorId = 0;
                Inventario.istanza.PickUpObject(oggettoEscapeValue);
                Inventario.istanza.oggettiUsed.Remove(oggettoEscapeValue);
                ObjectCallCanvas.CloseInterface();
                methodListener.RemoveObject();
                Destroy(gameObject);
                break;

            case Method.MethodType.destroy:
                ObjectCallCanvas.CloseInterface();
                methodListener.RemoveObject();
                Inventario.istanza.oggettiUsed.Remove(oggettoEscapeValue);
                Destroy(gameObject);
                break;
        }
    }
}