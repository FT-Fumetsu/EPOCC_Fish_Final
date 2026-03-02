using Dialogue.Element;
using UnityEngine;

public class DialogueLauncher : MonoBehaviour
{
    [SerializeField] private DialogueSequence _sequence;
    [SerializeField] private bool _isLanchedOnStart;
    [SerializeField] private bool _isReusable;

    public bool IsReusable => _isReusable;
    
    private bool _hasBeenLaunched;

    private void Start()
    {
        if (_isLanchedOnStart)
            LaunchDialogue();
    }

    public void LaunchDialogue()
    {
        if (_hasBeenLaunched && !_isReusable)
            return;

        _hasBeenLaunched = true;

        var displayer = FindFirstObjectByType<DialogueDisplayer>();

        if (displayer == null)
        {
            Debug.LogError("No DialogueDisplayer found.");
            return;
        }

        displayer.PlaySequence(_sequence);
    }
}