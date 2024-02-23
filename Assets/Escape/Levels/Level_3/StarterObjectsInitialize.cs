using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterObjectsInitialize : MonoBehaviour
{
    [SerializeField] List<OggettoEscape> objectToInitialize;
    public void Initializeobjects()
    {
        foreach( var x in objectToInitialize)
        {
            Inventario.istanza.oggettiUsed.Add(x.oggettoEscapeValue);
        }

        
    }


}
