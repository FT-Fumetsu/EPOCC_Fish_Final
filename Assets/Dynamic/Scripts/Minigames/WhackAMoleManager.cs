using System;
using Fish.Spawner;
using Inventory;
using Minigames.Countdown;
using UnityEngine;

namespace Minigame.Manager
{
    [RequireComponent(typeof(DialogueLauncher))]
    public class WhackAMoleManager : MonoBehaviour
    {
        [SerializeField] private Countdown _countdown;
        [SerializeField] private FishSpawner[] _fishSpawnerArray;

        private void Start()
        {
            LaunchDialogue();
        }

        private void LaunchDialogue()
        {
            var dialogueLauncher = GetComponent<DialogueLauncher>();
            dialogueLauncher.LaunchDialogue(StartGame);
        }

        private void StartGame()
        {
            _countdown.IsGameStarted = true;
            foreach (var fishSpawnerItem in _fishSpawnerArray)
            {
                fishSpawnerItem.StartSpawning();
            }
        }
    }
}