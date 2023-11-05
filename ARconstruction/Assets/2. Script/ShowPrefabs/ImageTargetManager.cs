using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ImageTargetManager : MonoBehaviour/*, ITrackableEventHandler*/
{
    [SerializeField] GameObject m_House;
    [SerializeField] GameObject m_HouseFrame;
    [SerializeField] GameObject m_MechanicalObj;
    [SerializeField] GameObject m_PlumbingObj;
    [SerializeField] GameObject m_FireProtectionObj;

    [SerializeField] Button m_resetBtn;
    [SerializeField] Button m_deleteBtn;

    //private TrackableBehaviour m_TrackableBehaviour;

    private void Start()
    {
        //m_TrackableBehaviour = GetComponent<TrackableBehaviour>();
        //if (m_TrackableBehaviour)
        //{
        //    m_TrackableBehaviour.RegisterTrackableEventHandler(this);
        //}

        //m_resetBtn.onClick.AddListener(ResetObjs);
        //m_deleteBtn.onClick.AddListener(Delete);

        //eventHandler = GetComponent<DefaultObserverEventHandler>();

        //eventHandler.OnTargetFound.AddListener(delegate
        //{
        //    Found();
        //});

        //eventHandler.OnTargetLost.AddListener(delegate
        //{
        //    Lost();
        //});

        m_resetBtn.onClick.AddListener(delegate
        {
            ResetObjs();
        });

        m_deleteBtn.onClick.AddListener(delegate
        {
            Delete();
        });
    }
    //public void OnTrackableStateChanged(
    // TrackableBehaviour.Status previousStatus,
    // TrackableBehaviour.Status newStatus)
    //{
    //    if (newStatus == TrackableBehaviour.Status.TRACKED)
    //    {
    //        Found();
    //    }
    //    else
    //    {
    //        Lost();
    //    }
    //}
    public void Found()
    {
        m_House.SetActive(true);
        m_HouseFrame.SetActive(false);
        m_MechanicalObj.SetActive(false);
        m_PlumbingObj.SetActive(false);
        m_FireProtectionObj.SetActive(false);
    }

    public void Lost()
    {
        m_House.SetActive(false);
        m_HouseFrame.SetActive(false);
        m_MechanicalObj.SetActive(false);
        m_PlumbingObj.SetActive(false);
        m_FireProtectionObj.SetActive(false);
    }

    public void ResetObjs()
    {
        Found();
    }

    public void Delete()
    {
        Lost();
    }
}
