using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OggettoEscape : Interactable
{
    public bool isMadeByPrinter = true;

    public string objectName ;
    public string className;
    public List<(string, string)> attributes = new List<(string, string)>();   // primo valore attributeName secondo attributeValue
    public List<(Method,List<string>)> methods = new List<(Method, List<string>)>(); // nome metodo , tipo metodo , lista di attributi a cui fa riferimento

    public MethodListener methodListener;

    public Mesh mesh;
    public Material material;


    private InventoryLoad inventoryLoad;

    public ObjectCallMethods ObjectCallCanvas;


    public void InitializeObject(string _objectName, string _className, List<(string, string)> _attributes, List<(Method, List<string>)> _methods  , MethodListener _methodListener, Mesh _mesh, Material _material)
    {
        objectName = _objectName;
        className = _className;
        attributes = _attributes;
        methods = _methods;
        methodListener = _methodListener;
        mesh = _mesh;
        material = _material;
    }


    // Start is called before the first frame update
    void Start()
    {
        inventoryLoad = FindObjectOfType<InventoryLoad>();
        if (mesh == null)
        {
            mesh = GetComponent<MeshFilter>().mesh;
            material = GetComponent<MeshRenderer>().material;
        }

        if (!isMadeByPrinter)
        {
            ReadOggettoEscapeNotPrinted();
        }

    }

    public void ReadOggettoEscapeNotPrinted()
    {
        OggettoEscapeNotPrintedValue oggettoNotPrinted = GetComponent<OggettoEscapeNotPrintedValue>();

        List<(string, string)> attributeTemporary = new List<(string, string)>();
        foreach (var x in oggettoNotPrinted.attributes){ attributeTemporary.Add((x.attributeName, x.attributeValue));}
        attributes = attributeTemporary;

        List<(Method, List<string>)> methodsTemporary = new List<(Method, List<string>)>();
        foreach (var x in oggettoNotPrinted.methods) { methodsTemporary.Add((x.method, x.attributes)); }
        methodsTemporary.Add((new Method("PickUpObject", Method.MethodType.pickUp), new List<string> { }));
        methods = methodsTemporary;

       
    }

    private OggettoEscape CopyObject()
    {
        GameObject nuovoGameobject= new GameObject();
        OggettoEscape ogettoCopia= nuovoGameobject.AddComponent<OggettoEscape>();
        ogettoCopia.InitializeObject(objectName,className,attributes,methods,methodListener,mesh,material);
        nuovoGameobject.name = objectName;
        return ogettoCopia;
    }
    override public void Interact()
    {
        if (isActive)
        {
            gameObject.SetActive(false);
            isActive = false;

            Inventario.istanza.PickUpObject(CopyObject());
            Destroy(gameObject);
        }
    }

    public void CallMethod(string m)
    {
        (Method, List<string>) method = methods.Find(x => x.Item1.methodName == m);
        Method.MethodType methodType = method.Item1.methodType;
        List<string> attributi = method.Item2;
        switch (methodType)
        {
            case Method.MethodType.getter:
                List<(string, string)> attributesWithValue = new List<(string, string)>();
                foreach (var attributo in attributi)
                {
                    (string, string) tupla = (attributo, attributes.Find(x => x.Item1 == attributo).Item2);
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
                    ObjectCallCanvas.setterMethod.CreateAttributeSetter( attributo, attributes.Find(x => x.Item1 == attributo).Item2);
                }

                break;

            case Method.MethodType.interactor:

                List<(string, string)> attributesWithValues = new List<(string, string)>();
                foreach (var attributo in attributi)
                {
                    (string,string) tupla= (attributo,attributes.Find(x => x.Item1 == attributo).Item2);
                    attributesWithValues.Add(tupla);
                }

                methodListener.Method(attributesWithValues);
                ObjectCallCanvas.CloseInterface();
                break;

            case Method.MethodType.pickUp:
                //aggiungere all'inventario
                Inventario.istanza.PickUpObject(CopyObject());
                ObjectCallCanvas.CloseInterface();
                methodListener.RemoveObject();
                Destroy(gameObject);
                break;

            case Method.MethodType.destroy:
                ObjectCallCanvas.CloseInterface();
                methodListener.RemoveObject();
                Destroy(gameObject);
                break;
        }
    }
}
