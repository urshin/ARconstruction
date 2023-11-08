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

    public string objID;

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

    public IEnumerator LoadObjDataFromJson()
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


            for (int i = 0; i < objList.ObjectInformation.Length; i++) 
            {
                if(objList.ObjectInformation[i].productName == objID)
                {
                    productName.text = objList.ObjectInformation[i].productName;
                    productCategory.text = objList.ObjectInformation[i].productCategory;
                    productMaterial.text = objList.ObjectInformation[i].productMaterial;
                    Floor.text = objList.ObjectInformation[i].Floor;
                    objectSize.text = objList.ObjectInformation[i].objectSize;
                    workFlow.text = objList.ObjectInformation[i].workFlow;
                    startDate.text = objList.ObjectInformation[i].startDate;
                    endDate.text = objList.ObjectInformation[i].endDate;
                    currentStatus.text = objList.ObjectInformation[i].currentStatus;
                }
        
            }
       

        }

    }
}
