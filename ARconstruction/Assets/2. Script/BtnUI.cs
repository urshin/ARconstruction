using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class BtnUI : MonoBehaviour
{
    public GameObject dropdownMenu; // 드롭 다운 메뉴
    private bool isMenuActive = false;  

    public CanvasScaler scaler;
    float fixedRatio = 16f / 9f; //해상도 Default
    float currentRatio = (float)Screen.width / (float)Screen.height; //현재 해상도 비율




    private void Start()
    {
        scaler = GetComponent<CanvasScaler>();
        dropdownMenu.SetActive(false); //메뉴 비활성화
        AdjustCanvasnScaler();
    }

    void AdjustCanvasnScaler()
    {
        // 현재 해상도 가로 비율이 더 길 경우
        if (currentRatio > fixedRatio)
        {
            scaler.matchWidthOrHeight = 1; // 가로 비율 유지
        }
        // 현재 해상도의 세로 비율이 더 길 경우
        else if (currentRatio < fixedRatio)
        {
            scaler.matchWidthOrHeight = 0; // 세로 비율 유지
        }
    }

    public void ToggleDropDown()
    {
        isMenuActive = !isMenuActive;
        dropdownMenu.SetActive(isMenuActive);
    }

}
