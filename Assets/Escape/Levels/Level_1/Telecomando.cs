using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telecomando : MethodListener
{
    [SerializeField] Clue Teoria;
    [SerializeField] private Animator TVAnimator;
    [SerializeField] private GameObject Chiave;
    private static readonly int OpenPanel = Animator.StringToHash("OpenPanel");

    public override bool Method(List<(string, string)> objectValue)
    {
        foreach (var value in attributeValueListener)
        {
            var tupla = objectValue.Find(x => x.Item1 == value.attribute);
            if (tupla != (null, null))
            {
                if (tupla.Item2 != value.value)
                {
                    ClosePanelAnimation();
                    return false;
                }
            }
            else
            {
                ClosePanelAnimation();
                return false;
            }



        }

        ApplyMethod();
        return true;
    }

    public override void ApplyMethod()
    {
        OpenPanelAnimation();
        DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
    }

    private void OpenPanelAnimation()
    {
        if (Chiave != null) Chiave.SetActive(true);
        TVAnimator.enabled = true;
        TVAnimator.SetBool(OpenPanel, true);
        IEnumerator ActivateChiave()
        {
            yield return new WaitUntil(() => TVAnimator.GetCurrentAnimatorStateInfo(0).IsName("SchermoAnimator"));
            yield return new WaitUntil(() => TVAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
            TVAnimator.enabled = false;
        }
        StartCoroutine(ActivateChiave());
    }

    private void ClosePanelAnimation()
    {
        TVAnimator.enabled = true;
        TVAnimator.SetBool(OpenPanel, false);
        IEnumerator DisableAnimator()
        {
            yield return new WaitUntil(() => TVAnimator.GetCurrentAnimatorStateInfo(0).IsName("SchermoIdle"));
            TVAnimator.enabled = false;
            if (Chiave != null) Chiave.SetActive(false);
        }
        StartCoroutine(DisableAnimator());
    }
}
