using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class PositionController : MonoBehaviour
{
    public Slider XPos;
    public Slider YPos;
    public Slider ZPos;
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    [SerializeField] float multi=1f;
    public Transform[] ControlledObject;
    public void UpdateObjectPosition(Transform ControlledObject)
    {
        Vector3 newPosition = new Vector3(XPos ? XPos.value * multi : x, YPos ? YPos.value * multi : y, ZPos ? ZPos.value * multi : z );
        foreach (Transform t in ControlledObject)
        {
            t.localPosition = newPosition;
        }
    }

}
