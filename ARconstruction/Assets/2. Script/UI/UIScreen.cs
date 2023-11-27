using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField] GameObject signInPanel;
    [SerializeField] GameObject signUpPanel;

   public void OnButtonSignUp()
    {
        signInPanel.SetActive(false);
        signUpPanel.SetActive(true  );
    }
    public void OnButtonBack()
    {
        signInPanel.SetActive(true);
        signUpPanel.SetActive(false);
    }
}
