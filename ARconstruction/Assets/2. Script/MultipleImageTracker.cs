using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultipleImageTracker : MonoBehaviour
{
    ARTrackedImageManager imageManager;

    // Start is called before the first frame update
    void Start()
    {
        // ARTrackedImageManager 컴포넌트를 가져옵니다.
        imageManager = GetComponent<ARTrackedImageManager>();

        // 이미지 트래킹 상태 변경 이벤트에 대한 콜백 함수를 등록합니다.
        imageManager.trackedImagesChanged += OnTrackedImage;
    }

    private void OnTrackedImage(ARTrackedImagesChangedEventArgs args)
    {
        // 새로운 이미지가 인식되었을 때의 처리
        foreach (ARTrackedImage trackedImage in args.added)
        {
            // 이미지의 이름을 가져옵니다. 이 이름은 이미지 라이브러리에서 설정한 것입니다.
            string imageName = trackedImage.referenceImage.name;

            // Resources 폴더에서 이미지의 이름과 동일한 이름을 가진 프리팹을 찾습니다.
            GameObject imagePrefab = Resources.Load<GameObject>(imageName);

            // 검색된 프리팹이 존재하는 경우
            if (imagePrefab != null)
            {
                // 이미지에 이미 자식 오브젝트가 없다면
                if (trackedImage.transform.childCount < 1)
                {
                    // 이미지의 위치와 회전에 해당하는 프리팹을 생성하고 이미지의 자식으로 설정합니다.
                    GameObject go = Instantiate(imagePrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    go.transform.SetParent(trackedImage.transform);
                }
            }
        }

        // 이미지 트래킹 중인 이미지들을 모두 순회
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            // 이미지에 자식 오브젝트가 있는 경우
            if (trackedImage.transform.childCount > 0)
            {
                // 자식 오브젝트의 위치와 회전을 이미지와 동기화시킵니다.
                trackedImage.transform.GetChild(0).transform.position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).transform.rotation = trackedImage.transform.rotation;
            }
        }

        //이미지 트래킹을 하다가 이미지가 사라지면
        foreach (ARTrackedImage trackedImage in args.removed)
        {
            // 이미지에 자식 오브젝트가 있는 경우
            if (trackedImage.transform.childCount > 0)
            {
                // 자식 오브젝트의 위치와 회전을 이미지와 동기화시킵니다.
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 이 함수에서는 프레임마다 어떤 작업도 수행하지 않음
    }
}