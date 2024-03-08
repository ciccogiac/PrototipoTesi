using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level4.Vetrinetta
{
    public class PinpadVetrinetta : MethodListener
    {
        [SerializeField] private AudioClip OpeningSound;
        [SerializeField] private AudioClip ClosingSound;
        [SerializeField] private AudioClip WrongCodeSound;
        [SerializeField] private Animator Sportello;
        [SerializeField] private AudioSource AudioSource;
        private static readonly int Open = Animator.StringToHash("open");
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
            if (Sportello.GetBool(Open)) PlayAudioClip(ClosingSound);
            Sportello.SetBool(Open, false);
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
                        Sportello.SetBool(Open, false);
                        PlayAudioClip(WrongCodeSound);
                        return false;
                    }
                }
                else
                {
                    Sportello.SetBool(Open, false);
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
            if (!Sportello.GetBool(Open))
            {
                if (!DatiPersistenti.istanza.methodsListeners.Contains(methodListenerID))
                {
                    PlayAudioClip(OpeningSound);
                    DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
                    _comingFromHere = false;
                }
            }
            Sportello.SetBool(Open, true);
        }
    }
}
