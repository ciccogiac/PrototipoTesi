using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level2
{
    public class Pinpad : MethodListener
    {
        [SerializeField] private Animator Sportello;
        [SerializeField] private GameObject Contained;
        private static readonly int Open = Animator.StringToHash("open");

        public override bool Method(List<(string, string)> objectValue)
        {
            foreach (var value in attributeValueListener)
            {
                (string, string) tupla = objectValue.Find(x => x.Item1 == value.attribute);
                if (tupla != (null, null))
                {
                    if (tupla.Item2 != value.value)
                    {
                        Sportello.SetBool(Open, false);
                        StartCoroutine(WaitForSportelloToBeClosedAndDeactivateContained());
                        return false;
                    }
                }
                else
                {
                    Sportello.SetBool(Open, false);
                    StartCoroutine(WaitForSportelloToBeClosedAndDeactivateContained());
                    return false;
                }
            


            }

            ApplyMethod();
            return true;
        }

        private IEnumerator WaitForSportelloToBeClosedAndDeactivateContained()
        {
            yield return new WaitUntil(() => Sportello.GetCurrentAnimatorStateInfo(0).IsName("SportelloClosed"));
            if (Contained != null) Contained.SetActive(false);
        }

        public override void ApplyMethod()
        {
            if (Contained != null) Contained.SetActive(true);
            Sportello.SetBool(Open, true);
            DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
        }
    }
}
