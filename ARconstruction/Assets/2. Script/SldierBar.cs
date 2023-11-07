using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SldierBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    #region GameObject
    [SerializeField] GameObject m_withoutWall;
    [SerializeField] GameObject m_FrameObj;
    [SerializeField] GameObject m_WallObj;
    [SerializeField] GameObject m_MechanicalObj;
    [SerializeField] GameObject m_PlumbingObj;
    [SerializeField] GameObject m_FireProtectionObj;
    #endregion
    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        if (value == 0)
        {
            Debug.Log(" ตส");
            m_withoutWall.SetActive(false);
            m_FrameObj.SetActive(false);
            m_WallObj.SetActive(false);
            m_MechanicalObj.SetActive(false);
            m_PlumbingObj.SetActive(false);
            m_FireProtectionObj.SetActive(false);
        }
        else if (value == 1)
        {
            Debug.Log(" ตส2");
            m_withoutWall.SetActive(false);
            m_FrameObj.SetActive(false);
            m_WallObj.SetActive(false);
            m_MechanicalObj.SetActive(true);
            m_PlumbingObj.SetActive(true);
            m_FireProtectionObj.SetActive(true);
        }
        else if (value == 2)
        {
            Debug.Log(" ตส3");
            m_withoutWall.SetActive(false);
            m_FrameObj.SetActive(true);
            m_WallObj.SetActive(false);
            m_MechanicalObj.SetActive(true);
            m_PlumbingObj.SetActive(true);
            m_FireProtectionObj.SetActive(true);
        }
        else if (value == 3)
        {
            Debug.Log(" ตส4");
            m_withoutWall.SetActive(false);
            m_FrameObj.SetActive(false);
            m_WallObj.SetActive(true);
            m_MechanicalObj.SetActive(true);
            m_PlumbingObj.SetActive(true);
            m_FireProtectionObj.SetActive(true);
        }
        else
        {
            m_withoutWall.SetActive(true);
            m_FrameObj.SetActive(false);
            m_WallObj.SetActive(true);
            m_MechanicalObj.SetActive(true);
            m_PlumbingObj.SetActive(true);
            m_FireProtectionObj.SetActive(true);
        }

    }


}
