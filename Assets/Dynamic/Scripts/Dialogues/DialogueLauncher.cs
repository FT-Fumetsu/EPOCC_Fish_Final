using UnityEngine;
using UnityEngine.Serialization;

public class DialogueLauncher : MonoBehaviour
{
    [SerializeField, TextArea(2,5)] private string[] _textArray;

    [SerializeField] private bool _isLanchedOnStart;

    [SerializeField] private bool _isReusable;
    
    private bool _hasBeenLaunched;
    
    public bool IsReusable => _isReusable;
    
    private void Start()
    {
        if (!_isLanchedOnStart)
            return;
        
        LaunchDialogue();
    }

    public void LaunchDialogue()
    {
        if (_hasBeenLaunched && !_isReusable)
            return;
        
        _hasBeenLaunched = true;
        
        var dialogueDisplayer = FindFirstObjectByType<DialogueDisplayer>();

        if (dialogueDisplayer == null)
        {
            Debug.LogError("DialogueLauncher: No DialogueDisplayer found in the scene.");
            return;
        }
        
        dialogueDisplayer.DisplayDialogue(_textArray);
    }
}
