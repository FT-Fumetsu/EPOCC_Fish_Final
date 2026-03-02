using UnityEngine;

namespace Dialogue.Element
{
    [CreateAssetMenu(fileName = "UIElement", menuName = "ScriptableObjects/UI Element")]
    public class DialogueUIElement : DialogueElement
    {
        public GameObject uiPrefab;

        public override void Execute(DialogueDisplayer displayer)
        {
            displayer.ShowUI(uiPrefab);
        }
    }
}