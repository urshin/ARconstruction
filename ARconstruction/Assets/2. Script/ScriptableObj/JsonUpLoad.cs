using LitJson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JsonUpLoad : MonoBehaviour
{
    const string jsonFilePath = "Data/RoofTop";

    private void Start()
    {
        ReadJsonFile();
    }

    JsonData ReadJsonFile()
    {
        var jsonFile = Resources.Load<TextAsset>(jsonFilePath);
        JsonData jsonData = JsonMapper.ToObject(jsonFile.ToString());
        return jsonData;
    }
}
