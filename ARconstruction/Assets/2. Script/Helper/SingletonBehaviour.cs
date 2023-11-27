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
            //SingletonBehaviour가 초기화 되기 전이라면
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
            //다른 게임 오브젝트가 있다면
            if(_instance != this)
            {
                //하나의 게임 오브젝트만 남도록 삭제
                Destroy(gameObject);
            }
            //Awake() 호출 전 할당된 인스턴스가 자기 자신이라면 아무것도 하지 않는다.
            return;
        }

        _instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}
