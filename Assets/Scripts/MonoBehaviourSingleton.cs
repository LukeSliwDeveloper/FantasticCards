using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<MonoBehaviourSingleton<T>>();
            }
            _instance.Initialize();
            return _instance as T;
        }
    }

    private static MonoBehaviourSingleton<T> _instance;

    protected static bool _wasInitialized;

    private void Awake()
    {
        if (_instance != this)
        {
            if (_instance == null)
                _instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        Initialize();
    }

    protected virtual void Initialize()
    {
        if (!_wasInitialized)
        {
            _wasInitialized = true;
        }
    }
}
