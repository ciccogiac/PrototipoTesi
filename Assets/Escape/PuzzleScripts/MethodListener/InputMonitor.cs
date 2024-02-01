using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputMonitor : Monitor
{
    [SerializeField] string classValueListener;
    [SerializeField] string valueTarget;

    [SerializeField] Animator door;

    public override bool MethodInput(List<(string, string)> objectValue, List<(string, string)> inputValue)
    {
        if (className != classValueListener)
        {
            //doorMonitor.SetError("Classe Errata");
            ChangeTubeColor("Error");
            return false;
        }
        else
        {
            ChangeTubeColor("Connected");

            Dictionary<string, string> combinedDictionary = objectValue.Concat(inputValue).ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);


            combinedDictionary.TryGetValue("mese", out string mese);
            combinedDictionary.TryGetValue("giorno", out string giorno);


            int.TryParse(mese, out int m);
            int.TryParse(giorno, out int g);

             m = (m - 1) * 30;
             g = g + m;
            int giorni = 365 - g;

            SetError(giorni.ToString());

            if (valueTarget == giorni.ToString())
            {
                ChangeTubeColor("Getter");
                door.SetBool("character_nearby", true);
                return true;
            }
        }
        ChangeTubeColor("Error");
        return false;
    }
}
