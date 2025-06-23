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
                    //�������� �ν��Ͻ��� ������Ʈ Ȯ�ο�
                    instance = FindObjectOfType<T>();
                    if(instance == null)
                    {
                        //�̸����� ������Ʈ ���� �� ������Ʈ ����
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
            //�̱��� �߰�
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
