using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionBlock : MonoBehaviour
{
    private GridBlock block;

    private void Start()
    {
        block = GetComponentInParent<GridBlock>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide");
        if (!block.isConnected) { block.ConnectBlock(); }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (block.isConnected) { block.DisconnectBlock(); }
    }

    
}
