using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowObjs : MonoBehaviour
{
    enum Toggle_Index { ViewAll, Frame, Wall, MEPF, Mechanical, Plumbing, FireProtection }
    enum Btn_Index { Reset, Delete, Phase }
    enum Obj_Index { WithoutWall, Frame, Wall, Mechanical, Plumbing, FireProtection }
    
    Toggle[] toggles;
    Button[] buttons;
    GameObject[] objs;

    void Start()
    {
        //ResourceManager에서 리소스 로드
        var resourceManager = ResourceManager.instance;
        toggles = resourceManager.toggles;
        objs = resourceManager.objects;
        buttons = resourceManager.buttons;

        //토글 초기화
        Initialize();

        //이벤트 리스너 추가
        AddTogglesListeners();
        AddButtonListeners();
    }
    public void Initialize()
    {
        //토글 초기 설정
        for (int i = 0; i < toggles.Length; i++)
        {
            //ViewAll토글만 활성화
            toggles[i].isOn = i == (int)Toggle_Index.ViewAll;
        }

        //toggles[(int)T_Index.ViewAll].isOn = true; // 처음에 모두 보이게 설정
        //toggles[(int)T_Index.Frame].isOn = false;
        //toggles[(int)T_Index.Wall].isOn = false;
        //toggles[(int)T_Index.Mechanical].isOn = false;
        //toggles[(int)T_Index.Plumbing].isOn = false;
        //toggles[(int)T_Index.FireProtection].isOn = false;

        //오브젝트 초기 설정
        for (int i = 0; i<objs.Length; i++)
        {
            //건물 외관 오브젝트만 활성화
            objs[i].SetActive(i == (int)Obj_Index.WithoutWall || i == (int)Obj_Index.Wall);
        }

        //objs[(int)O_Index.WithoutWall].SetActive(true);
        //objs[(int)O_Index.Frame].SetActive(false);
        //objs[(int)O_Index.Wall].SetActive(true);
        //objs[(int)O_Index.Mechanical].SetActive(false);
        //objs[(int)O_Index.Plumbing].SetActive(false);
        //objs[(int)O_Index.FireProtection].SetActive(false);
    }
    private void AddTogglesListeners()
    {
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(isOn => OnToggleValueChanged(toggle));
        }
    }
    private void AddButtonListeners()
    {
        buttons[(int)Btn_Index.Reset].onClick.AddListener(Initialize);
        buttons[(int)Btn_Index.Delete].onClick.AddListener(LostnDelete);
    }


    void OnToggleValueChanged(Toggle toggle)
    {
        if (toggle == toggles[(int)Toggle_Index.ViewAll])
        {
            bool viewAllEnabled = toggle.isOn;

            if (viewAllEnabled)
            {
                // 모든 다른 토글들을 비활성화
                toggles[(int)Toggle_Index.Frame].isOn = false;
                toggles[(int)Toggle_Index.Wall].isOn = false;
                toggles[(int)Toggle_Index.MEPF].isOn = false;
                toggles[(int)Toggle_Index.Mechanical].isOn = false;
                toggles[(int)Toggle_Index.Plumbing].isOn = false;
                toggles[(int)Toggle_Index.FireProtection].isOn = false;
            }

            objs[(int)Obj_Index.WithoutWall].SetActive(viewAllEnabled);
            objs[(int)Obj_Index.Wall].SetActive(viewAllEnabled);
        }
        else if (toggle == toggles[(int)Toggle_Index.Frame])
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                toggles[(int)Toggle_Index.ViewAll].isOn = false;
            }
            objs[(int)Obj_Index.Frame].SetActive(toggle.isOn); // 프레임 오브젝트 활성화/비활성화
        }
        else if (toggle == toggles[(int)Toggle_Index.Wall])
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                toggles[(int)Toggle_Index.ViewAll].isOn = false;
            }
            objs[(int)Obj_Index.Wall].SetActive(toggle.isOn); // 벽 오브젝트 활성화/비활성화
        }
        else if (toggle == toggles[(int)Toggle_Index.MEPF] && !toggle.isOn)
        {
            // MEPF 토글이 해제되면 다른 하위 토글들도 해제
            toggles[(int)Toggle_Index.Mechanical].isOn = false;
            toggles[(int)Toggle_Index.Plumbing].isOn = false;
            toggles[(int)Toggle_Index.FireProtection].isOn = false;
        }
        else if (toggle == toggles[(int)Toggle_Index.Mechanical])
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                toggles[(int)Toggle_Index.ViewAll].isOn = false;
            }
            objs[(int)Obj_Index.Mechanical].SetActive(toggle.isOn); // 기계 오브젝트 활성화/비활성화
        }
        else if (toggle == toggles[(int)Toggle_Index.Plumbing])
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                toggles[(int)Toggle_Index.ViewAll].isOn = false;
            }
            objs[(int)Obj_Index.Plumbing].SetActive(toggle.isOn); // 배관 오브젝트 활성화/비활성화
        }
        else if (toggle == toggles[(int)Toggle_Index.FireProtection])
        {
            if (toggle.isOn)
            {
                // 다른 토글 비활성화
                toggles[(int)Toggle_Index.ViewAll].isOn = false;
            }
            objs[(int)Obj_Index.FireProtection].SetActive(toggle.isOn); // 소방 오브젝트 활성화/비활성화
        }
    }

    public void LostnDelete()
    {
        //모든 토글 선택해제
        for(int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = false;
        }

        //toggles[(int)Toggle_Index.ViewAll].isOn = false;
        //toggles[(int)Toggle_Index.Frame].isOn = false;
        //toggles[(int)Toggle_Index.Wall].isOn = false;
        //toggles[(int)Toggle_Index.MEPF].isOn = false;
        //toggles[(int)Toggle_Index.Mechanical].isOn = false;
        //toggles[(int)Toggle_Index.Plumbing].isOn = false;
        //toggles[(int)Toggle_Index.FireProtection].isOn = false;
    }
}