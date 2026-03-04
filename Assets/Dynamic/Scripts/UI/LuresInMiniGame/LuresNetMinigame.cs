using System.Collections.Generic;
using Fishs.Spawner;
using UnityEngine;

using Lure.Data;
using Manager.Lure;
using UnityEngine.UI;

namespace UI.Lures.Buttons
{
    [System.Serializable]
    public class LureButtonNetEntry
    {
        [Tooltip("L'appât qu'on va check pour activer ou désactiver le bouton")]
        public LureData LureToCheck;
        
        [Tooltip("L'appât qu'on va utiliser quand on clique sur le bouton")]
        public LureData LureToUse;
        
        public GameObject Button;
    }
}

namespace UI.Lures
{
    using Buttons;
   public class LuresNetMinigame : MonoBehaviour
   {
       [SerializeField] private GameObject luresButton;
       [SerializeField] private List<LureButtonNetEntry> lureButtons = new();

       [SerializeField] private FishMonoSpawner _spawner;
       
       private void Start()
       {
           CheckLureButton(false);
           SetupButtons();
       }
       
       private void SetupButtons()
       {
           foreach (var entry in lureButtons)
           {
               if (entry == null || entry.Button == null)
                   continue;
   
               var btn = entry.Button.GetComponent<Button>();
               if (btn == null)
                   continue;
   
               btn.onClick.RemoveAllListeners();
               
               btn.onClick.AddListener(() => OnLureButtonClicked(entry.LureToUse, entry.LureToCheck));
           }
       }
       
       private void OnLureButtonClicked(LureData lureToUse, LureData lureToCheck)
       {
           if (lureToUse == null || LureManager.Instance == null)
               return;
   
           bool used = false;
           var lureManager = LureManager.Instance;
   
           if (lureManager != null && lureToCheck != null)
           {
               used = lureManager.BoolUseLure(lureToCheck);
           }
   
           if (used)
           {
               _spawner.SetActiveLure(lureToUse);

               CheckAllLures(true);
               CheckLureButton(true);
           }
           Debug.Log("Used ? " + used);
       }
   
       public void CheckLureButton(bool lureUsed)
       {
           if (!LureManager.Instance.HasAnyLure() || lureUsed)
           {
               luresButton.SetActive(false);
               return;
           }
           luresButton.SetActive(true);
       }
   
       public void CheckAllLures(bool lureUsed)
       {
           if (lureUsed)
           {
               foreach (var lureButtonItem in lureButtons)
               {
                   lureButtonItem.Button.SetActive(false);
               }

               return;
           }
           foreach (var lureButtonItem in lureButtons)
           {
               if (lureButtonItem == null || lureButtonItem.Button == null)
                   continue;
   
               int count = 0;
               if (lureButtonItem.LureToCheck != null)
                   count = LureManager.Instance.GetLureCount(lureButtonItem.LureToCheck);
   
               bool shouldBeActive = count > 0;
               Debug.Log("Button : " + lureButtonItem.Button.name + " | Count : " + count + " | Should be active : " + shouldBeActive);
               lureButtonItem.Button.SetActive(shouldBeActive);
           }
       }
   } 
}

