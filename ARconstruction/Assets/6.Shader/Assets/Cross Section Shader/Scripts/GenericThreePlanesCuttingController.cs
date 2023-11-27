using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenericThreePlanesCuttingController : MonoBehaviour
{
    public GameObject plane1; // 첫 번째 평면
    public GameObject plane2; // 두 번째 평면
    public GameObject plane3; // 세 번째 평면
    public GameObject[] RenderingObject;
    [SerializeField] Renderer[] rend; // 렌더러 컴포넌트 배열
    [SerializeField] Material[] material;

    // Use this for initialization
    void Start()
    {
        rend = new Renderer[RenderingObject.Length]; // 배열 크기 초기화

        for (int i = 0; i < RenderingObject.Length; i++)
        {
            rend[i] = RenderingObject[i].GetComponent<Renderer>();
        }

        // Create a list to hold the materials
        List<Material> materialList = new List<Material>();

        // Copy materials from rend to material list
        for (int i = 0; i < rend.Length; i++)
        {
            Material[] materials = rend[i].materials; // Get all materials for the renderer

            // Add each material to the list
            materialList.AddRange(materials);
        }

        // Convert the list to an array
        material = materialList.ToArray();

        UpdateShaderProperties(); // 쉐이더 속성 업데이트 함수 호출
    }

    void Update()
    {
        UpdateShaderProperties(); // 매 프레임마다 쉐이더 속성 업데이트 함수 호출
    }

    private void UpdateShaderProperties()
    {
        // 평면 1의 법선과 위치를 업데이트
        Vector3 normal1 = plane1.transform.TransformVector(new Vector3(0, 0, -1));
        Vector3 position1 = plane1.transform.position;

        // 평면 2의 법선과 위치를 업데이트
        Vector3 normal2 = plane2.transform.TransformVector(new Vector3(0, 0, -1));
        Vector3 position2 = plane2.transform.position;

        // 평면 3의 법선과 위치를 업데이트
        Vector3 normal3 = plane3.transform.TransformVector(new Vector3(0, 0, -1));
        Vector3 position3 = plane3.transform.position;

        // 모든 Material 배열을 순회하며 CrossSection/GenericThreePlanesBSP 쉐이더를 찾아 속성 업데이트
        for (int i = 0; i < material.Length; i++)
        {
            if (material[i].shader.name == "CrossSection/GenericThreePlanesBSP")
            {
                // 쉐이더 속성 업데이트
                material[i].SetVector("_Plane1Normal", normal1);
                material[i].SetVector("_Plane1Position", position1);
                material[i].SetVector("_Plane2Normal", normal2);
                material[i].SetVector("_Plane2Position", position2);
                material[i].SetVector("_Plane3Normal", normal3);
                material[i].SetVector("_Plane3Position", position3);
            }
        }
    }
}