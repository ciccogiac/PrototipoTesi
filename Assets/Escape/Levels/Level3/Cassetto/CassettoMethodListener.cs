using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level3.Cassetto
{
    [RequireComponent(typeof(AudioSource))]
    public class CassettoMethodListener : MethodListener
    {
        [SerializeField] private Animator CassettoAnimator;
        [SerializeField] private AudioClip OpeningSound;
        [SerializeField] private AudioClip ClosingSound;
        [SerializeField] private GameObject Contained;
        [SerializeField] private AudioSource AudioSource;
        private static readonly int Open = Animator.StringToHash("Open");
        private bool _comingFromHere;
        private IEnumerator WaitForCassettoToBeClosedAndDeactivateContained()
        {
            yield return new WaitUntil(() => CassettoAnimator.GetCurrentAnimatorStateInfo(0).IsName("CassettoClosed"));
            if (Contained != null) Contained.SetActive(false);
        }

        public override void Getter(List<(string, string)> objectValue)
        {
            if (CassettoAnimator.GetBool(Open)) PlayAudioClip(ClosingSound);
            CassettoAnimator.SetBool(Open, false);
            DatiPersistenti.istanza.methodsListeners.Remove(methodListenerID);
            StartCoroutine(WaitForCassettoToBeClosedAndDeactivateContained());
        }

        public override bool Method(List<(string, string)> objectValue)
        {
            _comingFromHere = true;
            ApplyMethod();
            return true;
        }
        private void PlayAudioClip(AudioClip clip)
        {
            if (AudioSource != null)
            {
                AudioSource.clip = clip;
                AudioSource.Play();
            }
        }

        public override void ApplyMethod()
        {
            if (Contained != null && Contained.GetComponent<Clue>() != null) Contained.SetActive(true);
            if (!CassettoAnimator.GetBool(Open))
            {
                if (!DatiPersistenti.istanza.methodsListeners.Contains(methodListenerID) || _comingFromHere)
                {
                    PlayAudioClip(OpeningSound);
                    DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
                    _comingFromHere = false;
                }
            }
            CassettoAnimator.SetBool(Open, true);
        }
    }
}
