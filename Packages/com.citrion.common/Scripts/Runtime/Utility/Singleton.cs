using UnityEngine;

namespace CitrioN.Common
{
  [SkipObfuscationRename]
  public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
  {
    private static T instance;

    private static object l = new object();

    [SkipObfuscationRename]
    public static T Instance
    {
      get
      {
        if (ApplicationIsQuitting)
        {
          ConsoleLogger.LogWarning("Singleton instance '" + typeof(T) +
              "' already destroyed on application quit." +
              " Won't create again - returning null.", LogType.Debug);
          return null;
        }

        lock (l)
        {
          if (instance == null)
          {
#if UNITY_2023_1_OR_NEWER
            instance = (T)FindAnyObjectByType(typeof(T));
#else
            instance = (T)FindObjectOfType(typeof(T));
#endif

#if UNITY_2023_1_OR_NEWER
            if (FindObjectsByType(typeof(T), FindObjectsSortMode.None).Length > 1)
#else
            if (FindObjectsOfType(typeof(T)).Length > 1)
#endif
            {
              ConsoleLogger.LogError("Something went really wrong " +
                  " - there should never be more than 1 singleton!" +
                  " Reopenning the scene might fix it.", LogType.Always);
              return instance;
            }

            #region Recently reenabled
            if (instance == null)
            {
              GameObject singleton = new GameObject();
              instance = singleton.AddComponent<T>();
              singleton.name = "[Singleton (Instantiated)] " + typeof(T).Name.ToString();

              if (Application.isPlaying)
              {
                DontDestroyOnLoad(singleton);
              }

              ConsoleLogger.Log($"A singleton instance of {typeof(T)} is needed in the scene.".Colorize(Color.magenta) + "\n" +
                                $"'{singleton}' was created with DontDestroyOnLoad.".Colorize(Color.magenta), LogType.Always);
            }
            else
            {
              if (instance.transform.parent != null)
              {
                instance.transform.SetParent(null);
              }
              if (Application.isPlaying)
              {
                DontDestroyOnLoad(instance.gameObject);
              }

              //Debug.Log("[Singleton] Using instance already created: " +
              //    instance.gameObject.name);
            }
            #endregion
          }

          return instance;
        }
      }
    }

    protected static bool ApplicationIsQuitting => ApplicationQuitListener.isQuitting;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>

    [SkipObfuscationRename]
    protected virtual void OnDestroy()
    {
      // If the application is not quitting don't update any variables
      // This could be the case if a scene change happens and for some odd reason
      // the OnDestroy method is called even tho the gameObject is not really destroyed?!
      if (ApplicationIsQuitting)
      {
        //applicationIsQuitting = true;
        if (instance == this)
        {
          instance = null;
        }
        //instance = null;
      }
    }

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    //static void Init()
    //{
    //  applicationIsQuitting = false;
    //  instance = null;
    //}

    //[SkipObfuscation]
    //public void Reload()
    //{
    //  applicationIsQuitting = false;
    //}

    [SkipObfuscationRename]
    protected virtual void Awake()
    {
      name = "[Singleton] " + name;
      transform.SetParent(null);
      //foreach (var item in FindObjectsOfType<T>())
      //{
      //  Debug.Log(item.name);
      //  if (item != this)
      //  {
      //    Destroy(item.gameObject);
      //  }
      //}
      //Reload();


      //Debug.Log(typeof(T));


      //instance = null;
      //DestroyImmediate(gameObject);

      // Check if the instance is not set
      if (instance == null)
      {
        // Get and assign singleton instance from this gameobjects components
        instance = GetComponent<T>();
        // Check if the gameobject is a child of another gameobject.
        // If it is a child unparent it so the DontDestoryOnLoad method can function properly.
        if (instance?.gameObject.transform.parent != null) instance?.gameObject.transform.SetParent(null);
        DontDestroyOnLoad(/*instance?.*/gameObject);
        ConsoleLogger.Log("Created new singleton of ".Colorize(Color.magenta) + 
                          instance?.GetType().ToString().Colorize(Color.magenta),
                          LogType.Always);
        //Reload();
      }
      // Check if instance is this singleton instance
      else if (instance == this)
      {
        // Check if the gameobject is a child of another gameobject.
        // If it is a child unparent it so the DontDestoryOnLoad method can function properly.
        if (gameObject.transform.parent != null) gameObject.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        //Reload();
      }
      else
      {
        // Destroy this gameobject if the instance was already assigned and not this instance
        DestroyImmediate(gameObject);
      }


      //if (instance != null && instance != this)
      //{
      //  DestroyImmediate(instance.gameObject, true);
      //  Debug.Log("1 " + typeof(T));
      //}

      //instance = GetComponent<T>();
      //DontDestroyOnLoad(gameObject);
      //Debug.LogFormat("<color=magenta>Created new singleton of " + instance.GetType().ToString() + "</color>");


      // Working code
      //if (instance != null)
      //{
      //  DestroyImmediate(gameObject, false);
      //}
      //else
      //{
      //  instance = GetComponent<T>();
      //  DontDestroyOnLoad(gameObject);
      //  Debug.LogFormat("<color=magenta>Created new singleton of " + instance.GetType().ToString() + "</color>");
      //}
    }

  }
}