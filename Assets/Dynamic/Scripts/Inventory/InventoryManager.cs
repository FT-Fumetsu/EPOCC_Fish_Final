using System;
using Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inventory
{
    public class InventoryManager : PersistentMonoSingleton<InventoryManager>
    {
        [SerializeField] private bool _isTutorialFinished;
        
        public bool IsTutorialFinished
        {
            get => _isTutorialFinished;
            set => _isTutorialFinished = value;
        }
    }
}