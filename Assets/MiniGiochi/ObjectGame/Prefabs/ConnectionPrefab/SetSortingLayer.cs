using TMPro;
using UnityEngine;

public class SetSortingLayer : MonoBehaviour
{
    public string sortingLayerName = "EndGame"; // Nome dello strato di rendering desiderato

    void Start()
    {
        // Ottieni il componente TextMeshPro
        TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();

        // Verifica se il componente TextMeshPro esiste
        if (textMeshPro != null)
        {
            // Ottieni l'ID dello strato di rendering dal nome
            int sortingLayerID = SortingLayer.NameToID(sortingLayerName);

            // Ottieni il componente Renderer
            Renderer renderer = textMeshPro.GetComponent<Renderer>();

            // Verifica se il componente Renderer esiste
            if (renderer != null)
            {
                // Ottieni il materiale dal Renderer
                Material material = renderer.material;

                // Imposta l'ID dello strato di rendering nel materiale
                material.SetFloat("_SortingLayer", sortingLayerID);

                // Aggiorna il materiale per riflettere le modifiche
                textMeshPro.ForceMeshUpdate();
            }
            else
            {
                Debug.LogError("Il componente Renderer non è presente sull'oggetto TextMeshProUGUI.");
            }
        }
        else
        {
            Debug.LogError("Il componente TextMeshProUGUI non è presente sull'oggetto.");
        }
    }
}
