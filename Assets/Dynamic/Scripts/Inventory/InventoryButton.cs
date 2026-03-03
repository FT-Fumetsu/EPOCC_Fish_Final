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
                Debug.Log("Open Inventory");
                OpenInventory();
                return;
            }
            
            LaunchDialogue();
            Debug.Log("Launched Inventory Dialogue");
        }

        private void OpenInventory()
        {
            if (_inventoryPanel == null)
            {
                Debug.Log("Inventory Panel not found!");
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