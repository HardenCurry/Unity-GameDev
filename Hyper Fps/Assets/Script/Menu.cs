using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuname;
    [HideInInspector] public bool Is_Open = false;
    public void Menu_Open()
    {
        Is_Open = true;
        gameObject.SetActive(true);
    }
    public void Menu_Close()
    {
        Is_Open = false;
        gameObject.SetActive(false);
    }
}
