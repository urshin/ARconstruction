using UnityEngine;
using System.Collections;

public class ThreeAAPlanesCuttingController : MonoBehaviour
{
    public GameObject planeYZ;
    public GameObject planeXZ;
    public GameObject planeXY;
    public Vector3 positionYZ;
    public Vector3 positionXZ;
    public Vector3 positionXY;
    [SerializeField] Renderer[] rend; // 렌더러 배열
    public GameObject[] RenderingObject; // GameObject 배열
    // Use this for initialization
    void Start()
    {
        // 렌더러 배열 초기화
        rend = new Renderer[RenderingObject.Length];
        for (int i = 0; i < RenderingObject.Length; i++)
        {
            rend[i] = RenderingObject[i].GetComponent<Renderer>();
            Debug.Log(rend[i].material.name);
            Material mat = rend[i].material;
            Debug.Log(mat.name);
        }
        UpdateShaderProperties();
    }

    void Update()
    {
        UpdateShaderProperties();
    }

    private void UpdateShaderProperties()
    {
        positionYZ = planeYZ.transform.localPosition;
        positionXZ = planeXZ.transform.localPosition;
        positionXY = planeXY.transform.localPosition;

        for (int i = 0; i < rend.Length; i++)
        {
            if (rend[i].material.shader.name == "CrossSection/ThreeAAPlanesBSP")
            {
                rend[i].material.SetVector("_Plane1Position", positionYZ/3);
                rend[i].material.SetVector("_Plane2Position", positionXZ/3);
                rend[i].material.SetVector("_Plane3Position", positionXY/3);
            }
        }
    }
}