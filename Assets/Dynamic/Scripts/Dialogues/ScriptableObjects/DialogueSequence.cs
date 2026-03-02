using System.Collections.Generic;
using UnityEngine;

namespace Dialogue.Element
{
    [CreateAssetMenu(fileName = "DialogueSequence", menuName = "ScriptableObjects/Dialogue/Sequence")]
    public class DialogueSequence : ScriptableObject
    {
        public List<DialogueElement> elements;
    }
}