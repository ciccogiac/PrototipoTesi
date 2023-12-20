using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorCamera_enemy : MonoBehaviour
{
    [SerializeField] float angoloRotazione = 45f; // angolo di rotazione in gradi 
    [SerializeField] float velocitaRotazione = 45f; // Velocità di rotazione in gradi al secondo
    [SerializeField] float pausaSuperiore = 1f;     // Tempo di pausa in cima alla rotazione in secondi
    [SerializeField] float pausaInferiore = 1f;     // Tempo di pausa in basso alla rotazione in secondi

    private enum DirezioneRotazione
    {
        VersoAlto,
        VersoBasso
    }

    private DirezioneRotazione direzioneAttuale = DirezioneRotazione.VersoAlto;
    private float tempoDiPausa;
    private bool wait = false;

    [SerializeField] Transform pointStartingRaycast;
    [SerializeField] Transform pointEndRaycast;
    [SerializeField] float raycastLength;

    [SerializeField] LineRenderer line;
    private int layerMask;
    private RaycastHit2D hit;

    [SerializeField] GameManager_ClassGame gameManager;

    private float temporaryRotation=0;
    private float localRotation=0;

    void OnValidate()
    {
        pointEndRaycast.position = pointStartingRaycast.position + pointStartingRaycast.right * raycastLength;
        line.SetPosition(0, pointStartingRaycast.position);
        line.SetPosition(1, pointEndRaycast.position);
    }
    private void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
        layerMask = LayerMask.GetMask("Arrow_Pointer", "Line");
        line.useWorldSpace = true;
        line.SetPosition(0,pointStartingRaycast.position);
        pointEndRaycast.position = pointStartingRaycast.position + pointStartingRaycast.right * raycastLength;
        gameManager = FindAnyObjectByType<GameManager_ClassGame>();
    }
    void Update()
    {
        line.SetPosition(0, pointStartingRaycast.position);
        line.SetPosition(1, pointEndRaycast.position);

        RaycastShoot();
        CameraRotation();

     
    }

    private void RaycastShoot()
    {
        hit = Physics2D.Raycast(pointStartingRaycast.position, pointStartingRaycast.right, raycastLength, layerMask);
        //Debug.DrawRay(pointStartingRaycast.position, pointStartingRaycast.right * raycastLength, Color.red, 1f);

        if (hit.collider != null && !hit.collider.GetComponentInParent<Drag_Rigidbody>().isConnected )
        {
            gameManager.GameOver();
        }
    }

    private void CameraRotation()
    {
        // Controlla il tempo di pausa
        if (wait && Time.time >= tempoDiPausa)
        {
            wait = false;
        }

        if (!wait)
        {
            switch (direzioneAttuale)
            {

                case DirezioneRotazione.VersoAlto:
                    // Ruota verso l'alto
                    //if (transform.rotation.eulerAngles.z <= 45f || transform.rotation.eulerAngles.z >= 270f)
                    if (localRotation <= angoloRotazione)
                        {
                        temporaryRotation = (Vector3.forward.z * velocitaRotazione * Time.deltaTime);
                        transform.Rotate(0,0,temporaryRotation);
                        localRotation += temporaryRotation;

                    }
                    else
                    {
                        // Ferma la rotazione e attendi
                        direzioneAttuale = DirezioneRotazione.VersoBasso;
                        tempoDiPausa = Time.time + pausaSuperiore;
                        wait = true;
                        localRotation = 0;
                    }
                    break;

                case DirezioneRotazione.VersoBasso:
                    // Ruota verso il basso
                    if (localRotation <= angoloRotazione)
                    {
                        temporaryRotation = (Vector3.forward.z * -velocitaRotazione * Time.deltaTime);
                        transform.Rotate(0, 0, temporaryRotation);
                        localRotation -= temporaryRotation;
                    }
                    else
                    {
                        // Ferma la rotazione e attendi
                        direzioneAttuale = DirezioneRotazione.VersoAlto;
                        tempoDiPausa = Time.time + pausaInferiore;
                        wait = true;
                        localRotation = 0;
                    }
                    break;
            }
        }
    }
}
