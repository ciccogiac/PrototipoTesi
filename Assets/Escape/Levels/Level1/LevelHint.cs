using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelHint : MonoBehaviour
{
    [SerializeField] GameObject HintBox;
    [SerializeField] TextMeshProUGUI hintText;

    public Hint[] hint;

    public int hintCounter = 0;
    [SerializeField] int hintDuration = 10;

    [SerializeField] bool _IsHintActive ;

    public void StartHintCounter()
    {
        if (_IsHintActive && hintCounter < hint.Length && hint[hintCounter].hintText != "")
                StartCoroutine(CheckHint(hint[hintCounter].hintFirstTimer));
    }

    public void nextHint(int hintNumber)
    {

        if (hintNumber > hintCounter && _IsHintActive)
        {
            StopAllCoroutines();
            HintBox.SetActive(false);
            hintCounter = hintNumber;

            if (hintCounter < hint.Length && hint[hintCounter].hintText != "")
                StartCoroutine(CheckHint(hint[hintCounter].hintFirstTimer));
        }
    }

    IEnumerator CheckHint(int duration)
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(ActivateHint());

    }

    IEnumerator ActivateHint()
    {
        if (hintCounter < hint.Length && hint[hintCounter].hintText != "")
            HintBox.SetActive(true);
        hintText.text = hint[hintCounter].hintText;
        yield return new WaitForSeconds(hintDuration);
        HintBox.SetActive(false);
        StartCoroutine(CheckHint(hint[hintCounter].hintRepeatTimer));

    }

}





[Serializable]
public class Hint
{
    public string hintText;
    public int hintFirstTimer;
    public int hintRepeatTimer;
}
