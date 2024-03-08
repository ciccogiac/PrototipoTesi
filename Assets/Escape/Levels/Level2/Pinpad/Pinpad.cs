using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Escape.Levels.Level2
{
    [RequireComponent(typeof(AudioSource))]
    public class Pinpad : MethodListener
    {
        [SerializeField] private AudioClip OpeningSound;
        [SerializeField] private AudioClip ClosingSound;
        [SerializeField] private AudioClip WrongCodeSound;
        [SerializeField] private Animator Sportello;
        [FormerlySerializedAs("Contained")] [SerializeField] private GameObject ContainedReadObject;
        [SerializeField] private BoxCollider colliderArmadietto;
        private static readonly int Open = Animator.StringToHash("open");
        private AudioSource _audioSource;

        [SerializeField] private LevelHint LevelHint;

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
            DatiPersistenti.istanza.methodsListeners.Remove(methodListenerID);
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

            if(LevelHint.hintCounter == 3)
                LevelHint.nextHint(4);

            ApplyMethod();
            return true;
        }

        private IEnumerator WaitForSportelloToBeClosedAndDeactivateContained()
        {
            yield return new WaitUntil(() => Sportello.GetCurrentAnimatorStateInfo(0).IsName("SportelloClosed"));
            colliderArmadietto.enabled = true;
            if (ContainedReadObject != null) ContainedReadObject.SetActive(false);
        }

        public override void ApplyMethod()
        {
            if (ContainedReadObject != null) ContainedReadObject.SetActive(true);
            if (!Sportello.GetBool(Open)) PlayAudioClip(OpeningSound);
            Sportello.SetBool(Open, true);
            DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
            colliderArmadietto.enabled = false;
        }
    }
}
