using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Method_initializer : MonoBehaviour
{
    public string method_name;
    [SerializeField] GameObject line_prefab;

    [SerializeField] Transform pointLine_start;

    private List<GameObject> linee;
    [SerializeField] int max_lineeNumber = 4;

    [SerializeField] GameObject button_less;
    [SerializeField] GameObject button_plus;

    // Start is called before the first frame update
    public void initialize()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = method_name;
        linee = new List<GameObject>();
        button_less.SetActive(false);
    }

    public void CreateNewLine()
    {
        if (linee.Count < max_lineeNumber) {
            GameObject oggettoIstanziato = Instantiate(line_prefab, pointLine_start.position, Quaternion.identity);
            oggettoIstanziato.transform.SetParent(gameObject.transform);
            linee.Insert(linee.Count, oggettoIstanziato);
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


}
