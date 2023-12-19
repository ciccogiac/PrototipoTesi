using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorCamera_enemy : MonoBehaviour
{

    public float velocitaRotazione = 45f; // Velocità di rotazione in gradi al secondo
    public float pausaSuperiore = 1f;     // Tempo di pausa in cima alla rotazione in secondi
    public float pausaInferiore = 1f;     // Tempo di pausa in basso alla rotazione in secondi

    private enum DirezioneRotazione
    {
        VersoAlto,
        VersoBasso
    }

    private DirezioneRotazione direzioneAttuale = DirezioneRotazione.VersoAlto;
    private float tempoDiPausa;
    private bool wait = false;

    [SerializeField] Transform pointStartingRaycast;
    [SerializeField] float raycastLength;

    void Update()
    {
        int layerMask = LayerMask.GetMask("Arrow_Pointer");
        RaycastHit2D hit = Physics2D.Raycast(pointStartingRaycast.position, pointStartingRaycast.right, raycastLength, layerMask);
        Debug.DrawRay(pointStartingRaycast.position, pointStartingRaycast.right * raycastLength, Color.red, 1f);

        if (hit.collider != null)
        {
            Debug.Log("colpito");
        }


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
                    if (transform.rotation.eulerAngles.z <= 45f || transform.rotation.eulerAngles.z >= 270f)
                    {
                        transform.Rotate(Vector3.forward * velocitaRotazione * Time.deltaTime);

                    }
                    else
                    {
                        // Ferma la rotazione e attendi
                        direzioneAttuale = DirezioneRotazione.VersoBasso;
                        tempoDiPausa = Time.time + pausaSuperiore;
                        wait = true;
                    }
                    break;

                case DirezioneRotazione.VersoBasso:
                    // Ruota verso il basso
                    if (transform.rotation.eulerAngles.z <= 90f || transform.rotation.eulerAngles.z  >= 315f)
                    {
                        transform.Rotate(Vector3.forward * -velocitaRotazione * Time.deltaTime);
                    }   
                    else
                    {
                        // Ferma la rotazione e attendi
                        direzioneAttuale = DirezioneRotazione.VersoAlto;
                        tempoDiPausa = Time.time + pausaInferiore;
                        wait = true;
                    }
                    break;
            }
        }

     
    }
}
