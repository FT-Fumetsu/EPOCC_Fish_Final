using Dialogue.Element;
using Singletons;
using UnityEngine;

namespace Tutorial.Manager
{
    [RequireComponent(typeof(DialogueLauncher))]
    public class TutorialManager : PersistentMonoSingleton<TutorialManager>
    {
        [SerializeField] private DialogueSequence _tutorialSequence;

        [SerializeField] private bool _isTutorialFinished;
        [SerializeField] private bool _isFirstTutorialFinished;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        
        public bool IsFirstTutorialFinished
        {
            get => _isFirstTutorialFinished;
            set => _isFirstTutorialFinished = value;
        }
        
        public bool IsTutorialFinished
        {
            get => _isTutorialFinished;
            set => _isTutorialFinished = value;
        }

        private void Start()
        {
            if (_isFirstTutorialFinished)
                return;
            
            var dialogueLauncher = GetComponent<DialogueLauncher>();
            dialogueLauncher.LaunchDialogue(OnTutorialDialogueEnd);
        }
        
        private void OnTutorialDialogueEnd()
        {
            IsFirstTutorialFinished = true;
        }
    }
}