using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OggettoEscape : Interactable
{
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
        SetObjectValue();
        if (mesh == null)
        {
            mesh = GetComponent<MeshFilter>().mesh;
            material = GetComponent<MeshRenderer>().material;
        }
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

    public void SetObjectValue()
    {
        /*
        objectName= "Chitarra";
        attributes = new List<(string, string)> { ("Legno", "Faggio"), ("Numero corde", "12"), ("isElectric", "false") };
        methods = new List<(string, MethodType, List<string>)> {
            ("CambiaCorde", MethodType.setter, new List<string>{ "Numero corde" }),
            ("GetTipoLegno", MethodType.getter,new List<string>{ "Legno" }),
            ("IsEletctricGuitar", MethodType.getter,new List<string>{ "isElectric" } ),
            ("PickUpObject", MethodType.pickUp, new List<string>{ }), ("DestroyObject", MethodType.destroy,new List<string>{ }) };
        */
        /*
        objectName = "Chiave";
        attributes = new List<(string, string)> {("DoorName", "ingresso") , ("Lunghezza", "3") };
        methods = new List<(string, Method.MethodType, List<string>)> {
            ("ApriPorta", Method.MethodType.interactor, new List<string>{  }),
            ("PickUpObject", Method.MethodType.pickUp, new List<string>{ }), ("DestroyObject", Method.MethodType.destroy,new List<string>{ }) };
        */
    }

    public void CallMethod(string m)
    {
        (Method, List<string>) method = methods.Find(x => x.Item1.methodName == m);
        Method.MethodType methodType = method.Item1.methodType;
        List<string> attributi = method.Item2;
        switch (methodType)
        {
            case Method.MethodType.getter:
                foreach(var attributo in attributi)
                {
                    Debug.Log("Attribute: " + attributo + " Value : " +  attributes.Find(x => x.Item1 == attributo).Item2);
                }
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
                methodListener.Method(attributes);
                ObjectCallCanvas.CloseInterface();
                break;

            case Method.MethodType.pickUp:
                //aggiungere all'inventario
                Inventario.istanza.PickUpObject(CopyObject());
                ObjectCallCanvas.CloseInterface();
                Destroy(gameObject);
                break;

            case Method.MethodType.destroy:
                ObjectCallCanvas.CloseInterface();
                Destroy(gameObject);
                break;
        }
    }
}
