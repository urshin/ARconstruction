using UnityEngine;
using UnityEngine.UI;

public class ShowObjs : MonoBehaviour
{
    #region Toggle
    [SerializeField] Toggle m_Toggle_ViewAll;
    [SerializeField] Toggle m_Toggle_Frame;
    [SerializeField] Toggle m_Toggle_Wall;
    [SerializeField] Toggle m_Toggle_MEPF;
    [SerializeField] Toggle m_Toggle_Mechanical;
    [SerializeField] Toggle m_Toggle_Plumbing;
    [SerializeField] Toggle m_Toggle_FireProtection;
    #endregion

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
        ToggleInit();

        //각 토글에 대한 이벤트 리스너를 추가
        Toggle[] toggles = new Toggle[]
        { m_Toggle_ViewAll, m_Toggle_Frame, m_Toggle_Wall, m_Toggle_MEPF, m_Toggle_Mechanical, m_Toggle_Plumbing, m_Toggle_FireProtection };

        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate
            {
                ToggleValueChanged(toggle);
            });
        }
    }

    void ToggleInit()
    {
        //토글 초기 설정
        m_Toggle_ViewAll.isOn = true;
        m_Toggle_Frame.isOn = false;
        m_Toggle_Wall.isOn = false;
        m_Toggle_Mechanical.isOn = false;
        m_Toggle_Plumbing.isOn = false;
        m_Toggle_FireProtection.isOn = false;
    }

    void ToggleValueChanged(Toggle toggle)
    {
        if (toggle == m_Toggle_ViewAll)
        {
            bool viewAllEnabled = toggle.isOn;

            m_withoutWall.SetActive(viewAllEnabled);
            m_WallObj.SetActive(viewAllEnabled);

            if (viewAllEnabled)
            {
                m_Toggle_Frame.isOn = false;
                m_Toggle_Wall.isOn = false;
                m_Toggle_MEPF.isOn = false;
                m_Toggle_Mechanical.isOn = false;
                m_Toggle_Plumbing.isOn = false;
                m_Toggle_FireProtection.isOn = false;
            }
        }
        else if (toggle == m_Toggle_Frame)
        {
            if (toggle.isOn)
            {
                m_Toggle_ViewAll.isOn = false;
            }
            m_FrameObj.SetActive(toggle.isOn);
        }
        else if (toggle == m_Toggle_Wall)
        {
            if (toggle.isOn)
            {
                m_Toggle_ViewAll.isOn = false;
            }
            m_WallObj.SetActive(toggle.isOn);
        }
        else if (toggle == m_Toggle_MEPF && !toggle.isOn)
        {
            m_Toggle_Mechanical.isOn = false;
            m_Toggle_Plumbing.isOn = false;
            m_Toggle_FireProtection.isOn = false;
        }
        else if (toggle == m_Toggle_Mechanical)
        {
            if (toggle.isOn)
            {
                m_Toggle_ViewAll.isOn = false;
            }
            m_MechanicalObj.SetActive(toggle.isOn);
        }
        else if (toggle == m_Toggle_Plumbing)
        {
            if (toggle.isOn)
            {
                m_Toggle_ViewAll.isOn = false;
            }
            m_PlumbingObj.SetActive(toggle.isOn);
        }
        else if (toggle == m_Toggle_FireProtection)
        {
            if (toggle.isOn)
            {
                m_Toggle_ViewAll.isOn = false;
            }
            m_FireProtectionObj.SetActive(toggle.isOn);
        }
    }
}
