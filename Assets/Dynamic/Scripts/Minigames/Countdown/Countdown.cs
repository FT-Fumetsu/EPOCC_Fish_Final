using System;
using Manager.Pause;
using TMPro;
using UnityEngine;

namespace Minigames.Countdown
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countdownText;
        [SerializeField] private float _remainingTime;

        [SerializeField] private GameObject _endPanel;
        
        [Header("VFX")]
        [Tooltip("Prefab VFX à instancier quand le timer se termine")]
        [SerializeField] private GameObject _endVfxPrefab;

        private bool _isGameStarted;

        public bool IsGameStarted
        {
            get => _isGameStarted;
            set => _isGameStarted = value;
        }

        private void Update()
        {
            if(!_isGameStarted)
                return;
            
            switch (_remainingTime)
            {
                case > 0:
                    _remainingTime -= Time.deltaTime;
                    break;
                case <= 0:
                    _remainingTime = 0;
                    OnTimerEnd();
                    break;
            }
            
            var minutes = Mathf.FloorToInt(_remainingTime / 60);
            var seconds = Mathf.FloorToInt(_remainingTime);
            var milliSeconds = Mathf.FloorToInt((_remainingTime % 1) * 100);

            if (_remainingTime <= 5)
            {
                _countdownText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
                return;
            }
            _countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }
        
        private void OnTimerEnd()
        {
            if (!_endPanel) 
                return;

            if (_endPanel.activeSelf)
                return;

            if (_endVfxPrefab != null)
            {
                var vfx = Instantiate(_endVfxPrefab, transform.position, Quaternion.identity);
                
                var play = vfx.GetComponent<global::VFX.PlayVFX>();
                play?.Play();
            }
            
            Save.SaveSystem.Instance?.Save();
            
            _endPanel.SetActive(true);
            
            PauseManager.Instance?.TogglePause(true);
        }
    }
}