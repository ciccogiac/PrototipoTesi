using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public StarterAssetsInputs _input;
    [SerializeField] GameObject canvasInventory;
    private bool inventortState = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.inventory == true)
        {
            _input.inventory = false;
            inventortState = !inventortState;
            canvasInventory.SetActive(inventortState);
            
        }
    }
}
