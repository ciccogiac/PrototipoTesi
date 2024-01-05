using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogScript : MonoBehaviour
{
    [SerializeField] private GameObject DialogTriggerText;
    [SerializeField] private GameObject DialogPanel;
    [SerializeField] private TMP_Text DialogText;
    [SerializeField] private string[] IntroMessages;
    [SerializeField] private string[] Level1Messages;
    [SerializeField] private string[] Level2Messages;
    [SerializeField] private string[] Level3Messages;

    private bool _dialogTriggered;
    private string[][] _messages;
    private int _level = 0;
    private int _message = 0;
    // Start is called before the first frame update
    void Start()
    {
        _messages = new[]
        {
            IntroMessages,
            Level1Messages,
            Level2Messages,
            Level3Messages
        };
        DialogText.text = _messages[_level][_message];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            _dialogTriggered = true;
            DialogTriggerText.SetActive(false);
            DialogPanel.SetActive(true);
        }

        if (_dialogTriggered && Input.GetKeyUp(KeyCode.Space))
        {
            _message++;
            if (_message == _messages[_level].Length)
            {
                _message = 0;
                if (_level < _messages.Length - 1)
                    _level++;
                _dialogTriggered = false;
                DialogPanel.SetActive(false);
                DialogTriggerText.SetActive(true);
            }
            DialogText.text = _messages[_level][_message];
        }
    }
}
