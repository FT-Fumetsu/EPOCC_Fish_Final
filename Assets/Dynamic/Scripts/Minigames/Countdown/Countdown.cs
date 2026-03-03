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

        [Header("SFX")]
        [Tooltip("SFX joué quand le timer se termine (sera lancé avant la mise en pause)")]
        [SerializeField] private AudioClip _endSfx;
        [Range(0f,1f), SerializeField] private float _endSfxVolume = 1f;

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
                
                var play = vfx.GetComponent<VFX.PlayVFX>();
                play?.Play();
            }
            
            if (_endSfx != null)
            {
                var pos = Camera.main != null ? Camera.main.transform.position : transform.position;
                PlaySFX.PlayClipAtPosition(_endSfx, pos, Mathf.Clamp01(_endSfxVolume), true, true);
            }
            
            Save.SaveSystem.Instance?.Save();
            
            _endPanel.SetActive(true);
            
            PauseManager.Instance?.TogglePause(true);
        }
    }
}