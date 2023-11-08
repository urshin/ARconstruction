using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PositionController : MonoBehaviour {
    public Slider XPos;
    public Slider YPos;
    public Slider ZPos;
    public float x;
    public float y;
    public float z;
    public Transform ControlledObject;
    public void UpdateObjectPosition(Transform ControlledObject)
    {
        Vector3 newPosition = new Vector3(XPos?XPos.value:x, YPos?YPos.value:y, ZPos?ZPos.value:z);
        ControlledObject.localPosition = newPosition;
    }
  
}
