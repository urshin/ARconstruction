using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TouchMgr : MonoBehaviour
{
    private Camera ARCam;
    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        ARCam = GameObject.Find("ARCamera").GetComponent<Camera>();
    }

    void Update()
    {
        //터치의 갯수가 0 이상이고 첫 번째 터치일 경우
        //if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||Input.GetMouseButton(0))
        if ( Input.GetMouseButtonDown(0))
        {
            //손가락으로 터치한 스크린 좌표를 기준으로 3차원 광선을 생성
            //ray = ARCam.ScreenPointToRay(Input.GetTouch(0).position);
            ray = ARCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits;

            hits = Physics.RaycastAll(ray, 100f);

            if(hits!=null && hits.Length>0)
            {
                RaycastHit closesHit = hits[0];

                for(int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].distance < closesHit.distance)
                    {
                        closesHit = hits[i];
                    }
                }

                string objectName = closesHit.transform.name;
                InformationSetting.Instance.UpdateUI(objectName);

            }   
        }
    }
}
