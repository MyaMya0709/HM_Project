using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static readonly object lockObj = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    //수동으로 인스턴스된 오브젝트 확인용
                    instance = FindObjectOfType<T>();
                    if(instance == null)
                    {
                        //이름으로 오브젝트 생성 및 컴포넌트 지정
                        GameObject singletonObj = new GameObject(typeof(T).Name);
                        instance = singletonObj.AddComponent<T>();
                        DontDestroyOnLoad(singletonObj);
                    }
                }
            }
            return instance;
        }
    }

    protected void Awake()
    {
        if (instance == null)
        {
            //싱글톤 추가
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
