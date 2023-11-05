using UnityEngine;
using System.Collections;

public class ThreeAAPlanesCuttingController : MonoBehaviour
{

    public GameObject planeYZ;
    public GameObject planeXZ;
    public GameObject planeXY;
    public Material mat;
    public Vector3 positionYZ;
    public Vector3 positionXZ;
    public Vector3 positionXY;
    public Renderer rend;
    public GameObject RenderingObject;
    // Use this for initialization
    void Start()
    {
        rend = RenderingObject.GetComponent<Renderer>();
        Debug.Log(rend.material.name);
        mat = rend.material;
        Debug.Log(mat.name);
        UpdateShaderProperties();
    }
    void Update()
    {
        UpdateShaderProperties();
    }

    private void UpdateShaderProperties()
    {
        positionYZ = planeYZ.transform.position;
        positionXZ = planeXZ.transform.position;
        positionXY = planeXY.transform.position;

        if (mat.shader.name == "CrossSection/ThreeAAPlanesBSP")
        {
           mat.SetVector("_Plane1Position", positionYZ);
           mat.SetVector("_Plane2Position", positionXZ);
            mat.SetVector("_Plane3Position", positionXY);
        }

        //for (int i = 0; i < rend.materials.Length; i++)
        //{
        //    if (rend.materials[i].shader.name == "CrossSection/ThreeAAPlanesBSP")
        //    {
        //        rend.materials[i].SetVector("Plane1Position", positionYZ);
        //        rend.materials[i].SetVector("Plane2Position", positionXZ);
        //        rend.materials[i].SetVector("Plane3Position", positionXY);
        //    }
        //}

    }
}