using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEditor;
using LitJson;
using UnityEngine.Networking;
using Unity.VisualScripting;

public class InformationSetting : MonoBehaviour
{
    public static InformationSetting Instance;
    
    ObjectInfo objList;

    [SerializeField] TextMeshProUGUI productName;
    [SerializeField] TextMeshProUGUI productCategory;
    [SerializeField] TextMeshProUGUI productMaterial;
    [SerializeField] TextMeshProUGUI Floor;
    [SerializeField] TextMeshProUGUI objectSize;
    [SerializeField] TextMeshProUGUI workFlow;
    [SerializeField] TextMeshProUGUI startDate;
    [SerializeField] TextMeshProUGUI endDate;
    [SerializeField] TextMeshProUGUI currentStatus;


    Dictionary<string, int> productNameToIndex = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartCoroutine(ReadJson());
    }

    public class ObjectInfo
    {
        public ObjData[] ObjectInformation;
    }


    private IEnumerator ReadJson()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "ObjInfo.json");


#if UNITY_ANDROID && !UNITY_EDITOR
        path = "jar:file://" + Application.streamingAssetsPath + "/" + "ObjInfo.json";
#endif

        using (UnityWebRequest www = UnityWebRequest.Get(path))
        {
            // 웹 요청을 보냅니다.
            yield return www.SendWebRequest();
            // JSON 데이터를 문자열로 읽어옵니다.
            string jsonData = www.downloadHandler.text;
            objList = JsonUtility.FromJson<ObjectInfo>(jsonData);

            AddDictionary();
        }
    }

    private void AddDictionary()
    {
        for (int i = 0; i < objList.ObjectInformation.Length; i++)
        {
            productNameToIndex.Add(objList.ObjectInformation[i].productName, i);
        }
    }

    public void UpdateUI(string objID)
    {
        productName.text = objList.ObjectInformation[productNameToIndex[objID]].productName;
        productCategory.text = objList.ObjectInformation[productNameToIndex[objID]].productCategory;
        productMaterial.text = objList.ObjectInformation[productNameToIndex[objID]].productMaterial;
        Floor.text = objList.ObjectInformation[productNameToIndex[objID]].Floor;
        objectSize.text = objList.ObjectInformation[productNameToIndex[objID]].objectSize;
        workFlow.text = objList.ObjectInformation[productNameToIndex[objID]].workFlow;
        startDate.text = objList.ObjectInformation[productNameToIndex[objID]].startDate;
        endDate.text = objList.ObjectInformation[productNameToIndex[objID]].endDate;
        currentStatus.text = objList.ObjectInformation[productNameToIndex[objID]].currentStatus;
    }
}
