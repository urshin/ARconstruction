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
    [SerializeField] private GameObject dropdownMenu; //MEDF토글 활성화시 나오는 드롭다운메뉴

    private bool isMenuActive = false; // Dropdown 메뉴 active값
    private bool isPhaseBtnOn = true; // 공정버튼 on/off bool값

    private ShowObjs showObjScript;

    private void Start()
    {
        //UI초기화
        InitializeUI();

        //ShowObjs.cs 참조
        showObjScript = FindObjectOfType<ShowObjs>();

        //ResourceManager에서 버튼 배열 참조
        buttons = ResourceManager.instance.buttons;

        //공정 버튼 클릭 시 이벤트 리스너 할당 TogglePhase 메서드 실행
        buttons[(int)ButtonIndex.Phase].onClick.AddListener(TogglePhase);
    }

    public void ToggleDropDown()
    {
        isMenuActive = !isMenuActive;
        dropdownMenu.SetActive(isMenuActive);
    }
    private void InitializeUI()
    {
        //ResourceManager에서 Toggles 배열 참조
        toggles = ResourceManager.instance.toggles;

        dropdownMenu.SetActive(false);
        phaseSlider.interactable = false;

        //모든 토글 Interactable = true 상태
        SetTogglesInteractable(true);
    }

    private void TogglePhase()
    {
        if (showObjScript != null)
        {
            if (isPhaseBtnOn)
                //모든 Objs.SetActive = false; 상태로
                showObjScript.LostnDelete();
            else
                //ViewAll토글.isOn = true; 상태로
                showObjScript.ToggleInit();
        }

        //공정버튼 on일 경우 토글/기타 버튼들 Interactable = false;
        SetTogglenBtnInteractable(!isPhaseBtnOn);

        //버튼 눌리면 isPhaseBtnOn bool값 변경
        isPhaseBtnOn = !isPhaseBtnOn;
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
        if (!interactable)
            phaseSlider.value = 0;
    }

    private void SetTogglesInteractable(bool interactable)
    {
        foreach (Toggle toggle in toggles)
        {
            toggle.interactable = interactable;
        }
    }
}
