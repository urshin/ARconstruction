using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class ServerTest : MonoBehaviour
{
    private void Awake()
    {
        BackendSetUP();
    }
    void BackendSetUP()
    {
        var bro = Backend.Initialize(true);

        if(bro.IsSuccess())
        {
            print("초기화 성공");
        }
        else
        {
            print("초기화 실패");
        }
    }

}
