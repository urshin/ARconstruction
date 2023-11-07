using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowObjs : MonoBehaviour
{
    #region Toggle
    Toggle m_Toggle_ViewAll;
    Toggle m_Toggle_Frame;
    Toggle m_Toggle_Wall;
    Toggle m_Toggle_MEPF;
    Toggle m_Toggle_Mechanical;
    Toggle m_Toggle_Plumbing;
    Toggle m_Toggle_FireProtection;
    #endregion

    #region Button
    [SerializeField] Button m_resetBtn;
    [SerializeField] Button m_deleteBtn;
    #endregion

    #region GameObject
    GameObject m_withoutWall;
    GameObject m_FrameObj;
    GameObject m_WallObj;
    GameObject m_MechanicalObj;
    GameObject m_PlumbingObj;
    GameObject m_FireProtectionObj;
    #endregion

    void Start()
    {
        Toggle[] togglesFromManager = ResourceManager.instance.toggles;
        m_Toggle_ViewAll = togglesFromManager[0];
        m_Toggle_Frame = togglesFromManager[1];
        m_Toggle_Wall = togglesFromManager[2];
        m_Toggle_MEPF = togglesFromManager[3];
        m_Toggle_Mechanical = togglesFromManager[4];
        m_Toggle_Plumbing = togglesFromManager[5];
        m_Toggle_FireProtection = togglesFromManager[6];

        GameObject[] objs = ResourceManager.instance.objects;
        m_withoutWall = objs[0];
        m_FrameObj = objs[1];
        m_WallObj = objs[2];
        m_MechanicalObj = objs[3];
        m_PlumbingObj = objs[4];
        m_FireProtectionObj = objs[5];

        ToggleInit();

        foreach (Toggle toggle in togglesFromManager)
        {
            toggle.onValueChanged.AddListener(delegate
            {
                ToggleValueChanged(toggle); // 토글 값 변경 이벤트 핸들러를 등록
            });
        }

        m_resetBtn.onClick.AddListener(delegate
        {
            ToggleInit();
        });

        m_deleteBtn.onClick.AddListener(delegate
        {
            LostnDelete();
        });
    }

    public void ToggleInit()
    {
        // 토글 초기 설정
        m_Toggle_ViewAll.isOn = true; // 처음에 모두 보이게 설정
        m_Toggle_Frame.isOn = false;
        m_Toggle_Wall.isOn = false;
        m_Toggle_Mechanical.isOn = false;
        m_Toggle_Plumbing.isOn = false;
        m_Toggle_FireProtection.isOn = false;

        m_withoutWall.SetActive(true);
        m_FrameObj.SetActive(false);
        m_WallObj.SetActive(true);
        m_MechanicalObj.SetActive(false);
        m_PlumbingObj.SetActive(false);
        m_FireProtectionObj.SetActive(false);
    }

    void ToggleValueChanged(Toggle toggle)
    {
        if (toggle == m_Toggle_ViewAll)
        {
            bool viewAllEnabled = toggle.isOn;

            if (viewAllEnabled)
            {
                // 모든 다른 토글들을 비활성화
                m_Toggle_Frame.isOn = false;
                m_Toggle_Wall.isOn = false;
                m_Toggle_MEPF.isOn = false;
                m_Toggle_Mechanical.isOn = false;
                m_Toggle_Plumbing.isOn = false;
                m_Toggle_FireProtection.isOn = false;
            }

            m_withoutWall.SetActive(viewAllEnabled);
            m_WallObj.SetActive(viewAllEnabled);
        }
        else if (toggle == m_Toggle_Frame)
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                m_Toggle_ViewAll.isOn = false;
            }
            m_FrameObj.SetActive(toggle.isOn); // 프레임 오브젝트 활성화/비활성화
        }
        else if (toggle == m_Toggle_Wall)
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                m_Toggle_ViewAll.isOn = false;
            }
            m_WallObj.SetActive(toggle.isOn); // 벽 오브젝트 활성화/비활성화
        }
        else if (toggle == m_Toggle_MEPF && !toggle.isOn)
        {
            // MEPF 토글이 해제되면 다른 하위 토글들도 해제
            m_Toggle_Mechanical.isOn = false;
            m_Toggle_Plumbing.isOn = false;
            m_Toggle_FireProtection.isOn = false;
        }
        else if (toggle == m_Toggle_Mechanical)
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                m_Toggle_ViewAll.isOn = false;
            }
            m_MechanicalObj.SetActive(toggle.isOn); // 기계 오브젝트 활성화/비활성화
        }
        else if (toggle == m_Toggle_Plumbing)
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                m_Toggle_ViewAll.isOn = false;
            }
            m_PlumbingObj.SetActive(toggle.isOn); // 배관 오브젝트 활성화/비활성화
        }
        else if (toggle == m_Toggle_FireProtection)
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                m_Toggle_ViewAll.isOn = false;
            }
            m_FireProtectionObj.SetActive(toggle.isOn); // 소방 오브젝트 활성화/비활성화
        }
    }

    public void LostnDelete()
    {
        m_Toggle_ViewAll.isOn = false;
        m_Toggle_Frame.isOn = false;
        m_Toggle_Wall.isOn = false;
        m_Toggle_MEPF.isOn = false;
        m_Toggle_Mechanical.isOn = false;
        m_Toggle_Plumbing.isOn = false;
        m_Toggle_FireProtection.isOn = false;
    }
}