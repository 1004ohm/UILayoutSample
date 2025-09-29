using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonoInitializer
{
    public static List<Action> _initializationActions = new List<Action>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void StaticInit()
    {
        if(_initializationActions != null && _initializationActions.Count>0) //리스트에 등록이 되어있다면
        {
            for(int i =0; i< _initializationActions.Count; i++) 
            {
                _initializationActions[i].Invoke(); //초기화 실행
            }
        }
        _initializationActions = new List<Action>();
    }

}
public interface ISingleton
{ }

public class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
{
    private static T _instance;

    private static readonly object Lock = new object();
    private static bool _applicationIsQuitting;

    protected virtual bool ShouldRename => false;
    private static bool isMonoSingletonInit = false;

    protected static void StaticFieldInit()
    {
        isMonoSingletonInit = false;
        _instance = null;
        _applicationIsQuitting = false;
    }

    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                return _instance;
            }

            lock (Lock)
            {
                if (_instance)
                    return _instance;

                _instance = (T)FindObjectOfType(typeof(T));

                if (!_instance)
                {
                    _instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    if (!_instance)
                    {
                        Debug.Log("[MonoSingleton]Something went really wrong - there should never be more than 1 singleton! Reopening the scene might fix it.");
                    }

                    Debug.Log($"[MonoSingleton]An instance of {typeof(T)} is needed in the scene, so '{_instance.name}' was created with DontDestroyOnLoad.");
                }

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    Debug.Log(
                        $"[MonoSingleton]Something went really wrong - there should never be more than 1 singleton! Reopening the scene might fix it.");
                }

                return _instance;
            }
        }
    }

    #region Mono
    protected virtual void Awake()
    {
        if (_instance &&
            _instance != this)
        {
            Debug.Log($"{typeof(T)} already exist!");
            Destroy(gameObject);
            return;
        }

        if (!_instance)
        {
            _instance = (T)this;
        }

        if (ShouldRename)
        {
            name = typeof(T).ToString();
        }
        isMonoSingletonInit = true;
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }

    protected virtual void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
        MonoInitializer._initializationActions.Add(StaticFieldInit);
    }

    #endregion
}
