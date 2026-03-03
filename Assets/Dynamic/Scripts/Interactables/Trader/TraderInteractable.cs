using Dialogue.Element;
using Manager.Fishs;
using UnityEngine;
using Tutorial.Manager;

namespace Interactables.Trader
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(DialogueLauncher))]
    public class TraderInteractable : InteractableBase
    {
        [SerializeField] private GameObject _tradeUi;
        [SerializeField] private GameObject _lureTradeUi;
        [SerializeField] private GameObject _codexUi;
        [SerializeField] private GameObject _tradeOrCodexChoiceUi;

        [SerializeField] private GameObject _mobileControls;

        [Header("Dialogues")]
        [SerializeField] private DialogueSequence _tutorialDialogue;
        [SerializeField] private DialogueSequence _notEnoughFishDialogue;
        [SerializeField] private DialogueSequence _baseDialogue;
        
        private DialogueLauncher _dialogueLauncher;

        private void Start()
        {
            _dialogueLauncher = GetComponent<DialogueLauncher>();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void Interact()
        {
            if(FishsManager.Instance?.FishsCount == 0 && TutorialManager.Instance?.IsTutorialFinished != true)
            {
                _dialogueLauncher.Sequence = _notEnoughFishDialogue;
                _dialogueLauncher.LaunchDialogue();
            }
            
            else if (TutorialManager.Instance?.IsTutorialFinished != true)
            {
                _dialogueLauncher.Sequence = _tutorialDialogue;
                _dialogueLauncher.LaunchDialogue(OnTutorialDialogueEnd);
            }
            else
            {
                _dialogueLauncher.Sequence = _baseDialogue;
                _dialogueLauncher.LaunchDialogue(OpenUiChoice);
            }
        }

        public void OpenTradeUi()
        {
            if (!_tradeUi) 
                return;
            
            _tradeUi.SetActive(true);
            _tradeOrCodexChoiceUi.SetActive(false);
            _lureTradeUi.SetActive(false);
            if (_mobileControls)
            {
                _mobileControls.SetActive(false);
            }
        }

        public void OpenCodexUi()
        {
            _tradeOrCodexChoiceUi.SetActive(false);
            _codexUi.SetActive(true);
        }

        private void OpenUiChoice()
        {
            if (!_tradeOrCodexChoiceUi)
                return;
            
            if (_mobileControls)
            {
                _mobileControls.SetActive(false);
            }
            
            _tradeOrCodexChoiceUi.SetActive(true);
        }
        
        private static void OnTutorialDialogueEnd()
        {
            TutorialManager.Instance.IsTutorialFinished = true;
        }
    }
}
