using System;
using UnityEngine;

namespace SardineFish.Utils
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        public static T Instance { get; private set;}
        public bool DontDestroyOnLoad = false;

        protected virtual void Awake()
        {
            Instance = this as T;
            if (DontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}