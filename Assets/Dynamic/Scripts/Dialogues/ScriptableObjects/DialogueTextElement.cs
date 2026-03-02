using UnityEngine;

namespace Dialogue.Element
{
    [CreateAssetMenu(fileName = "TextElement", menuName = "ScriptableObjects/TextElement")]
    public class DialogueTextElement : DialogueElement
    {
        [TextArea(2, 5)] public string text;
        public string speakerName;

        public override void Execute(DialogueDisplayer displayer)
        {
            displayer.ShowText(text, speakerName);
        }
    }
}