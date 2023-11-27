using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowObjs : MonoBehaviour
{
    enum Toggle_Index { ViewAll, Frame, Wall, MEPF, Mechanical, Plumbing, FireProtection }
    enum Btn_Index { Reset, Delete, Phase,PopUp }
    enum Obj_Index { Else, Frame, Wall, Mechanical, Plumbing, FireProtection }
    
    Toggle[] toggles;
    Button[] buttons;
    GameObject[] objs;

    void Start()
    {
        //ResourceManager���� ���ҽ� �ε�
        var resourceManager = ResourceManager.Instance;
        toggles = resourceManager.toggles;
        objs = resourceManager.objects;
        buttons = resourceManager.buttons;

        //��� �ʱ�ȭ
        Initialize();

        //�̺�Ʈ ������ �߰�
        AddTogglesListeners();
        AddButtonListeners();
    }
    public void Initialize()
    {
        //��� �ʱ� ����
        for (int i = 0; i < toggles.Length; i++)
        {
            //ViewAll��۸� Ȱ��ȭ
            toggles[i].isOn = i == (int)Toggle_Index.ViewAll;
        }

        //������Ʈ �ʱ� ����
        for (int i = 0; i < objs.Length; i++)
        {
            //��� ������Ʈ Ȱ��ȭ
            objs[i].SetActive(true);
        }
    }
    private void AddTogglesListeners()
    {
        //��� ��ۿ� �̺�Ʈ ������ �߰�
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(isOn => OnToggleValueChanged(toggle));
        }
    }
    private void AddButtonListeners()
    {
        //Reset, Delete ��ư�� �̺�Ʈ ������ �߰�
        buttons[(int)Btn_Index.Reset].onClick.AddListener(Initialize);
        buttons[(int)Btn_Index.Delete].onClick.AddListener(LostnDelete);
        buttons[(int)Btn_Index.PopUp].onClick.AddListener(PopUp);
    }

    void OnToggleValueChanged(Toggle toggle)
    {
        //��� üũ���¿� ���� bool ����
        bool isToggleOn = toggle.isOn;

        //ViewAll ��� üũ�� ���
        if(toggle == toggles[(int)Toggle_Index.ViewAll])
        {
            //ViewAll����� üũ�� ���
            if (isToggleOn)
            {
                // ViewAll�� �ٸ� ��� üũ����
                for (int i = (int)Toggle_Index.Frame; i <= (int)Toggle_Index.FireProtection; i++)
                {
                    toggles[i].isOn = !isToggleOn;
                }
            }

            //��� ������Ʈ Ȱ��ȭ
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].SetActive(isToggleOn);
            }
        }
        else
        {
            //ViewAll��� �� �ٸ� ����� üũ�� ���
            if (isToggleOn)
            {
                //ViewAll��� üũ ����
                toggles[(int)Toggle_Index.ViewAll].isOn = false;
            }
            
            //Frame ��� Ŭ�� ��
            if(toggle == toggles[(int)Toggle_Index.Frame])
            {
                objs[(int)Obj_Index.Frame].SetActive(toggle.isOn);
            }
            //Wall ��� Ŭ�� ��
            else if (toggle == toggles[(int)Toggle_Index.Wall])
            {
                objs[(int)Obj_Index.Wall].SetActive(toggle.isOn);
            }
            //MEPF ��� Ŭ�� ��
            else if (toggle == toggles[(int)Toggle_Index.MEPF] && !toggle.isOn)
            {
                // MEPF ����� �����Ǹ� �ٸ� ���� ��۵鵵 ����
                for (int i = (int)Toggle_Index.Mechanical; i <= (int)Toggle_Index.FireProtection; i++)
                {
                    toggles[i].isOn = false;
                }
            }
            //Mechanical ��� Ŭ�� ��
            else if (toggle == toggles[(int)Toggle_Index.Mechanical])
            {
                objs[(int)Obj_Index.Mechanical].SetActive(toggle.isOn);
            }
            //Plumbing ��� Ŭ�� ��
            else if (toggle == toggles[(int)Toggle_Index.Plumbing])
            {
                objs[(int)Obj_Index.Plumbing].SetActive(toggle.isOn);
            }
            //FireProtection ��� Ŭ�� ��
            else if (toggle == toggles[(int)Toggle_Index.FireProtection])
            {
                objs[(int)Obj_Index.FireProtection].SetActive(toggle.isOn);
            }
        }
    }
    [SerializeField] GameObject _PopUp;
    bool ispopup =false;
    public void PopUp()
    {
        ispopup = !ispopup;

        _PopUp.SetActive(ispopup);
    }

    public void LostnDelete()
    {
        //��� ��� �������� -> ��� Objs�� ��Ȱ��ȭ��
        for(int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = false;
        }

        //��ۺ� ������Ʈ�� �������� ���� ��Ÿ ��������(â�� ��)�� ����
        objs[(int)Obj_Index.Else].SetActive(false);
    }
}