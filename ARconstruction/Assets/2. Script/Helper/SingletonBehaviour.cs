using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;

    public static T Instance
    {
        get
        {
            //SingletonBehaviour�� �ʱ�ȭ �Ǳ� ���̶��
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    protected void Awake()
    {
        if(_instance != null)
        {
            //�ٸ� ���� ������Ʈ�� �ִٸ�
            if(_instance != this)
            {
                //�ϳ��� ���� ������Ʈ�� ������ ����
                Destroy(gameObject);
            }
            //Awake() ȣ�� �� �Ҵ�� �ν��Ͻ��� �ڱ� �ڽ��̶�� �ƹ��͵� ���� �ʴ´�.
            return;
        }

        _instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}
