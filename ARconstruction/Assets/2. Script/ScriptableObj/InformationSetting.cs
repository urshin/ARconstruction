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

    public string fileName;


    [SerializeField] TextMeshProUGUI productName;
    [SerializeField] TextMeshProUGUI productCategory;
    [SerializeField] TextMeshProUGUI productMaterial;
    [SerializeField] TextMeshProUGUI Floor;
    [SerializeField] TextMeshProUGUI objectSize;
    [SerializeField] TextMeshProUGUI workFlow;
    [SerializeField] TextMeshProUGUI startDate;
    [SerializeField] TextMeshProUGUI endDate;
    [SerializeField] TextMeshProUGUI currentStatus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(Instance);
        }
    }

    /*  public void LoadObjDataFromJson(string fileName)
      {
          string path =  "jar:file://" + Application.streamingAssetsPath + "/" + fileName + ".json";
          textMeshProUGUI.text = path;
          string jsonData = File.ReadAllText(path);
          objData = JsonUtility.FromJson<ObjData>(jsonData);
      }*/
    public class ObjectInfo
    {
        public ObjData[] ObjectInformation;
    }

    public IEnumerator LoadObjDataFromJson(string fileName)
    {


        string path = Path.Combine(Application.streamingAssetsPath, "ObjInfo.json");

#if UNITY_ANDROID && !UNITY_EDITOR
        path = "jar:file://" + Application.streamingAssetsPath + "/" + fileName + ".json";
#endif

        using (UnityWebRequest www = UnityWebRequest.Get(path))
        {
            // 웹 요청을 보냅니다.
            yield return www.SendWebRequest();
            // JSON 데이터를 문자열로 읽어옵니다.
            string jsonData = www.downloadHandler.text;
            ObjectInfo objList = JsonUtility.FromJson<ObjectInfo>(jsonData);


            productName.text = objList.ObjectInformation[0].productName;
            productCategory.text = objList.ObjectInformation[0].productCategory;
            productMaterial.text = objList.ObjectInformation[0].productMaterial;
            Floor.text = objList.ObjectInformation[0].Floor;
            objectSize.text = objList.ObjectInformation[0].objectSize;
            workFlow.text = objList.ObjectInformation[0].workFlow;
            startDate.text = objList.ObjectInformation[0].startDate;
            endDate.text = objList.ObjectInformation[0].endDate;
            currentStatus.text = objList.ObjectInformation[0].currentStatus;

        }

    }
}
