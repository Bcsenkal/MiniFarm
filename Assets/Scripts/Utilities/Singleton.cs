using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{

    static T m_instance;

    public static T Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<T>();

                if(m_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    m_instance = singleton.AddComponent<T>();
                }
            }
            return m_instance;
        }

        
    }

    protected virtual void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (m_instance == this)
        {
            m_instance = null;
        }
    }

    public static bool IsInitialized => m_instance != null;
}
