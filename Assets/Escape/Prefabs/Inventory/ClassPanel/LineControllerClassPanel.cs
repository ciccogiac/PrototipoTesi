using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineControllerClassPanel : MonoBehaviour
{
    //public Transform pointLineStart;
    public Vector2 endPosition;
    public Transform e;


    private List<Transform> lt = new List<Transform>();

    //public Image lineImage; // Riferimento all'oggetto Image da modificare
    public List<Image> lineImage;

    private bool canStart = false;

    // Metodo per impostare la nuova posizione dell'immagine
    public void SetImagePosition(int i )
    {

        RectTransform rectTransform = lineImage[i].rectTransform;

        Vector2 startPosition = rectTransform.anchorMin;
        Vector2 direction =  startPosition - endPosition;
        float length = direction.magnitude;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Imposta la lunghezza e l'angolazione della "linea"
        rectTransform.sizeDelta = new Vector2(length, rectTransform.sizeDelta.y);
        rectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void SetPointLine(List<Transform> ep)
    {
        lt = ep;

        //lineImage

        if (lineImage.Count > 0)
        {
            // Duplica l'immagine e modifica le sue dimensioni
            for (int i = 1; i < ep.Count; i++)
            {
                Image nuovaImmagine = Instantiate(lineImage[0]); // Duplica l'immagine
                nuovaImmagine.transform.SetParent(transform); // Imposta il genitore dell'immagine duplicata

                // Modifica le dimensioni dell'immagine duplicata
                RectTransform rectTransform = nuovaImmagine.GetComponent<RectTransform>();
                rectTransform.position = lineImage[0].rectTransform.position;
                rectTransform.sizeDelta =new Vector2( 0f,10f);

                // Aggiungi la nuova immagine alla lista
                lineImage.Add(nuovaImmagine);
            }

            canStart = true;
        }


    }

    
    private void Update()
    {
        if (canStart)
        {
            int i = 0;

            foreach (var t in lt)
            {
                e = t;


                endPosition = e.InverseTransformPoint(lineImage[i].rectTransform.position);
                SetImagePosition(i);

                i++;
            }
        }


        //endPosition = e.InverseTransformPoint(lineImage.rectTransform.position);
        //SetImagePosition();
    }
    
}
