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
        Debug.Log("Skipped");
        displayer.Next();
    }
}
