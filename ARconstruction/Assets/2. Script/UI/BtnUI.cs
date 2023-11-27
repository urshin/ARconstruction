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
    [SerializeField] private GameObject dropdownMenu; //MEDF��� Ȱ��ȭ �� ������ ��Ӵٿ�޴�

    private bool isMenuActive = false; // Dropdown �޴� active��
    private bool isPhaseBtnOn = false; // ������ư on/off bool��

    private ShowObjs showObjScript;

    private void Start()
    {
        //UI�ʱ�ȭ
        InitializeUI();

        //ShowObjs.cs ����
        showObjScript = FindObjectOfType<ShowObjs>();

        //ResourceManager���� ��ư �迭 ����
        buttons = ResourceManager.Instance.buttons;

        //���� ��ư Ŭ�� �� �̺�Ʈ ������ �Ҵ� TogglePhase �޼��� ����
        buttons[(int)ButtonIndex.Phase].onClick.AddListener(BtnPhase);
    }

    public void ToggleDropDown()
    {
        isMenuActive = !isMenuActive;
        dropdownMenu.SetActive(isMenuActive);
    }
    private void InitializeUI()
    {
        //ResourceManager���� ���ҽ� �ε�
        toggles = ResourceManager.Instance.toggles;

        dropdownMenu.SetActive(false);
        phaseSlider.interactable = false;

        //��� ��� Interactable = true ����
        SetTogglesInteractable(true);
    }

    private void BtnPhase()
    {
        //��ư Ŭ�� �� bool�� ����
        isPhaseBtnOn = !isPhaseBtnOn;

        if (showObjScript != null)
        {
            if (isPhaseBtnOn)
            {
                //��� Objs.SetActive = false; ���·�
                showObjScript.LostnDelete();
                //������ư on�� ��� ���/��Ÿ ��ư�� Interactable = false;
                SetTogglenBtnInteractable(!isPhaseBtnOn);
            }

            else
            {
                //������ưoff�Ǹ� �����̴� �� 0���� �ʱ�ȭ
                phaseSlider.value = 0;
                SetTogglenBtnInteractable(!isPhaseBtnOn);
                //ViewAll���.isOn = true; ���·�
                showObjScript.Initialize();
            }
        }
    }

    private void SetTogglenBtnInteractable(bool interactable)
    {
        //��� ���� ����
        SetTogglesInteractable(interactable);

        //��ư ���� ����
        buttons[(int)ButtonIndex.Reset].interactable = interactable;
        buttons[(int)ButtonIndex.Delete].interactable = interactable;

        //���� �����̴� ���� ����
        phaseSlider.interactable = !interactable;
        //������ưoff�Ǹ� �����̴� �� 0���� �ʱ�ȭ
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
