using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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

    [SerializeField] GameObject tutorialCanvas;

    Transform livelloGridTrovato ;
    Transform livelloInventoryTrovato;

    [SerializeField] GameObject UpPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject InventoryPanel;


    public AudioSource _audioSource;

    [SerializeField] AudioClip rotationSound;
    public AudioClip deleteSound;
    [SerializeField] AudioClip resetSound;
    public AudioClip selectSound;
    [SerializeField] AudioClip closeSound;
    public AudioClip selectSound2;
    [SerializeField] AudioClip completeSound;

    [SerializeField] GameObject commandPanel1;
    [SerializeField] GameObject commandPanel2;
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

            isABlockSelected = false;

        }
      
    }

    private void LoadLevel()
    {
         livelloGridTrovato = gridLevels.transform.Find(className);
         livelloInventoryTrovato = inventoryLevels.transform.Find(className);

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

    private void ActivateTutorial()
    {
        livelloGridTrovato.gameObject.SetActive(false);
        livelloInventoryTrovato.gameObject.SetActive(false);
        UpPanel.SetActive(false);
        gamePanel.SetActive(false);
        InventoryPanel.SetActive(false);
        tutorialCanvas.SetActive(true);
    }

    public void EndTutorial()
    {
        tutorialCanvas.SetActive(false);
        livelloGridTrovato.gameObject.SetActive(true);
        livelloInventoryTrovato.gameObject.SetActive(true);
        UpPanel.SetActive(true);
        gamePanel.SetActive(true);
        InventoryPanel.SetActive(true);

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

        trash.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        LoadLevel();
        LoadAttributeGame();

        //ActivateTutorial();

        if (className == "Operatore" && !DatiPersistenti.istanza.isTutorialStarted_OG)
        {
            DatiPersistenti.istanza.isTutorialStarted_OG = true;
            ActivateTutorial();
        }
           

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
        if (isABlockSelected) {
            selectedBlock.RotateBlock(true);
            _audioSource.clip = rotationSound;
            _audioSource.Play();
        }
    }

    public void Right_BlockRotation()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (isABlockSelected) { 
            selectedBlock.RotateBlock(false);
            _audioSource.clip = rotationSound;
            _audioSource.Play();
        }
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

            _audioSource.clip = resetSound;
            _audioSource.Play();
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

        _audioSource.clip = deleteSound;
        _audioSource.Play();
    }
    public void CloseGame()
    {
        if (!isTemporaryItemDragging)
        {
            Cursor.visible = false;
            if (is_game_won) {
                //Non devo eliminare la classe dall'inventario , anche perchè posso creare più oggetti della stessa classe
                //Non genero qui l'oggetto perchè viene inserito nell'inventario quando raccolto dalla stampante
                //Qui devo dare l'input alla stampante per avviare l'animazione di stampa e stampare il relativo oggetto
                DatiPersistenti.istanza.isObjectToPrint = true;
                DatiPersistenti.istanza.attributesValues = attributesValues ;
                

                //In maniera temporanea gestisco la vittoria creando direttamente nell'inventario l'oggetto desiderato
                //Inventario.istanza.oggetti.Add(objectName);

                SceneManager.LoadScene(DatiPersistenti.istanza.sceneIndex); }
            else {
                _audioSource.clip = closeSound;
                _audioSource.Play();
                SceneManager.LoadScene(DatiPersistenti.istanza.sceneIndex);
               
            }
        }
    }

    public void AttributeComplete()
    {
        StartCoroutine(AttributeCompletedCoroutine(secondToShowCompleted));
    }

    IEnumerator AttributeCompletedCoroutine(float time)
    {
        isABlockSelected = true;
        attributeGrids[attributeGamelevel - 1].gameObject.SetActive(false);
        attributeValue_text.text = attributeTarget_text.text;
        commandPanel1.SetActive(false);
        commandPanel2.SetActive(false);
        canvas_AttributeComplete.SetActive(true);
        _audioSource.clip = completeSound;
        _audioSource.Play();
        yield return new WaitForSeconds(time);

        canvas_AttributeComplete.SetActive(false);
        commandPanel2.SetActive(true);
        LoadAttributeGame();
    }

#if UNITY_EDITOR
    //Cheat
    private void Update()
    {
    
        if (EditorApplication.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AttributeCompletedCoroutine(1f));
            }

        }
    }
#endif
}
