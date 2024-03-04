using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public static PlayMusic istanza;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] clips;

    public void PlaySong(int i)
    {

        if(i < clips.Length && i>-1)
        {
            _audioSource.clip = clips[i];
            _audioSource.Play();
        }
    }

    void Awake()
    {
        // Assicurati che esista una sola istanza di questo oggetto
        if (istanza == null)
        {
            istanza = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
