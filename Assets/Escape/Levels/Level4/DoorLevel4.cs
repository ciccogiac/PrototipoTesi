using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Escape.Levels.Level4
{
    public class DoorLevel4 : MethodListener
    {
        [FormerlySerializedAs("door")] [SerializeField] private Animator DoorAnimator;

        [FormerlySerializedAs("methodsListenerToRead")] [SerializeField] private List<MethodListener> MethodsListenersToRead;
        [SerializeField] private string ClassValueListener;

        [FormerlySerializedAs("doorMonitor")] [SerializeField] private Monitor DoorMonitor;

        [UsedImplicitly]
        private bool _isTutorialOpened;

        [SerializeField] private Clue Teoria;
        [FormerlySerializedAs("tutorialCanvas")] [SerializeField] private GameObject TutorialCanvas;
        [FormerlySerializedAs("ocm")] [SerializeField] private ObjectCallMethods Ocm;
        private static readonly int Open = Animator.StringToHash("open");
        [SerializeField] private GameObject Ray1;
        [SerializeField] private GameObject Ray2;

        [SerializeField] ObjectCallMethods ObjectCallCanvas;
        [SerializeField] GameManager_Escape gameManager;

        public override void SetClass(string nameClass)
        {
            base.SetClass(nameClass);
            DoorMonitor.SetClass(nameClass);
            
            if (className != ClassValueListener)
            {
                DoorMonitor.SetError("Classe Errata");
                ChangeTubeColor("Error");
            }
        }
        
        public override void RemoveObject()
        {
            base.RemoveObject();
            DoorMonitor.RemoveObject();
        }
        public override bool  Method(List<(string, string)> objectValue)
        {
            if (className != ClassValueListener)
            {
                DoorMonitor.SetError("Classe Errata");
                ChangeTubeColor("Error");
                 return false;
            }

            StartCoroutine(ScanAnimation());

            return true;
        }

        private IEnumerator ScanAnimation()
        {
            var rayOriginalScale = Ray1.transform.localScale;
            Ray1.transform.localScale = Vector3.zero;
            Ray2.transform.localScale = Vector3.zero;
            Ray1.SetActive(true);
            Ray2.SetActive(true);
            var elapsedTime = 0.0f;
            while (elapsedTime < 0.5f)
            {
                Ray1.transform.localScale =
                    Vector3.Lerp(Ray1.transform.localScale, rayOriginalScale, elapsedTime / 0.5f);
                Ray2.transform.localScale =
                    Vector3.Lerp(Ray2.transform.localScale, rayOriginalScale, elapsedTime / 0.5f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Ray1.transform.localScale = rayOriginalScale;
            Ray2.transform.localScale = rayOriginalScale;
            elapsedTime = 0.0f;
            while (elapsedTime < 1.0f)
            {
                Ray1.transform.Rotate(0f, 0f, -1f);
                Ray2.transform.Rotate(0f, 0f, -1f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            elapsedTime = 0.0f;
            while (elapsedTime < 1.0f)
            {
                Ray1.transform.Rotate(0f, 0f, 1f);
                Ray2.transform.Rotate(0f, 0f, 1f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            elapsedTime = 0.0f;
            while (elapsedTime < 0.5f)
            {
                Ray1.transform.localScale = Vector3.Lerp(Ray1.transform.localScale, Vector3.zero, elapsedTime / 0.5f);
                Ray2.transform.localScale = Vector3.Lerp(Ray2.transform.localScale, Vector3.zero, elapsedTime / 0.5f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Ray1.SetActive(false);
            Ray2.SetActive(false);
            Ray1.transform.localScale = rayOriginalScale;
            Ray2.transform.localScale = rayOriginalScale;


            var error = false;
            foreach (var value in attributeValueListener)
            {
                var found = false;
                var correctValue = true;
                foreach (var m in MethodsListenersToRead)
                {


                    if (m.objectAttributeValue != null && value.className == m.className)
                    {
                        var classValue = Inventario.istanza.classi.Find(x => x.className == m.className);

                        if (classValue != null)
                        {

                            if (classValue.attributes.Find(x => x.attribute == value.attribute).visibility)
                            {
                                
                                //(string, string) tupla = m.objectAttributeValue.Find(x => x.Item1 == value.attribute && x.Item2 == value.value);
                                var tupla = m.objectAttributeValue.Find(x => x.Item1 == value.attribute && x.Item2.Equals(value.value, StringComparison.OrdinalIgnoreCase));

                                if (tupla != (null, null))
                                {
                                    found = true;
                                    break;
                                }
                                DoorMonitor.SetError("Attributo : " + value.attribute + " ha un valore errato");
                                ChangeTubeColor("Error");
                                found = false;
                                correctValue = false;
                                continue;
                            }
                            else
                            {
                                if (Teoria != null)
                                {
                                    TutorialCanvas.SetActive(true);
                                    Ocm.isTutorialStarted = true;
                                }
                                DoorMonitor.SetError("Attributo : " + value.attribute + " non accessibile perchè private");
                                ChangeTubeColor("Error");
                                correctValue = false;
                                continue;
                            }
                        }

                        else //� un attrbiuteNotPrinted
                        {
                            var tupla = m.objectAttributeValue.Find(x => x.Item1 == value.attribute && x.Item2 == value.value);
                            if (tupla != (null, null))
                            {
                                found = true;
                                continue;
                            }
                            else
                            {
                                tupla = m.objectAttributeValue.Find(x => x.Item1 == value.attribute && x.Item2 != value.value);
                                if (tupla != (null, null))
                                {
                                    DoorMonitor.SetError("Attributo : " + value.attribute + " ha un valore errato");
                                    ChangeTubeColor("Error");
                                    found = false;
                                    correctValue = false;
                                    continue;
                                }
                            }
                        }
                    }
                }

                if (!found)
                {
                    if (correctValue) DoorMonitor.SetError("Oggetto della classe  " + value.className + " non trovato");
                    ChangeTubeColor("Error");
                    error = true;
                }


            }
            if(!error)
                ApplyMethod();

            if(gameManager.isSeeing)
                ObjectCallCanvas.CloseInterface();

        }
        public void GetTeory()
        {
            Teoria.isActive = true;
            Teoria.Interact();
        }
        public override void ApplyMethod()
        {
            DoorMonitor.SetError("");
            var children = transform.Cast<Transform>().ToList();
            foreach (var child in children)
            {
                child.parent = null;
            }
            ChangeTubeColor("Getter");
            DoorAnimator.SetBool(Open, true);
            DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
        }
    }
}
