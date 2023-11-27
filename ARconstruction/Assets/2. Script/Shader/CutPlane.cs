using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutPlane : MonoBehaviour
{
    // 잘린 면의 법선 벡터를 저장할 변수
    private Vector3 normal;

    // 여러 잘린 객체의 머티리얼을 조작하기 위한 게임 오브젝트 리스트 변수
    public List<GameObject> clipObjs;
    private List<Material> clipObjMats;

    void Start()
    {
        // 잘린 면의 법선 벡터를 초기화
        this.normal = this.GetComponent<MeshFilter>().mesh.normals[0];

        // 각 잘린 객체의 머티리얼을 가져와 리스트에 저장
        clipObjMats = new List<Material>();
        foreach (var obj in clipObjs)
        {
            Material mat = obj.GetComponent<MeshRenderer>().material;
            clipObjMats.Add(mat);
        }
    }

    void Update()
    {
        // 프레임마다 실행되는 업데이트 함수

        // 잘린 객체의 머티리얼에 PlaneCenter와 PlaneNormal을 설정
        // 이것은 셰이더에서 사용될 것이다.

        // PlaneCenter: 자르는 평면의 중심 위치
        Vector3 planeCenter = this.transform.position;

        // PlaneNormal: 자르는 평면의 법선 벡터 (로컬 좌표를 월드 좌표로 변환)
        Vector3 planeNormal = this.transform.TransformDirection(this.normal);

        // 모든 잘린 객체에 설정 적용
        foreach (var mat in clipObjMats)
        {
            mat.SetVector("_PlaneCenter", planeCenter);
            mat.SetVector("_PlaneNormal", planeNormal);
        }
    }
}