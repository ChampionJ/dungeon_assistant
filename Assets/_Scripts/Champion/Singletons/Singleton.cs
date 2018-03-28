using System;

namespace Champion
{
    /// <summary>
    /// A singleton for any normal class.
    /// If object for singleton is not initally found, one will be created for you.
    /// </summary>
    public abstract class Singleton<T>
    {
        private static T _Instance;

        public static T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = Activator.CreateInstance<T>();
                }

                return _Instance;
            }
        }
    }
}