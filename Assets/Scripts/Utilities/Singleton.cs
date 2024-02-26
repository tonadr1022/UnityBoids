using UnityEngine;

namespace Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null) Debug.LogError("Singleton not found");
                return _instance;
            }
        }
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            } else if (_instance != this as T)
            {
                Debug.LogError("Singleton already exists, destroying this instance.");
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this as T)
            {
                Destroy(gameObject);
            }
        }
    }
}
