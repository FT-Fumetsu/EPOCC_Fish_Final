using Manager.Pause;
using TMPro;
using UnityEngine;

public class DialogueDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    public void DisplayDialogue(string[] textArray)
    {
        PauseManager.Instance?.TogglePause(true);
        _dialoguePanel.SetActive(true);
        _dialogueText.text = textArray[0];
    }

    private void UpdateDialogueText()
    {
        
    }
}
