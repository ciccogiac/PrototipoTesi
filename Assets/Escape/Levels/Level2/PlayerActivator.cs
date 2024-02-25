using System;
using UnityEngine;

namespace Escape.Levels.Level2
{
    public class PlayerActivator : MonoBehaviour
    {
        [SerializeField] private GameObject LongSpeech;
        [SerializeField] private GameObject Dialog;

        private void Update()
        {
            if (!LongSpeech.activeSelf)
            {
                Dialog.SetActive(true);
                Destroy(this);
            }
        }
    }
}
