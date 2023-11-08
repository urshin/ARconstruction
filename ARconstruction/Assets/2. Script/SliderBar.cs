using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    GameObject[] gameObjects;

    void Start()
    {
        // ResourceManager에서 오브젝트를 가져와 배열에 저장
        gameObjects = ResourceManager.instance.objects;

        // Slider의 값이 변경될 때 호출될 메서드 설정
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        // 먼저, 모든 오브젝트를 비활성화
        SetActiveObjects(false);

        // 값에 따라 특정 오브젝트들만 활성화
        switch (value)
        {
            case 0:
                SetActiveObjects(false);
                break;
            case 1:
                SetActiveForIndices(true, 3, 4, 5);
                break;
            case 2:
                SetActiveForIndices(true, 1, 3, 4, 5);
                break;
            case 3:
                SetActiveForIndices(true, 2, 3, 4, 5);
                break;
            default:
                gameObjects[0].SetActive(true);
                SetActiveForIndices(true, 2, 3, 4, 5);
                break;
        }
    }

    // 모든 오브젝트의 활성 상태를 설정하는 메서드
    private void SetActiveObjects(bool active)
    {
        foreach (GameObject obj in gameObjects)
        {
            if (obj != null)
            {
                obj.SetActive(active);
            }
        }
    }

    // 주어진 인덱스의 오브젝트만 활성화하는 메서드
    private void SetActiveForIndices(bool active, params int[] indices)
    {
        foreach (int i in indices)
        {
            if (i >= 0 && i < gameObjects.Length && gameObjects[i] != null)
            {
                gameObjects[i].SetActive(active);
            }
        }
    }
}
