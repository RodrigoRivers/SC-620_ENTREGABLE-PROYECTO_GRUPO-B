using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    static Singleton<T> _instance;
    static readonly object _syncLock = new object();

    protected virtual void Awake()
    {
        bool destroyCurrentInstance = true;

        if (_instance == null)
        {
            lock (_syncLock)
            { 
                if (_instance == null) 
                {
                    _instance = this;
                    DontDestroyOnLoad(gameObject);
                    destroyCurrentInstance = false;
                }
            }
        }

        if (destroyCurrentInstance)
        {
            Destroy(gameObject);
            return;
        }
    }

    static Singleton<T> GetInstance()
    {
        return _instance;
    }

    public static T Instance
    {
        get 
        { 
            return GetInstance() as T;
        }
    }
}
