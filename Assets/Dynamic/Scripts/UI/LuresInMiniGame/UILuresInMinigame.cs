using System.Collections.Generic;
using UnityEngine;
using System.Collections;

using Lure.Data;
using Manager.Lure;
using UnityEngine.Audio;
using UnityEngine.Serialization;
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
       [SerializeField] private GameObject _luresButton;
       [SerializeField] private List<LureButtonEntry> _lureButtons = new();
       [SerializeField] private GameObject _usedLureText;
       
       [Header("SFX")]
       [SerializeField] private AudioClip _lureSfx;
       [SerializeField, Range(0f, 1f)] private float _sfxVolume = 1f;
       [SerializeField] private AudioMixerGroup _sfxGroup;
       
       private Coroutine _usedLureCoroutine;
       
       private void Start()
       {
           CheckLureButton(false);
           SetupButtons();
       }
       
       private void SetupButtons()
       {
           foreach (var entry in _lureButtons)
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
               used = lureManager.BoolUseLure(lure);
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
               
               if (_lureSfx != null)
               {
                   var pos = Camera.main != null ? Camera.main.transform.position : transform.position;
                   PlaySFX.PlayClipAtPosition(_lureSfx, pos, Mathf.Clamp01(_sfxVolume), true, true, _sfxGroup);
               }

               if (_usedLureText == null) 
                   return;
               
               if (_usedLureCoroutine != null)
                   StopCoroutine(_usedLureCoroutine);
               _usedLureCoroutine = StartCoroutine(ShowUsedLureTextRealtime(2f));
           }
       }

       private IEnumerator ShowUsedLureTextRealtime(float duration)
       {
           _usedLureText.SetActive(true);
           yield return new WaitForSecondsRealtime(duration);
           _usedLureText.SetActive(false);
           _usedLureCoroutine = null;
       }
   
       private void CheckLureButton(bool lureUsed)
       {
           if (!LureManager.Instance.HasAnyLure() || lureUsed)
           {
               _luresButton.SetActive(false);
               return;
           }
           _luresButton.SetActive(true);
       }
   
       private void CheckAllLures(bool lureUsed)
       {
           if (lureUsed)
           {
               foreach (var lureButtonItem in _lureButtons)
               {
                   lureButtonItem.button.SetActive(false);
               }

               return;
           }
           foreach (var lureButtonItem in _lureButtons)
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
