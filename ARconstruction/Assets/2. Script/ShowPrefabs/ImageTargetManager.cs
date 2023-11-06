using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTargetManager : MonoBehaviour
{
    [SerializeField] GameObject m_House;
    [SerializeField] GameObject m_HouseFrame;
    [SerializeField] GameObject m_MechanicalObj;
    [SerializeField] GameObject m_PlumbingObj;
    [SerializeField] GameObject m_FireProtectionObj;

    [SerializeField] Toggle m_Toggle_ViewAll;
    [SerializeField] Toggle m_Toggle_Frame;
    [SerializeField] Toggle m_Toggle_Wall;
    [SerializeField] Toggle m_Toggle_MEPF;
    [SerializeField] Toggle m_Toggle_Mechanical;
    [SerializeField] Toggle m_Toggle_Plumbing;
    [SerializeField] Toggle m_Toggle_FireProtection;

    [SerializeField] Button m_resetBtn;
    [SerializeField] Button m_deleteBtn;


    private void Start()
    {
        m_resetBtn.onClick.AddListener(delegate
        {
            FoundnReset();
        });

        m_deleteBtn.onClick.AddListener(delegate
        {
            LostnDelete();
        });
    }
    public void FoundnReset()
    {
        //m_House.SetActive(true);
        //m_HouseFrame.SetActive(false);
        //m_MechanicalObj.SetActive(false);
        //m_PlumbingObj.SetActive(false);
        //m_FireProtectionObj.SetActive(false);
    }

    public void LostnDelete()
    {
        m_House.SetActive(false);
        m_HouseFrame.SetActive(false);
        m_MechanicalObj.SetActive(false);
        m_PlumbingObj.SetActive(false);
        m_FireProtectionObj.SetActive(false);
    }
}
