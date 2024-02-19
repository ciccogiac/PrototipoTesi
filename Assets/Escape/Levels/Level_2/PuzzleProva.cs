using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleProva : MethodListener
{
    [SerializeField] Printer3DController _printer3D;
    [SerializeField] GameManager_Escape gameManager;

    public override bool Method(List<(string, string)> objectValue)
    {
        Debug.Log("Fai partire il puzzle");
        ApplyMethod();
        return true;
    }

    public override void ApplyMethod()
    {
        DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
        _printer3D.gameObject.SetActive(true);
        gameManager.printer = _printer3D;
    }
}
