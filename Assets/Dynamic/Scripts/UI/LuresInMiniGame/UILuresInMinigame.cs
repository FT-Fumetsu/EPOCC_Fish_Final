using System.Collections.Generic;
using UnityEngine;

using Lure.Data;
using Manager.Lure;
using UnityEngine.UI;

namespace UI.Lures.Buttons
{
    [System.Serializable]
    public class LureButtonEntry
    {
        public LureData lure;
        public GameObject button;
    }
}


namespace UI.Lures
{
    using Buttons;
   public class UILuresInMinigame : MonoBehaviour
   {
       [SerializeField] private GameObject luresButton;
       [SerializeField] private List<LureButtonEntry> lureButtons = new();
       
       void Start()
       {
           CheckLureButton(false);
           SetupButtons();
       }
       
       private void SetupButtons()
       {
           foreach (var entry in lureButtons)
           {
               if (entry == null || entry.button == null)
                   continue;
   
               var btn = entry.button.GetComponent<Button>();
               if (btn == null)
                   continue;
   
               btn.onClick.RemoveAllListeners();
   
               var localLure = entry.lure;
   
               btn.onClick.AddListener(() => OnLureButtonClicked(localLure));
           }
       }
       
       private void OnLureButtonClicked(LureData lure)
       {
           if (lure == null || LureManager.Instance == null)
               return;
   
           bool used = false;
           var lureManager = LureManager.Instance;
   
           if (lureManager != null && lure != null)
           {
               used = lureManager.BoolUseLure(lure); // appel direct, compile-time checked
           }
   
           if (used)
           {               
               var spawnerArray = FindObjectsByType<Fish.Spawner.FishSpawner>(FindObjectsSortMode.None);
               if (spawnerArray != null)
               {
                   foreach(var spawnerItem in spawnerArray)
                   {
                       spawnerItem.SetActiveLure(lure);
                   }
               }
               else
               {
                   var spawner = FindAnyObjectByType<Fishs.Spawner.FishMonoSpawner>();
                   spawner.SetActiveLure(lure);
               }
               
               
               CheckAllLures(true);
               CheckLureButton(true);
           }
       }
   
       private void CheckLureButton(bool lureUsed)
       {
           if (!LureManager.Instance.HasAnyLure() || lureUsed)
           {
               luresButton.SetActive(false);
               return;
           }
           luresButton.SetActive(true);
       }
   
       private void CheckAllLures(bool lureUsed)
       {
           if (lureUsed)
           {
               foreach (var lureButtonItem in lureButtons)
               {
                   lureButtonItem.button.SetActive(false);
               }

               return;
           }
           foreach (var lureButtonItem in lureButtons)
           {
               if (lureButtonItem == null || lureButtonItem.button == null)
                   continue;
   
               int count = 0;
               if (lureButtonItem.lure != null)
                   count = LureManager.Instance.GetLureCount(lureButtonItem.lure);
   
               bool shouldBeActive = count > 0;
               lureButtonItem.button.SetActive(shouldBeActive);
           }
       }
   } 
}

