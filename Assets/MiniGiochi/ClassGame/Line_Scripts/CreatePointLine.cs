using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePointLine : MonoBehaviour
{
    [SerializeField] LineController lc;
    public  bool isAlreadyANode = false;

    public enum position_collider
    {
        UP_sx,
        UP_dx,
        DW_sx,
        DW_dx
    }

    public position_collider collider_pos;
    private void OnCollisionEnter2D(Collision2D collision)
    {/*
        if (!isAlreadyANode)
        {
            isAlreadyANode = true;

            GameObject node = new GameObject("node");
            node.transform.position = new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, 0);
            lc.setNodes(node.transform,this);
           
           
        }
        */

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlreadyANode)
        {
            isAlreadyANode = true;
             
            GameObject node = new GameObject("node");
            node.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            lc.setNodes(node.transform, this);


        }
    }
}
