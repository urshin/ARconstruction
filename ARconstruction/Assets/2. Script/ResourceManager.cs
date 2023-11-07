using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    public Toggle[] toggles;
    public GameObject[] objects;
    public Button[] buttons;

    private void Awake()
    {
        ResourceManager.instance = this;
    }
}
