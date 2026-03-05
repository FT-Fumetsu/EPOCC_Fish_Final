using System.Collections;
using Dialogue.Element;
using Manager.Pause;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _dialogueSpeakerText;
    [SerializeField] private float _textDisplayDelay = 0.05f;
    
    [Header("Other UI Prefabs"), Tooltip("Les UI qui doivent ne pas apparaître pendant le dialogue")]
    [SerializeField] private GameObject[] _otherUIPrefabs;

    private DialogueSequence _currentSequence;
    private int _currentIndex;

    private System.Action _onDialogueEnd;

    private string _currentText;
    private GameObject _currentUI;

    private void Start()
    {
        _dialogueText.text = string.Empty;
    }

    public void PlaySequence(DialogueSequence sequence, System.Action onEnd = null)
    {
        foreach (var uiItem in _otherUIPrefabs)
        {
            uiItem.SetActive(false);
        }
        
        _currentSequence = sequence;
        _currentIndex = 0;

        _onDialogueEnd = onEnd;

        PauseManager.Instance?.TogglePause(true);

        PlayCurrentElement();
    }

    private void PlayCurrentElement()
    {
        if (_currentIndex >= _currentSequence.elements.Count)
        {
            EndDialogue();
            return;
        }
        _dialogueText.text = string.Empty;

        _currentSequence.elements[_currentIndex].Execute(this);
    }

    public void ShowText(string text, string speaker)
    {
        _dialoguePanel.SetActive(true);
        _dialogueSpeakerText.text = speaker;
        _currentText = text;
        _dialogueText.text = text;
    }

    IEnumerator TypeLine(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(_textDisplayDelay);
        }
    }

    public void ShowUI(GameObject uiPrefab)
    {
        _dialoguePanel.SetActive(false);
        _currentUI = Instantiate(uiPrefab);
    }

    public void Next()
    {
        Destroy(_currentUI);
        _currentIndex++;
        PlayCurrentElement();
    }

    private void EndDialogue()
    {
        _dialoguePanel.SetActive(false);
        PauseManager.Instance?.TogglePause(false);
        
        foreach (var uiItem in _otherUIPrefabs)
        {
            uiItem.SetActive(true);
        }

        // Invoke the optional end callback and clear it
        _onDialogueEnd?.Invoke();
        _onDialogueEnd = null;
    }

    public void TryToSkipDialogue()
    {
        if (_currentUI)
        {
            Next();
        }
        else if (_dialogueText.text == _currentText)
        {
            Next();
        }
        else
        {
            StopAllCoroutines();
            _dialogueText.text = _currentText;
        }
    }
}