using UnityEngine;

public class UiDialogueSkipper : MonoBehaviour
{
    public void SkipUiDialogue()
    {
        var displayer = FindFirstObjectByType<DialogueDisplayer>();

        if (displayer == null)
        {
            return;
        }
        displayer.Next();
    }
}
