using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotazioneTesto : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Ottieni la rotazione della telecamera
        Quaternion rotazioneTelecamera = Camera.main.transform.rotation;

        // Applica la rotazione al testo
        transform.rotation = rotazioneTelecamera;
    }
}
