using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataManager : MonoBehaviour
{

    public ObjData objData;

    public string fileName = "";

    [ContextMenu("To Json Data")]//컴포넌트에 뒤따라오는 함수를 실행하는 메뉴버튼을 추가
    void SaveObjDataToJson()
    {
        string jsonData = JsonUtility.ToJson(objData, true);
        string path = Application.streamingAssetsPath + "/" + fileName + ".json"; //경로+파일이름
        File.WriteAllText(path, jsonData); //경로에 제이슨 데이터 저장
        print(path);
    }

    [ContextMenu("From Json Data")]
    void LoadObjDataFromJson()
    {
        string path = Application.dataPath + "/" + fileName + ".json";
        string jsonData = File.ReadAllText(path);
        objData = JsonUtility.FromJson<ObjData>(jsonData);
    }
}
