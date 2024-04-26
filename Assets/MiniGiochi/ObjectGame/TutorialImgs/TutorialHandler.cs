using UnityEngine;
using UnityEngine.Serialization;

public class TutorialHandler : MonoBehaviour
{
    private int _currentIndex = 0;
    [SerializeField] private GameObject GameManager;
    [FormerlySerializedAs("backButton")] [SerializeField] private GameObject BackButton;
    [FormerlySerializedAs("nextButton")] [SerializeField] private GameObject NextButton;

    public void CloseTutorial()
    {
        _currentIndex = 0;
        UpdatePage();
        var gameManagerObjectGame = GameManager.GetComponent<GameManager_ObjectGame>();
        if (gameManagerObjectGame != null) gameManagerObjectGame.EndTutorial();
        var gameManagerClassGame = GameManager.GetComponent<GameManager_ClassGame>();
        if (gameManagerClassGame != null) gameManagerClassGame.EndTutorial();
    }
    public void IncrementIndex()
    {
        _currentIndex++;
        UpdatePage();
    }
    public void DecrementIndex()
    {
        _currentIndex--;
        UpdatePage();
    }
    private void UpdatePage()
    {
        BackButton.SetActive(!IsFirst());
        NextButton.SetActive(!IsLast());
        if (_currentIndex >= 0 && _currentIndex < transform.childCount)
        {
            for (var index = 0; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(index == _currentIndex);
            }
        }
    }
    private bool IsFirst()
    {
        return _currentIndex == 0;
    }
    private bool IsLast()
    {
        return _currentIndex == transform.childCount - 1;
    }
}
