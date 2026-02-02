using UnityEngine;

namespace Singletons
{
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject gameObject = new GameObject();
                        gameObject.name = typeof(T).Name;
                        _instance = gameObject.AddComponent<T>();
                        _instance.OnMonoSingletonCreated();
                    }
                }
                return _instance;
            }
        }

        public static void CreateInstance()
        {
            DestroyInstance();
            _instance = Instance;
        }

        public static void DestroyInstance()
        {
            if (_instance == null)
                return;
            
            _instance.Uninitialize();
            _instance = default(T);
        }
        
        private InitializationStatus _initializationStatus = InitializationStatus.None;
        public virtual bool IsInitialized => _initializationStatus == InitializationStatus.Initialized;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                Initialize();
            }
            else if (_instance != this)
            {
                if (Application.isPlaying)
                    Destroy(gameObject);
                else
                    DestroyImmediate(gameObject);
            }
        }

        public virtual void Initialize()
        {
            if (_initializationStatus != InitializationStatus.None)
                return;

            _initializationStatus = InitializationStatus.Initializing;
            OnInitializing();
            _initializationStatus  = InitializationStatus.Initialized;
            OnInitialized();
        }

        public virtual void Uninitialize() { }

        protected virtual void OnMonoSingletonCreated() { }

        protected virtual void OnInitializing() { }

        protected virtual void OnInitialized() { }
    }
}