using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_electric : MonoBehaviour
{
    [SerializeField] GameManager_ClassGame gameManager;
    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager_ClassGame>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Line") || collision.CompareTag("Arrow_Pointer"))
        {
            gameManager.GameOver();
        }
    }

}
