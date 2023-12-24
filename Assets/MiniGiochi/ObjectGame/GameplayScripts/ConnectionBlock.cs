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

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ConnectionBlock" + block.gameObject.name);

        if (collision.CompareTag("ConnectionGive") && collision.GetComponentInParent<GridBlock>().isConnected && !block.isConnected)
        { block.ConnectBlock(); }
    }
    */

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("ConnectionBlock" + block.gameObject.name);

        if (collision.CompareTag("ConnectionGive") && collision.GetComponentInParent<GridBlock>().isConnected && !block.isConnected)
             { block.ConnectBlock(); }

        if (collision.CompareTag("ConnectionGive") && !collision.GetComponentInParent<GridBlock>().isConnected && block.isConnected)
        { block.DisconnectBlock(); }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ConnectionGive")  && block.isConnected)
        { block.DisconnectBlock(); }
    }

    
}
