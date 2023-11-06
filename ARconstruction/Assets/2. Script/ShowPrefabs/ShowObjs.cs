using UnityEngine;
using UnityEngine.UI;

public class ShowObjs : MonoBehaviour
{
    #region Toggle
    [SerializeField] Toggle m_Toggle_ViewAll; // 모든 오브젝트를 보여주는 토글
    [SerializeField] Toggle m_Toggle_Frame; // 프레임 오브젝트를 보여주는 토글
    [SerializeField] Toggle m_Toggle_Wall; // 벽 오브젝트를 보여주는 토글
    [SerializeField] Toggle m_Toggle_MEPF; // MEPF 오브젝트를 보여주는 토글
    [SerializeField] Toggle m_Toggle_Mechanical; // 기계 오브젝트를 보여주는 토글
    [SerializeField] Toggle m_Toggle_Plumbing; // 배관 오브젝트를 보여주는 토글
    [SerializeField] Toggle m_Toggle_FireProtection; // 소방 오브젝트를 보여주는 토글
    #endregion

    #region GameObject
    [SerializeField] GameObject m_withoutWall; // 벽 없이 보여지는 오브젝트
    [SerializeField] GameObject m_FrameObj; // 프레임 오브젝트
    [SerializeField] GameObject m_WallObj; // 벽 오브젝트
    [SerializeField] GameObject m_MechanicalObj; // 기계 오브젝트
    [SerializeField] GameObject m_PlumbingObj; // 배관 오브젝트
    [SerializeField] GameObject m_FireProtectionObj; // 소방 오브젝트
    #endregion

    void Start()
    {
        ToggleInit(); // 토글 초기 설정을 호출

        // 각 토글에 대한 이벤트 리스너를 추가
        Toggle[] toggles = new Toggle[]
        { m_Toggle_ViewAll, m_Toggle_Frame, m_Toggle_Wall, m_Toggle_MEPF, m_Toggle_Mechanical, m_Toggle_Plumbing, m_Toggle_FireProtection };

        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate
            {
                ToggleValueChanged(toggle); // 토글 값 변경 이벤트 핸들러를 등록
            });
        }
    }

    void ToggleInit()
    {
        // 토글 초기 설정
        m_Toggle_ViewAll.isOn = true; // 처음에 모두 보이게 설정
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

            m_withoutWall.SetActive(viewAllEnabled); // 벽 없이 보여지는 오브젝트 활성화/비활성화
            m_WallObj.SetActive(viewAllEnabled); // 벽 오브젝트 활성화/비활성화

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
}