using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BtnUI : MonoBehaviour
{
    Toggle[] toggles;
    enum ButtonIndex { Reset, Delete, Phase}
    Button[] buttons;

    [SerializeField] private Slider phaseSlider;
    [SerializeField] private GameObject dropdownMenu; //MEDF토글 활성화 시 나오는 드롭다운메뉴

    private bool isMenuActive = false; // Dropdown 메뉴 active값
    private bool isPhaseBtnOn = false; // 공정버튼 on/off bool값

    private ShowObjs showObjScript;

    private void Start()
    {
        //UI초기화
        InitializeUI();

        //ShowObjs.cs 참조
        showObjScript = FindObjectOfType<ShowObjs>();

        //ResourceManager에서 버튼 배열 참조
        buttons = ResourceManager.Instance.buttons;

        //공정 버튼 클릭 시 이벤트 리스너 할당 TogglePhase 메서드 실행
        buttons[(int)ButtonIndex.Phase].onClick.AddListener(BtnPhase);
    }

    public void ToggleDropDown()
    {
        isMenuActive = !isMenuActive;
        dropdownMenu.SetActive(isMenuActive);
    }
    private void InitializeUI()
    {
        //ResourceManager에서 리소스 로드
        toggles = ResourceManager.Instance.toggles;

        dropdownMenu.SetActive(false);
        phaseSlider.interactable = false;

        //모든 토글 Interactable = true 상태
        SetTogglesInteractable(true);
    }

    private void BtnPhase()
    {
        //버튼 클릭 시 bool값 변경
        isPhaseBtnOn = !isPhaseBtnOn;

        if (showObjScript != null)
        {
            if (isPhaseBtnOn)
            {
                //모든 Objs.SetActive = false; 상태로
                showObjScript.LostnDelete();
                //공정버튼 on일 경우 토글/기타 버튼들 Interactable = false;
                SetTogglenBtnInteractable(!isPhaseBtnOn);
            }

            else
            {
                //공정버튼off되면 슬라이더 값 0으로 초기화
                phaseSlider.value = 0;
                SetTogglenBtnInteractable(!isPhaseBtnOn);
                //ViewAll토글.isOn = true; 상태로
                showObjScript.Initialize();
            }
        }
    }

    private void SetTogglenBtnInteractable(bool interactable)
    {
        //토글 상태 설정
        SetTogglesInteractable(interactable);

        //버튼 상태 설정
        buttons[(int)ButtonIndex.Reset].interactable = interactable;
        buttons[(int)ButtonIndex.Delete].interactable = interactable;

        //공정 슬라이더 상태 설정
        phaseSlider.interactable = !interactable;
        //공정버튼off되면 슬라이더 값 0으로 초기화
        //if (!interactable)
        //    phaseSlider.value = 0;
    }

    private void SetTogglesInteractable(bool interactable)
    {
        foreach (Toggle toggle in toggles)
        {
            toggle.interactable = interactable;
        }
    }
}
