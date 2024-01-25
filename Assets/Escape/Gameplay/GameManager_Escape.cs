using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Escape : MonoBehaviour
{
    public Clue[] clues;

    public ObjectInteraction[] objectInteractors;

    [SerializeField] GameObject objectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        clues = FindObjectsOfType<Clue>();

        foreach(var clue in clues)
        {
            if (Inventario.istanza.IsCluePickedUp(clue) || Inventario.istanza.IsClueUsed(clue) ) { Destroy(clue.gameObject); }
        }

        objectInteractors = FindObjectsOfType<ObjectInteraction>();

        foreach (var objectInteractor in objectInteractors)
        {
            OggettoEscapeValue o = Inventario.istanza.oggettiUsed.Find(x => x.ObjectInteractorId !=0 && x.ObjectInteractorId == objectInteractor.Id);
            if (o != null)
            {
                InstanziaOggetto(o, objectInteractor);
            }
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void InstanziaOggetto(OggettoEscapeValue oggettoEscapeValue , ObjectInteraction objectInteraction)
    {
        GameObject oggettoIstanziato = Instantiate(objectPrefab, objectInteraction.objectPoint.position, Quaternion.identity);
        oggettoIstanziato.GetComponent<OggettoEscape>().SetOggettoEscapeValue(oggettoEscapeValue);

        oggettoIstanziato.transform.position = objectInteraction.objectPoint.position;
        oggettoIstanziato.transform.SetParent(objectInteraction.gameObject.transform);

        oggettoIstanziato.gameObject.GetComponent<MeshFilter>().mesh = oggettoIstanziato.GetComponent<OggettoEscape>().oggettoEscapeValue.mesh;
        oggettoIstanziato.gameObject.GetComponent<MeshRenderer>().materials = oggettoIstanziato.GetComponent<OggettoEscape>().oggettoEscapeValue.material;
        float fattoreScala = 0.5f;
        oggettoIstanziato.gameObject.transform.localScale *= fattoreScala;
        oggettoIstanziato.gameObject.SetActive(true);

        //oggettoIstanziato.GetComponent<OggettoEscape>().SetObjectValue();
        objectInteraction.oggetto = oggettoIstanziato.GetComponent<OggettoEscape>();
        objectInteraction.oggetto.methodListener = objectInteraction.methodListener;
        objectInteraction.oggetto.methodListener.SetClass(objectInteraction.oggetto.oggettoEscapeValue.className);


    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Cursor.lockState = CursorLockMode.Locked;
    }


}
