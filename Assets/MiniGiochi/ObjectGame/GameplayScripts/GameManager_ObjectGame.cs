using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_ObjectGame : MonoBehaviour
{
    public GridBlock selectedBlock;
    public bool isABlockSelected = false;
    [SerializeField] GameObject emptyBlock;

    [SerializeField] GameObject ButtonDeselectBlock;
    [SerializeField] GameObject ButtonRemoveBlock;
    [SerializeField] GameObject ButtonsRotation;

    public enum AttributeType
    {
        intero,
        booleano,
        stringa
    }
    public AttributeType attributeType;

    private int attributeIntValue=0;
    public int AttributeIntTarget=0;

    private bool attributeBoolValue = false;
    public bool AttributeBoolTarget = false;

    private string attributeStringValue = "";
    public string AttributeStringTarget = "";

    [SerializeField] TextMeshProUGUI attributeValue_text;
    [SerializeField] TextMeshProUGUI attributeTarget_text;

    [SerializeField] TextMeshProUGUI attributeName_text;

    public bool isTemporaryItemDragging = false;
    public GameObject trash;

    [SerializeField] Block[] blocks;

    [SerializeField] float secondToShowCompleted = 5f;
    [SerializeField] GameObject canvas_AttributeComplete;

    private int attributeGamelevel = 0;
    [SerializeField] AttributeGrid[] attributeGrids;
    [SerializeField] AttributeInventory[] attributeInventory;

    private bool is_game_won = false;

    [SerializeField] private string className;
    [SerializeField] TextMeshProUGUI className_text;
    [SerializeField] private string objectName;
    [SerializeField] TextMeshProUGUI objectName_text;

    private List<Attribute> attributesValues = new List<Attribute>();

    [SerializeField] GameObject gridLevels;
    [SerializeField] GameObject inventoryLevels;

    private void ReadLevel()
    {

        if (attributeGamelevel > 0)
        {
            attributeGrids[attributeGamelevel - 1].gameObject.SetActive(false);

            foreach (var x in attributeInventory)
            {
                if (x.attributeName == attributeGrids[attributeGamelevel - 1].attributeName)
                { x.gameObject.SetActive(false); continue; }
            }
            //attributeInventory[attributeGamelevel - 1].gameObject.SetActive(false); }
        }

            attributeGrids[attributeGamelevel].gameObject.SetActive(true);

            foreach (var x in attributeInventory)
            {
                if (x.attributeName == attributeGrids[attributeGamelevel].attributeName)
                { x.gameObject.SetActive(true); continue; }
            }

            //attributeInventory[attributeGamelevel].gameObject.SetActive(true);

            attributeType = attributeGrids[attributeGamelevel].attributeType;
            attributeName_text.text = attributeGrids[attributeGamelevel].attributeName;
            AttributeIntTarget = attributeGrids[attributeGamelevel].AttributeIntTarget;
            AttributeBoolTarget = attributeGrids[attributeGamelevel].AttributeBoolTarget;
            AttributeStringTarget = attributeGrids[attributeGamelevel].AttributeStringTarget;

            attributeGamelevel++;

        
    }
    private void LoadAttributeGame()
    {
        if (attributeGamelevel >= attributeGrids.Length) { is_game_won = true; CloseGame(); }
        else
        {
            ButtonDeselectBlock.SetActive(false);
            ButtonsRotation.SetActive(false);
            ButtonRemoveBlock.SetActive(false);
            isTemporaryItemDragging = false;
            isABlockSelected = false;

            ReadLevel();
            blocks = null;
            blocks = FindObjectsOfType<Block>();

            switch (attributeType)
            {
                case AttributeType.intero:
                    attributeValue_text.text = attributeIntValue.ToString();
                    attributeTarget_text.text = AttributeIntTarget.ToString();
                    break;

                case AttributeType.booleano:
                    attributeValue_text.text = attributeBoolValue.ToString();
                    attributeTarget_text.text = AttributeBoolTarget.ToString();
                    break;

                case AttributeType.stringa:
                    attributeValue_text.text = attributeStringValue;
                    attributeTarget_text.text = AttributeStringTarget;
                    break;

            }

            Attribute tupla = new Attribute (attributeName_text.text, attributeTarget_text.text);
            attributesValues.Add(tupla);

        }
      
    }

    private void LoadLevel()
    {
        Transform livelloGridTrovato = gridLevels.transform.Find(className);
        Transform livelloInventoryTrovato = inventoryLevels.transform.Find(className);

        if (livelloGridTrovato != null)
        {
            // Fai qualcosa con l'oggetto trovato
            //Debug.Log("Livello grid trovato: " + livelloGridTrovato.name);

            attributeGrids = livelloGridTrovato.GetComponentsInChildren<AttributeGrid>(true);
            livelloGridTrovato.gameObject.SetActive(true);
            
        }
        else
        {
            Debug.LogWarning("Nessun livello grid trovato con il nome specificato.");
        }

        if (livelloInventoryTrovato != null)
        {
            // Fai qualcosa con l'oggetto trovato
            //Debug.Log("Livello inventory trovato: " + livelloInventoryTrovato.name);

            attributeInventory = livelloInventoryTrovato.GetComponentsInChildren<AttributeInventory>(true);
            livelloInventoryTrovato.gameObject.SetActive(true);

        }
        else
        {
            Debug.LogWarning("Nessun livello inventory trovato con il nome specificato.");
        }


    }


    private void Start()
    {
        if (DatiPersistenti.istanza != null)
        {
            className = DatiPersistenti.istanza.className;
            objectName = DatiPersistenti.istanza.objectName;
        }
        className_text.text = className;
        objectName_text.text = objectName;

        trash = FindObjectOfType<TrashTemporaryItem>().gameObject;
        trash.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        LoadLevel();
        LoadAttributeGame();
    }

    public void CalculateAttributeValue(int value) { attributeIntValue += value; attributeValue_text.text = attributeIntValue.ToString(); }
    public void CalculateAttributeValue(bool value) { attributeBoolValue = value; attributeValue_text.text = attributeBoolValue.ToString(); }
    public void CalculateAttributeValue(char value) { attributeStringValue += value; attributeValue_text.text = attributeStringValue; }
    public void RemoveLastCharAttributeValue() {attributeStringValue = attributeStringValue.Substring(0, attributeStringValue.Length - 1); attributeValue_text.text = attributeStringValue; }

    public void VerifyAttributeValue() { 
        
        switch (attributeType) {
            case AttributeType.intero:
                if (attributeIntValue == AttributeIntTarget) { Debug.Log("Raggiunto il valore target"); AttributeComplete(); }
                break;

            case AttributeType.booleano:
                if (attributeBoolValue == AttributeBoolTarget) { Debug.Log("Raggiunto il valore target"); AttributeComplete(); }
                break;

            case AttributeType.stringa:
                if (attributeStringValue == AttributeStringTarget) { Debug.Log("Raggiunto il valore target"); AttributeComplete(); }
                break;

        }
    }

    public void Left_BlockRotation()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (isABlockSelected) { selectedBlock.RotateBlock(true); }
    }

    public void Right_BlockRotation()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (isABlockSelected) { selectedBlock.RotateBlock(false); }
    }

    public void SelectBlock(GridBlock block)
    {
        selectedBlock = block;
        isABlockSelected = true;
        block.SelectBlock();

        ButtonDeselectBlock.SetActive(true);  
        ButtonsRotation.SetActive(true);
        if(!selectedBlock.isStationary)
            ButtonRemoveBlock.SetActive(true);
    }

    public void DeselectBlock()
    {
        selectedBlock.DeselectBlock();
        selectedBlock = null;
        isABlockSelected = false;
       
        ButtonDeselectBlock.SetActive(false);
        ButtonsRotation.SetActive(false);
        ButtonRemoveBlock.SetActive(false);
    }

    public void ResetPath()
    {
        if (!isTemporaryItemDragging)
        {
            foreach (var block in blocks)
            {
                block.RestoreEmptyBlock();
            }

            if(selectedBlock!=null) { selectedBlock.DeselectBlock(); }
            selectedBlock = null;
            isABlockSelected = false;

            ButtonDeselectBlock.SetActive(false);
            ButtonRemoveBlock.SetActive(false);
            ButtonsRotation.SetActive(false);
        }
    }

    public void RemoveGridBlock()
    {
        GameObject g = Instantiate(emptyBlock, new Vector3(0f, 0f, 0f), Quaternion.identity);
        g.transform.parent = selectedBlock.gameObject.transform.parent;
        selectedBlock.gameObject.GetComponent<GridBlock>().inventoryReference.CancelDeleteItem();
        Destroy(selectedBlock.gameObject);
        g.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        selectedBlock = null;
        isABlockSelected = false;

        ButtonDeselectBlock.SetActive(false);
        ButtonRemoveBlock.SetActive(false);
        ButtonsRotation.SetActive(false);
    }
    public void CloseGame()
    {
        if (!isTemporaryItemDragging)
        {
            Cursor.visible = false;
            if (is_game_won) {
                //Non devo eliminare la classe dall'inventario , anche perch� posso creare pi� oggetti della stessa classe
                //Non genero qui l'oggetto perch� viene inserito nell'inventario quando raccolto dalla stampante
                //Qui devo dare l'input alla stampante per avviare l'animazione di stampa e stampare il relativo oggetto
                DatiPersistenti.istanza.isObjectToPrint = true;
                DatiPersistenti.istanza.attributesValues = attributesValues ;
                

                //In maniera temporanea gestisco la vittoria creando direttamente nell'inventario l'oggetto desiderato
                //Inventario.istanza.oggetti.Add(objectName);

                SceneManager.LoadScene("Playground"); }
            else { SceneManager.LoadScene("Playground"); }
        }
    }

    public void AttributeComplete()
    {
        StartCoroutine(AttributeCompletedCoroutine(secondToShowCompleted));
    }

    IEnumerator AttributeCompletedCoroutine(float time)
    {
        attributeGrids[attributeGamelevel - 1].gameObject.SetActive(false);
        canvas_AttributeComplete.SetActive(true);
        yield return new WaitForSeconds(time);

        canvas_AttributeComplete.SetActive(false);
        LoadAttributeGame();
    }

    //Cheat
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(AttributeCompletedCoroutine(1f));
        }
    }
}
