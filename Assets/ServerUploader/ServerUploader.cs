using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace ServerUploader
{
    public class ServerUploader : MonoBehaviour
    {
        private const string ServerUrl = "https://matteobiffoni.eu.pythonanywhere.com/upload";

        public static IEnumerator UploadToServer()
        {
            DatiPersistenti.LogMessage("Log terminato");
            if (File.Exists(DatiPersistenti.LOGFilePath))
            {
                var logFileData = File.ReadAllBytes(DatiPersistenti.LOGFilePath);
                var form = new WWWForm();
                form.AddBinaryData("file", logFileData, Path.GetFileName(DatiPersistenti.LOGFilePath), "text/plain");
                using var www = UnityWebRequest.Post(ServerUrl, form);
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log($"Errore nel caricamento del file di log: {www.error}");
                }
                else
                {
                    Debug.Log("File di log caricato con successo!");
                    DatiPersistenti.LogFileUploaded = true;
                }
            }
            else
            {
                Debug.Log("File di log non trovato");
            }
        }
    }
}
