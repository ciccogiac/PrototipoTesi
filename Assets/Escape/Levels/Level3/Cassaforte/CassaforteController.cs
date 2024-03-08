using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level3.Cassaforte
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class CassaforteController : MethodListener
    {
        [SerializeField] private AudioClip OpeningSound;
        [SerializeField] private AudioClip ClosingSound;
        [SerializeField] private AudioClip WrongCodeSound;
        [SerializeField] private Animator Animator;
        [SerializeField] private AudioSource AudioSource;
        private static readonly int Open = Animator.StringToHash("Open");
        private bool _comingFromHere;

        private void PlayAudioClip(AudioClip clip)
        {
            if (AudioSource != null)
            {
                AudioSource.clip = clip;
                AudioSource.Play();
            }
        }
        public override void Getter(List<(string, string)> objectValue)
        {
            if (Animator.GetBool(Open)) PlayAudioClip(ClosingSound);
            Animator.SetBool(Open, false);
            DatiPersistenti.istanza.methodsListeners.Remove(methodListenerID);
        }
        public override bool Method(List<(string, string)> objectValue)
        {
            foreach (var value in attributeValueListener)
            {
                var tupla = objectValue.Find(x => x.Item1 == value.attribute);
                if (tupla != (null, null))
                {
                    if (tupla.Item2 != value.value)
                    {
                        Animator.SetBool(Open, false);
                        PlayAudioClip(WrongCodeSound);
                        return false;
                    }
                }
                else
                {
                    Animator.SetBool(Open, false);
                    PlayAudioClip(WrongCodeSound);
                    return false;
                }
            }
            _comingFromHere = true;
            ApplyMethod();
            return true;
        }
        public override void ApplyMethod()
        {
            if (!Animator.GetBool(Open)) {
                if (!DatiPersistenti.istanza.methodsListeners.Contains(methodListenerID) || _comingFromHere)
                {
                    PlayAudioClip(OpeningSound);
                    DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
                    _comingFromHere = false;
                }
            }
            Animator.SetBool(Open, true);
        }
    }
}
