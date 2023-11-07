using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class BtnUI : MonoBehaviour
{
    ShowObjs showObjScript;

    #region Toggle
    [SerializeField] Toggle m_Toggle_ViewAll;
    [SerializeField] Toggle m_Toggle_Frame;
    [SerializeField] Toggle m_Toggle_Wall;
    [SerializeField] Toggle m_Toggle_MEPF;
    [SerializeField] Toggle m_Toggle_Mechanical;
    [SerializeField] Toggle m_Toggle_Plumbing;
    [SerializeField] Toggle m_Toggle_FireProtection;
    #endregion

    #region Button
    [SerializeField] Button m_resetBtn;
    [SerializeField] Button m_deleteBtn;
    [SerializeField] Button m_phaseBtn;
    #endregion

    [SerializeField] Slider phaseSlider;

    public GameObject dropdownMenu; // 드롭 다운 메뉴
    private bool isMenuActive = false;
    bool isPhaseBtnOn = true;

    private void Start()
    {
        UIInit();

        showObjScript = FindObjectOfType<ShowObjs>();

        m_phaseBtn.onClick.AddListener(delegate
        {
            ClickPhaseBtn();
        });
    }

    public void ToggleDropDown()
    {
        isMenuActive = !isMenuActive;
        dropdownMenu.SetActive(isMenuActive);
    }

    void UIInit()
    {
        //모든 토글 Interactable = true;
        m_Toggle_ViewAll.interactable = true;
        m_Toggle_Frame.interactable = true;
        m_Toggle_Wall.interactable = true;
        m_Toggle_MEPF.interactable = true;
        m_Toggle_Mechanical.interactable = true;
        m_Toggle_Plumbing.interactable = true;
        m_Toggle_FireProtection.interactable = true;

        dropdownMenu.SetActive(false); //메뉴 비활성화

        //공정slider = false;
        phaseSlider.interactable = false;
    }

    void ClickPhaseBtn()
    {
        if(isPhaseBtnOn == true)
        {

            if (showObjScript != null)
            {
                showObjScript.LostnDelete();
            }

            //모든 토글 Interactable = false;
            m_Toggle_ViewAll.interactable = false;
            m_Toggle_Frame.interactable = false;
            m_Toggle_Wall.interactable = false;
            m_Toggle_MEPF.interactable = false;
            m_Toggle_Mechanical.interactable = false;
            m_Toggle_Plumbing.interactable = false;
            m_Toggle_FireProtection.interactable = false;

            //부분별보기용 버튼(Reset/Delete) 클릭 불가
            m_resetBtn.interactable = false;
            m_deleteBtn.interactable = false;

            //공정slider = true;
            phaseSlider.interactable = true;

            isPhaseBtnOn = false;
        }
        else
        {
            if (showObjScript != null)
            {
                showObjScript.ToggleInit();
            }

            //모든 토글 Interactable = true;
            m_Toggle_ViewAll.interactable = true;
            m_Toggle_Frame.interactable = true;
            m_Toggle_Wall.interactable = true;
            m_Toggle_MEPF.interactable = true;
            m_Toggle_Mechanical.interactable = true;
            m_Toggle_Plumbing.interactable = true;
            m_Toggle_FireProtection.interactable = true;

            //부분별보기용 버튼(Reset/Delete) 클릭 가능
            m_resetBtn.interactable = true;
            m_deleteBtn.interactable = true;

            //공정slider = false;
            phaseSlider.value = 0;
            phaseSlider.interactable = false;

            isPhaseBtnOn = true;
        }
    }
}
