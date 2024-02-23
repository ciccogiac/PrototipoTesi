using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level2.Door
{
    [RequireComponent(typeof(Animator))]
    public class DoorLevel2 : MethodListener
    {
        private Animator _animator;
        [SerializeField] private List<MethodListener> MethodsListenerToRead;
        [SerializeField] private string ClassValueListener;
        [SerializeField] private MonitorLevel2 DoorMonitor;
        private static readonly int Open = Animator.StringToHash("open");

        public override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
        }
        public override void SetClass(string nameClass)
        {
            base.SetClass(nameClass);
            DoorMonitor.SetClass(nameClass);
            if (className != ClassValueListener)
            {
                DoorMonitor.SetError("Classe errata");
                ChangeTubeColor("Error");
            }
        }
        public override void RemoveObject()
        {
            base.RemoveObject();
            DoorMonitor.RemoveObject();
        }
        public override bool Method(List<(string, string)> objectValue)
        {
            if (className != ClassValueListener)
            {
                DoorMonitor.SetError("Classe errata");
                ChangeTubeColor("Error");
                return false;
            }

            foreach (var value in attributeValueListener)
            {
                var found = false;
                var correctValue = true;
                foreach (var m in MethodsListenerToRead)
                {
                    if (m.objectAttributeValue != null && value.className == m.className)
                    {
                        var classValue = Inventario.istanza.classi.Find(x => x.className == m.className);
                        if (classValue != null)
                        {
                            if (classValue.attributes.Find(x => x.attribute == value.attribute).visibility)
                            {
                                var tupla = m.objectAttributeValue.Find(x =>
                                    x.Item1 == value.attribute && x.Item2 == value.value);
                                if (tupla != (null, null))
                                {
                                    found = true;
                                }
                                else if (tupla.Item1 != null)
                                {
                                    DoorMonitor.SetError($"Attributo: {value.attribute} ha un valore errato");
                                    ChangeTubeColor("Error");
                                    found = false;
                                    correctValue = false;
                                }
                                else
                                {
                                    DoorMonitor.SetError(
                                        $"Non riesco a legggere il valore di: {value.attribute}. Si prega di chiamare il metodo corretto");
                                    ChangeTubeColor("Error");
                                    found = false;
                                    correctValue = false;
                                }
                            }
                            else
                            {
                                Debug.Log($"Attributo: {value.attribute} non accessibile perchè private");
                                DoorMonitor.SetError($"Attributo: {value.attribute} non accessibile perchè private");
                                ChangeTubeColor("Error");
                                correctValue = false;
                            }
                        }
                        else
                        {
                            var tupla = m.objectAttributeValue.Find(x =>
                                x.Item1 == value.attribute && x.Item2 == value.value);
                            if (tupla != (null, null))
                            {
                                found = true;
                            }
                            else
                            {
                                tupla = m.objectAttributeValue.Find(x =>
                                    x.Item1 == value.attribute && x.Item2 != value.value);
                                if (tupla != (null, null))
                                {
                                    DoorMonitor.SetError($"Attributo: {value.attribute} ha un valore errato");
                                    ChangeTubeColor("Error");
                                    found = false;
                                    correctValue = false;
                                }
                            }
                        }
                    }
                }

                if (!found)
                {
                    if (correctValue) DoorMonitor.SetError($"Nessun oggetto della classe {value.className} trovato");
                    ChangeTubeColor("Error");
                    return false;
                }
            }
            ApplyMethod();
            return true;
        }
        public override void ApplyMethod()
        {
            DoorMonitor.SetError("");
            ChangeTubeColor("Getter");
            _animator.SetBool(Open, true);
            DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
        }
    }
}
