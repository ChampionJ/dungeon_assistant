using UnityEngine;
using System.Collections;

namespace Champion
{
    /// <summary>
    /// A singleton for MonoBehaviour based classes. Singleton will initialize on the first Instance get request or on Awake, whichever happens first.
    /// This singleton is intended for a MonoBehaviour that is designed to act like a static object (like a GameManager).
    /// </summary>
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        private static T _Instance;
        private static bool _Initialized;

        public static T Instance
        {
            get
            {
                if (!_Initialized && _Instance == null)
                {
                    _Instance = FindObjectOfType<T>();

                    if (_Instance != null)
                        _Instance.SingletonAwake();

                    _Initialized = true;
                }

                return _Instance;
            }
        }

        private void Awake ()
        {
            if (_Instance == null || !_Initialized)
            {
                _Instance = (T)this;
                _Instance.SingletonAwake();

                _Initialized = true;
            }
            else
            {
                Debug.LogException(new System.Exception("Duplicate singleton found for " + typeof(T).Name + "."), gameObject);
            }
        }

        /// <summary>
        /// Replaces the MonoBehaviour Awake call. Since MonoBehaviourSingleton are not newly constructed objects, use this initialization method
        /// to setup the singleton object as needed when the singleton is initialized.
        /// </summary>
        protected abstract void SingletonAwake ();
    }
}