using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class BtnUI : MonoBehaviour
{
    public GameObject dropdownMenu; // 드롭 다운 메뉴
    private bool isMenuActive = false;  



    private void Start()
    {
        dropdownMenu.SetActive(false); //메뉴 비활성화
    }



    public void ToggleDropDown()
    {
        isMenuActive = !isMenuActive;
        dropdownMenu.SetActive(isMenuActive);
    }

}
