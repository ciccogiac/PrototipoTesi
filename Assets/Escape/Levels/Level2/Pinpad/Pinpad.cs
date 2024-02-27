using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level2
{
    [RequireComponent(typeof(AudioSource))]
    public class Pinpad : MethodListener
    {
        [SerializeField] private AudioClip OpeningSound;
        [SerializeField] private AudioClip ClosingSound;
        [SerializeField] private AudioClip WrongCodeSound;
        [SerializeField] private Animator Sportello;
        [SerializeField] private GameObject Contained;
        private static readonly int Open = Animator.StringToHash("open");
        private AudioSource _audioSource;

        public override void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void PlayAudioClip(AudioClip clip)
        {
            if (_audioSource != null)
            {
                _audioSource.clip = clip;
                _audioSource.Play();
            }
        }

        public override void Getter(List<(string, string)> objectValue)
        {
            if (Sportello.GetBool(Open)) PlayAudioClip(ClosingSound);
            Sportello.SetBool(Open, false);
            StartCoroutine(WaitForSportelloToBeClosedAndDeactivateContained());
        }
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
                        PlayAudioClip(WrongCodeSound);
                        StartCoroutine(WaitForSportelloToBeClosedAndDeactivateContained());
                        return false;
                    }
                }
                else
                {
                    Sportello.SetBool(Open, false);
                    PlayAudioClip(WrongCodeSound);
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
            if (Contained != null && Contained.GetComponent<Clue>() != null) Contained.SetActive(true);
            if (!Sportello.GetBool(Open)) PlayAudioClip(OpeningSound);
            Sportello.SetBool(Open, true);
            DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
        }
    }
}
