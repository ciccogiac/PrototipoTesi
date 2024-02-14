using System;
using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogStarter : MonoBehaviour
{
    private GameManager_Escape _gameManager;
    private StarterAssetsInputs _input;
    [SerializeField] private CinemachineVirtualCamera DialogCamera; 
    [SerializeField] private Message[] Messages;
    [SerializeField] private TMP_Text MessageText;
    [SerializeField] private TMP_Text CharacterName;
    [SerializeField] private Image CharacterImage;

    private int _counter;
    private bool _dialogUsed;
    private bool _dialogOpen;
    private bool _clickTriggered;
    private bool _typing;
    private IEnumerator _typingCoroutine;
    
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager_Escape>();
        _input = FindObjectOfType<StarterAssetsInputs>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_dialogUsed)
        {
            SetupDialogCanvasWithMessage(Messages[_counter]);
            _gameManager.ActivateDialogCanvas();
            DialogCamera.enabled = true;
            _dialogOpen = true;
        }
    }

    private void Update()
    {
        if (_dialogOpen)
        {
            if (_input.interact && !_clickTriggered)
            {
                _clickTriggered = true;
                if (_typing)
                    ForceMessage();
                else 
                    NextMessage();
            }
            if (!_input.interact && _clickTriggered)
            {
                _clickTriggered = false;
            }

            if (_input.skip)
            {
                EndDialog();
            }
        }
    }

    private void ForceMessage()
    {
        StopCoroutine(_typingCoroutine);
        MessageText.text = Messages[_counter].GetMessageText();
        _typing = false;
    }

    private void NextMessage()
    {
        _counter++;
        if (_counter < Messages.Length)
            SetupDialogCanvasWithMessage(Messages[_counter]);
        else
            EndDialog();
    }

    private void EndDialog()
    {
        _gameManager.DeactivateDialogCanvas();
        _gameManager.SwitchCameraToPrimary(DialogCamera);
        _dialogUsed = true;
        _dialogOpen = false;
    }

    private void SetupDialogCanvasWithMessage(Message m)
    {
        _typingCoroutine = TypeSentence(m.GetMessageText());
        StartCoroutine(_typingCoroutine);
        CharacterName.text = m.GetCharacter().GetName();
        CharacterImage.sprite = m.GetCharacter().GetImage();
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _typing = true;
        MessageText.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            MessageText.text += letter;
            yield return null;
        }
        _typing = false;
    }
}
