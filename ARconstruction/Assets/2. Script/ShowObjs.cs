using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowObjs : MonoBehaviour
{
    enum T_Index { ViewAll, Frame, Wall, MEPF, Mechanical, Plumbing, FireProtection }
    Toggle[] toggles;

    enum B_Index { Reset, Delete, Phase }
    Button[] buttons;

    enum O_Index { WithoutWall, Frame, Wall, Mechanical, Plumbing, FireProtection }
    GameObject[] objs;

    void Start()
    {
        //ResourceManager에서 리소스 로드
        toggles = ResourceManager.instance.toggles;
        objs = ResourceManager.instance.objects;
        buttons = ResourceManager.instance.buttons;

        //토글 초기화
        ToggleInit();

        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate
            {
                ToggleValueChanged(toggle); // 토글 값 변경 이벤트 핸들러를 등록
            });
        }

        buttons[(int)B_Index.Reset].onClick.AddListener(delegate
        {
            ToggleInit();
        });

        buttons[(int)B_Index.Delete].onClick.AddListener(delegate
        {
            LostnDelete();
        });
    }

    public void ToggleInit()
    {
        // 토글 초기 설정
        toggles[(int)T_Index.ViewAll].isOn = true; // 처음에 모두 보이게 설정
        toggles[(int)T_Index.Frame].isOn = false;
        toggles[(int)T_Index.Wall].isOn = false;
        toggles[(int)T_Index.Mechanical].isOn = false;
        toggles[(int)T_Index.Plumbing].isOn = false;
        toggles[(int)T_Index.FireProtection].isOn = false;

        objs[(int)O_Index.WithoutWall].SetActive(true);
        objs[(int)O_Index.Frame].SetActive(false);
        objs[(int)O_Index.Wall].SetActive(true);
        objs[(int)O_Index.Mechanical].SetActive(false);
        objs[(int)O_Index.Plumbing].SetActive(false);
        objs[(int)O_Index.FireProtection].SetActive(false);
    }

    void ToggleValueChanged(Toggle toggle)
    {
        if (toggle == toggles[(int)T_Index.ViewAll])
        {
            bool viewAllEnabled = toggle.isOn;

            if (viewAllEnabled)
            {
                // 모든 다른 토글들을 비활성화
                toggles[(int)T_Index.Frame].isOn = false;
                toggles[(int)T_Index.Wall].isOn = false;
                toggles[(int)T_Index.MEPF].isOn = false;
                toggles[(int)T_Index.Mechanical].isOn = false;
                toggles[(int)T_Index.Plumbing].isOn = false;
                toggles[(int)T_Index.FireProtection].isOn = false;
            }

            objs[(int)O_Index.WithoutWall].SetActive(viewAllEnabled);
            objs[(int)O_Index.Wall].SetActive(viewAllEnabled);
        }
        else if (toggle == toggles[(int)T_Index.Frame])
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                toggles[(int)T_Index.ViewAll].isOn = false;
            }
            objs[(int)O_Index.Frame].SetActive(toggle.isOn); // 프레임 오브젝트 활성화/비활성화
        }
        else if (toggle == toggles[(int)T_Index.Wall])
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                toggles[(int)T_Index.ViewAll].isOn = false;
            }
            objs[(int)O_Index.Wall].SetActive(toggle.isOn); // 벽 오브젝트 활성화/비활성화
        }
        else if (toggle == toggles[(int)T_Index.MEPF] && !toggle.isOn)
        {
            // MEPF 토글이 해제되면 다른 하위 토글들도 해제
            toggles[(int)T_Index.Mechanical].isOn = false;
            toggles[(int)T_Index.Plumbing].isOn = false;
            toggles[(int)T_Index.FireProtection].isOn = false;
        }
        else if (toggle == toggles[(int)T_Index.Mechanical])
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                toggles[(int)T_Index.ViewAll].isOn = false;
            }
            objs[(int)O_Index.Mechanical].SetActive(toggle.isOn); // 기계 오브젝트 활성화/비활성화
        }
        else if (toggle == toggles[(int)T_Index.Plumbing])
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                toggles[(int)T_Index.ViewAll].isOn = false;
            }
            objs[(int)O_Index.Plumbing].SetActive(toggle.isOn); // 배관 오브젝트 활성화/비활성화
        }
        else if (toggle == toggles[(int)T_Index.FireProtection])
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                toggles[(int)T_Index.ViewAll].isOn = false;
            }
            objs[(int)O_Index.FireProtection].SetActive(toggle.isOn); // 소방 오브젝트 활성화/비활성화
        }
    }

    public void LostnDelete()
    {
        toggles[(int)T_Index.ViewAll].isOn = false;
        toggles[(int)T_Index.Frame].isOn = false;
        toggles[(int)T_Index.Wall].isOn = false;
        toggles[(int)T_Index.MEPF].isOn = false;
        toggles[(int)T_Index.Mechanical].isOn = false;
        toggles[(int)T_Index.Plumbing].isOn = false;
        toggles[(int)T_Index.FireProtection].isOn = false;
    }
}