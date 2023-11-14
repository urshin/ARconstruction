using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public static SceneChange instance;
    public SceneChange Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    public void SceneLoad()
    {

        SceneManager.LoadScene("ARconstruction");

    }

}
