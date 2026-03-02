using UnityEngine;

namespace Dialogue.Element
{
    public abstract class DialogueElement : ScriptableObject
    {
        public abstract void Execute(DialogueDisplayer displayer);
    }
}