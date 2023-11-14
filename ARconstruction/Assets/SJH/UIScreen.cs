using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.Events;

public class UIScreen : MonoBehaviour
{
    [SerializeField] private UIScreen previousScreen = null;
    public UIScreen activeScreen;


    public void Focus(UIScreen screen)
    {
        if (screen == activeScreen)
            return;

        if (activeScreen)
            activeScreen.Defocus();

        screen.previousScreen = activeScreen;

        activeScreen = screen;

        screen.Focus();
    }
    void Focus()
    {
        if (gameObject)
        {
            gameObject.SetActive(true);
        }
    }
    public void Defocus()
    {
        if (gameObject)
        {
            gameObject.SetActive(false);
        }
    }


}
