using UnityEngine;
using System.Collections;

public class GenericThreePlanesCuttingController : MonoBehaviour
{
    public GameObject plane1; // 첫 번째 평면
    public GameObject plane2; // 두 번째 평면
    public GameObject plane3; // 세 번째 평면
    public Renderer rend; // 렌더러 컴포넌트
    public Vector3 normal1; // 첫 번째 평면의 법선
    public Vector3 position1; // 첫 번째 평면의 위치
    public Vector3 normal2; // 두 번째 평면의 법선
    public Vector3 position2; // 두 번째 평면의 위치
    public Vector3 normal3; // 세 번째 평면의 법선
    public Vector3 position3; // 세 번째 평면의 위치

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>(); // 스크립트가 연결된 게임 오브젝트의 렌더러 컴포넌트 가져오기
        UpdateShaderProperties(); // 쉐이더 속성 업데이트 함수 호출
    }

    void Update()
    {
        UpdateShaderProperties(); // 매 프레임마다 쉐이더 속성 업데이트 함수 호출
    }

    private void UpdateShaderProperties()
    {
        // 평면 1의 법선과 위치를 업데이트
        normal1 = plane1.transform.TransformVector(new Vector3(0, 0, -1));
        position1 = plane1.transform.position;

        // 평면 2의 법선과 위치를 업데이트
        normal2 = plane2.transform.TransformVector(new Vector3(0, 0, -1));
        position2 = plane2.transform.position;

        // 평면 3의 법선과 위치를 업데이트
        normal3 = plane3.transform.TransformVector(new Vector3(0, 0, -1));
        position3 = plane3.transform.position;

        // 모든 Material 배열을 순회하며 CrossSection/GenericThreePlanesBSP 쉐이더를 찾아 속성 업데이트
        for (int i = 0; i < rend.materials.Length; i++)
        {
            if (rend.materials[i].shader.name == "CrossSection/GenericThreePlanesBSP")
            {
                // 쉐이더 속성 업데이트
                rend.materials[i].SetVector("_Plane1Normal", normal1);
                rend.materials[i].SetVector("_Plane1Position", position1);
                rend.materials[i].SetVector("_Plane2Normal", normal2);
                rend.materials[i].SetVector("_Plane2Position", position2);
                rend.materials[i].SetVector("_Plane3Normal", normal3);
                rend.materials[i].SetVector("_Plane3Position", position3);
            }
        }
    }
}