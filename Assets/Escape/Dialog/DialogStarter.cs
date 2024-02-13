using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogStarter : MonoBehaviour
{
    private GameManager_Escape gameManager;
    public StarterAssetsInputs _input;
    [SerializeField] CinemachineVirtualCamera dialogCamera;

    private int i = 0;
    private bool dialogUsed = false;
    private bool dialogOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager_Escape>();
        _input = FindObjectOfType<StarterAssetsInputs>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dialogUsed)
        {
            gameManager.ActivateDialogCanvas();
            dialogCamera.enabled = true;
            dialogOpen = true;
        }
    }

    private void Update()
    {
        if (dialogOpen)
        {
            if (_input.interact)
            {
                Debug.Log("Dialogo n " + i);
                i++;
            }

            if (_input.skip)
            {
                gameManager.DeactivateDialogCanvas();
                gameManager.SwitchCameraToPrimary(dialogCamera);
                dialogUsed = true;
                dialogOpen = false;
            }
        }
    }
}
