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
    [SerializeField] private float CameraSwitchDuration = 1f;

    private int _counter = -1;
    public bool _dialogUsed;
    private bool _dialogOpen;
    private bool _clickTriggered;
    private bool _backClickTriggered;
    private bool _typing;
    private IEnumerator _typingCoroutine;
    private CinemachineBlendDefinition _previousCinemachineBlendDefinition;

    [SerializeField] LevelManager _levelManager;
    [SerializeField] LevelHint _levelHint;
    [SerializeField] bool _isActivationObjectDialog = false;
    [SerializeField] bool _isTeoryDialog = false;
    [SerializeField] bool _isHintDialog = false;
    [SerializeField] int _hintNumber;
    [SerializeField] private GameObject BackButtonImg;

    public int _dialogID;
    private bool _dialogFinished;
    
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager_Escape>();
        _input = FindObjectOfType<StarterAssetsInputs>();
        _levelManager = FindObjectOfType<LevelManager>();
	    if (_isHintDialog)
                _levelHint = FindObjectOfType<LevelHint>();
    }

    public bool GetDialogOpen()
    {
        return _dialogOpen;
    }

    public bool GetDialogFinished()
    {
        return _dialogFinished;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !_dialogUsed && _levelHint.hintCounter == _hintNumber)
        {
            _dialogUsed = true;
            _previousCinemachineBlendDefinition =
                Camera.main!.transform.GetComponent<CinemachineBrain>().m_DefaultBlend;
            if (Math.Abs(_previousCinemachineBlendDefinition.m_Time - CameraSwitchDuration) > .1f)
            {
                Camera.main!.transform.GetComponent<CinemachineBrain>().m_DefaultBlend =
                    new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, CameraSwitchDuration);
            }
            _gameManager.ActivateDialogCanvas();
            DialogCamera.enabled = true;
            _dialogOpen = true;
            NextMessage();
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
                _input.skip = false;
                EndDialog();
            }

            if (_counter > 0)
            {
                if (!BackButtonImg.activeSelf)
                {
                    BackButtonImg.SetActive(true);
                }
                if (_input.back && !_backClickTriggered)
                {
                    _backClickTriggered = true;
                    PreviousMessage();
                }

                if (!_input.back && _backClickTriggered)
                {
                    _backClickTriggered = false;
                }
            }
            else
            {
                _backClickTriggered = false;
                BackButtonImg.SetActive(false);
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

    private void PreviousMessage()
    {
        if (_typing)
            ForceMessage();
        _counter--;
        SetupDialogCanvasWithMessage(Messages[_counter]);
    }

    private void EndDialog()
    {
        _input.interact = false;
        _gameManager.DeactivateDialogCanvas();
        //_gameManager.SwitchCameraToPrimary(DialogCamera);
        _dialogUsed = true;
        _dialogFinished = true;
        _dialogOpen = false;

        if (_isActivationObjectDialog)
            _levelManager.ActivateObjects();
        if (_isTeoryDialog)
        {
            _gameManager.DialogCamera = DialogCamera;
            GetTeory();
        }
     
        else
            _gameManager.SwitchCameraToPrimary(DialogCamera);


        if (_isHintDialog && _levelHint != null)
            _levelHint.nextHint(_hintNumber + 1 );

        DatiPersistenti.istanza.dialogUsed.Add(_dialogID);

        if (Math.Abs(_previousCinemachineBlendDefinition.m_Time - CameraSwitchDuration) > .1f)
        {
            IEnumerator ResetCinemachineBlendDefinitionAfterDuration()
            {
                yield return new WaitForSeconds(CameraSwitchDuration);
                Camera.main!.transform.GetComponent<CinemachineBrain>().m_DefaultBlend =
                    _previousCinemachineBlendDefinition;
            }

            StartCoroutine(ResetCinemachineBlendDefinitionAfterDuration());
        }
    }

    public void GetTeory()
    {
        Clue teoria = GetComponentInChildren<Clue>();
        if(teoria != null && teoria.clueType == Clue.ClueType.Teoria)
        {
            teoria.Interact();
        }
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