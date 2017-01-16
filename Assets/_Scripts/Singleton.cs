using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    private static string InstanceNotFoundOnLevel;

    public virtual void Awake()
    {
        if (Instance != this)
        {
            Destroy(this);
            Destroy(gameObject);
        }
      //  DontDestroyOnLoad(instance.gameObject);
    }

    public static bool IsInstanced()
    {
        if (instance == null)
        {
            TryToInstantinate();
        }

        return instance != null;
    }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                TryToInstantinate();

                if (instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
                }
            }

            return instance;
        }

        set
        {
            instance = null;
        }
    }

    private static void TryToInstantinate()
    {
        if (SceneManager.GetActiveScene().name != InstanceNotFoundOnLevel)
        {
            instance = (T)FindObjectOfType(typeof(T));

            if (instance == null)
            {
                InstanceNotFoundOnLevel = SceneManager.GetActiveScene().name;
            Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
        }
        }
    }
}