using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    private string saveFilePath ;

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/save.dat";
    }

    public void Save(int numberToSave)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(saveFilePath);

        SaveData saveData = new SaveData(numberToSave);

        formatter.Serialize(fileStream, saveData);
        fileStream.Close();

        Debug.Log("Salvataggio completato. Nuovo numero salvato: " + numberToSave);
    }

    public int LoadSave()
    {
        if (File.Exists(saveFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(saveFilePath, FileMode.Open);

            SaveData saveData = (SaveData)formatter.Deserialize(fileStream);
            fileStream.Close();

            return saveData.savedNumber;
        }
        else
        {
            Debug.Log("Nessun salvataggio trovato. Creazione di un nuovo salvataggio.");
            return 0; // Valore predefinito se non ci sono salvataggi
        }
    }
}

[System.Serializable]
public class SaveData
{
    public int savedNumber;

    public SaveData(int number)
    {
        savedNumber = number;
    }
}
