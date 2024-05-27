using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/save.dat";
    }

    public void Save(int numberToSave, List<(string, string)> teory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(saveFilePath);

        SaveData saveData = new SaveData(numberToSave,teory);

        formatter.Serialize(fileStream, saveData);
        fileStream.Close();
        
        //Debug.Log("Salvataggio completato. Nuovo numero salvato: " + numberToSave);
        DatiPersistenti.LogMessage($"Iniziato nuovo livello: {numberToSave}");
    }

    public (int, List<(string, string)> ) LoadSave()
    {
        if (File.Exists(saveFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(saveFilePath, FileMode.Open);

            SaveData saveData = (SaveData)formatter.Deserialize(fileStream);
            fileStream.Close();

            return (saveData.savedNumber,saveData.teoria);
        }
        else
        {
            Debug.Log("Nessun salvataggio trovato. Creazione di un nuovo salvataggio.");
            return (0,null); // Valore predefinito se non ci sono salvataggi
        }
    }
}

[System.Serializable]
public class SaveData
{
    public int savedNumber;
    public List<(string, string)> teoria = new List<(string, string)>();

    public SaveData(int number, List<(string, string)> teory)
    {
        savedNumber = number;
        teoria = teory;
    }
}
