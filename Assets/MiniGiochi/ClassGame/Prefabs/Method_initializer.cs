using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*Inizializza il box metodo
  Gestisce il numero di linee , aggiungendone o sottraendone.
 */
public class Method_initializer : MonoBehaviour
{
    public string method_name;
    [SerializeField] GameObject line_prefab;    //Prefab della linea

    [SerializeField] Transform pointLine_start; //Punto iniziale della linea
    [SerializeField] Transform pointArrow_start; //Punto iniziale della linea
    [SerializeField] Transform[] pointArrows_starts; //Punto iniziale della linea

    private List<GameObject> linee;             //Lista delle linee del box
    [SerializeField] int max_lineeNumber = 4;   //Massimo numero di linee possibili per box metodo

    //Bottoni per Aggiungere/Rimuovere linee
    [SerializeField] GameObject button_less;
    [SerializeField] GameObject button_plus;

    [SerializeField] RectTransform method_boxImage;

    [SerializeField] TextMeshProUGUI methodNameText;

    // Start is called before the first frame update
    public void initialize()
    {
        methodNameText.text = method_name;
        linee = new List<GameObject>();
        button_less.SetActive(false);
    }

    //Crea una nuova linea dal box del metodo
    public void CreateNewLine()
    {
        if (linee.Count < max_lineeNumber) {
            GameObject oggettoIstanziato = Instantiate(line_prefab, pointLine_start.position, Quaternion.identity);
            oggettoIstanziato.transform.SetParent(gameObject.transform);
            linee.Insert(linee.Count, oggettoIstanziato);
            Define_ConnectionPoints(oggettoIstanziato.transform);
            Drag_Rigidbody mi = oggettoIstanziato.GetComponent<Drag_Rigidbody>();       
            if (mi != null)
            {
                mi.method_name = method_name;
                mi.lc.setLineStartingPoint(pointLine_start, mi.gameObject.transform);
            }
            if (!button_less.active) { button_less.SetActive(true); }
            if (linee.Count == 4) { button_plus.SetActive(false); }
        }

    }

    //Rimuove l'ultima linea creata dal box del metodo
    public void RemoveLine()
    {
        if (linee.Count > 0)
        {
            GameObject g = linee[linee.Count - 1];
            linee.RemoveAt(linee.Count - 1);
            Destroy(g);
            if (!button_plus.active) { button_plus.SetActive(true); }
        }
        if(linee.Count == 0) { button_less.SetActive(false); }
    }

    private void Define_ConnectionPoints(Transform l)
    {
        /*
        float f = (method_boxImage.rect.height / 3) * ((linee.Count -1) % 4);
        Vector3 pos = pointArrow_start.position;
        pos.y = pointArrow_start.position.y + (f );
        l.transform.position = pos;
        */
        
        l.transform.position = pointArrows_starts[(linee.Count - 1) % 4].position;

    }

}
