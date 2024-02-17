using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<SwitchCameraObject> switchCameraObejcts;
    [SerializeField] List<ObjectInteraction> objectInteractions;

    public void DeactivateObjects()
    {
        foreach(var x in switchCameraObejcts)
        {
            x.isActive = false;
            x.isInteractable = false;
        }

        foreach (var x in objectInteractions)
        {
            x.isActive = false;
        }
    }

    public void ActivateObjects()
    {
        foreach (var x in switchCameraObejcts)
        {
            x.isActive = true;
            x.isInteractable = true;
        }

        foreach (var x in objectInteractions)
        {
            x.isActive = true;
        }
    }

    private void Start()
    {
        DeactivateObjects();
    }


}
