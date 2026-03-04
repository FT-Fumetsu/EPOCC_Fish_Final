using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(DialogueLauncher))]
    public class InventoryButton: MonoBehaviour
    {
        [SerializeField] private InventoryMenu _inventoryPanel;
        
        private DialogueLauncher _dialogueLauncher;
        
        private void Start()
        {
            _dialogueLauncher = GetComponent<DialogueLauncher>();
        }
        
        public void OnButtonPressed()
        {
            if (InventoryManager.Instance.IsTutorialFinished == true)
            {
                OpenInventory();
                return;
            }
            
            LaunchDialogue();
        }

        private void OpenInventory()
        {
            if (_inventoryPanel == null)
            {
                return;
            }
            
            if(!InventoryManager.Instance.IsTutorialFinished)
                InventoryManager.Instance.IsTutorialFinished = true;
            
            _inventoryPanel.gameObject.SetActive(true);
        }

        private void LaunchDialogue()
        {
            _dialogueLauncher.LaunchDialogue(OpenInventory);
        }
    }
}