using System;
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
        var resourceManager = ResourceManager.Instance;
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

        //오브젝트 초기 설정
        for (int i = 0; i<objs.Length; i++)
        {
            //건물 외관 오브젝트만 활성화
            objs[i].SetActive(i == (int)Obj_Index.WithoutWall || i == (int)Obj_Index.Wall);
        }
    }
    private void AddTogglesListeners()
    {
        //모든 토글에 이벤트 리스너 추가
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(isOn => OnToggleValueChanged(toggle));
        }
    }
    private void AddButtonListeners()
    {
        //Reset, Delete 버튼에 이벤트 리스너 추가
        buttons[(int)Btn_Index.Reset].onClick.AddListener(Initialize);
        buttons[(int)Btn_Index.Delete].onClick.AddListener(LostnDelete);
    }

    void OnToggleValueChanged(Toggle toggle)
    {
        bool isToggleOn = toggle.isOn;

        //ViewAll 토글 체크일 경우
        if(toggle == toggles[(int)Toggle_Index.ViewAll])
        {
            SetViewAllToggleState(toggle);
        }
        else
        {
            //ViewAll토글 외 다른 토글이 체크될 경우
            if (isToggleOn)
            {
                //ViewAll토글 체크 해제
                toggles[(int)Toggle_Index.ViewAll].isOn = false;
            }
            
            //Frame 토글 클릭 시
            if(toggle == toggles[(int)Toggle_Index.Frame])
            {
                objs[(int)Obj_Index.Frame].SetActive(toggle.isOn);
            }
            //Wall 토글 클릭 시
            else if (toggle == toggles[(int)Toggle_Index.Wall])
            {
                objs[(int)Obj_Index.Wall].SetActive(toggle.isOn);
            }
            //MEPF 토글 클릭 시
            else if (toggle == toggles[(int)Toggle_Index.MEPF] && !toggle.isOn)
            {
                // MEPF 토글이 해제되면 다른 하위 토글들도 해제
                for (int i = (int)Toggle_Index.Mechanical; i <= (int)Toggle_Index.FireProtection; i++)
                {
                    toggles[i].isOn = false;
                }
            }
            //Mechanical 토글 클릭 시
            else if (toggle == toggles[(int)Toggle_Index.Mechanical])
            {
                objs[(int)Obj_Index.Mechanical].SetActive(toggle.isOn);
            }
            //Plumbing 토글 클릭 시
            else if (toggle == toggles[(int)Toggle_Index.Plumbing])
            {
                objs[(int)Obj_Index.Plumbing].SetActive(toggle.isOn);
            }
            //FireProtection 토글 클릭 시
            else if (toggle == toggles[(int)Toggle_Index.FireProtection])
            {
                objs[(int)Obj_Index.FireProtection].SetActive(toggle.isOn);
            }
        }
    }

    void SetViewAllToggleState(Toggle toggle)
    {
        bool viewAllEnabled = toggle.isOn;

        if (viewAllEnabled)
        {
            // ViewAll외 다른 토글 체크해제
            for (int i = (int)Toggle_Index.Frame; i <= (int)Toggle_Index.FireProtection; i++)
            {
                toggles[i].isOn = !viewAllEnabled;
            }
        }

        //건물 외관 Objs만 활성화
        objs[(int)Obj_Index.WithoutWall].SetActive(viewAllEnabled);
        objs[(int)Obj_Index.Wall].SetActive(viewAllEnabled);
    }

    public void LostnDelete()
    {
        //모든 토글 선택해제 -> 모든 Objs들 비활성화됨
        for(int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = false;
        }
    }
}