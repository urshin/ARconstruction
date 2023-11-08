using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : SingletonBehaviour<ResourceManager> //ΩÃ±€≈Ê ªÛº”
{
    //public static ResourceManager instance;

    public Toggle[] toggles;
    public GameObject[] objects;
    public Button[] buttons;

    private void Awake()
    {
        base.Awake();
        //ResourceManager.instance = this;
    }
}
