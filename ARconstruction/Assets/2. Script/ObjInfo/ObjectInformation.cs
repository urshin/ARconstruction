using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/ObjectData")]
public class ObjectInformation : ScriptableObject
{
    [SerializeField] public string productName;
    [SerializeField] public string productCategory;
    [SerializeField] public string productMaterial;
    [SerializeField] public string Floor;
    [SerializeField] public string objectSize;
    [SerializeField] public string workFlow;
    [SerializeField] public string startDate;
    [SerializeField] public string endDate;
    [SerializeField] public string currentStatus;
}
