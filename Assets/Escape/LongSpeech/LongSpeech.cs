using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class LongSpeech : MonoBehaviour
{
    private GameManager_Escape _gameManager;
    private StarterAssetsInputs _input;
    [SerializeField] private TMP_Text CampoTesto;
    [SerializeField][TextArea(5, 20)] private string[] Testi;
    private bool _clickTriggered;
    private int _counter = -1;
    private bool _typing;
    private IEnumerator _typingCoroutine;
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager_Escape>();
        _input = FindObjectOfType<StarterAssetsInputs>();
    }

    private void OnEnable()
    {
        _counter = -1;
        NextLongSpeech();
    }

    private void Update()
    {
        if (_input.interact && !_clickTriggered)
        {
            _clickTriggered = true;
            if (_typing)
                ForceLongSpeech();
            else 
                NextLongSpeech();
        }
        if (!_input.interact && _clickTriggered)
        {
            _clickTriggered = false;
        }

        if (_input.skip)
        {
            _input.skip = false;
            EndLongSpeech();
        }
    }

    private void ForceLongSpeech()
    {
        StopCoroutine(_typingCoroutine);
        CampoTesto.text = Testi[_counter];
        _typing = false;
    }

    private void NextLongSpeech()
    {
        _counter++;
        if (_counter < Testi.Length)
            SetupLongSpeechCanvasWithSpeech(Testi[_counter]);
        else
            EndLongSpeech();
    }
    
    private void EndLongSpeech()
    {
        _gameManager.DeactivateLongSpeechCanvas(gameObject);
        
    }
    private void SetupLongSpeechCanvasWithSpeech(string speech)
    {
        _typingCoroutine = TypeSentence(speech);
        StartCoroutine(_typingCoroutine);
    }
    
    private IEnumerator TypeSentence(string sentence)
    {
        _typing = true;
        CampoTesto.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            CampoTesto.text += letter;
            yield return null;
        }
        _typing = false;
    }
}
