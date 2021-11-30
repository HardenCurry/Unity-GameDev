using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu_Manager : MonoBehaviour
{
    public static Menu_Manager Instance;
    public Menu[] menus;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        OpenMenu("LogIn");
    }
    public void OpenMenu(string _menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (_menu == menus[i].Menuname)
            {
                menus[i].gameObject.SetActive(true);
            }
            else
            {
                menus[i].gameObject.SetActive(false);
            }
        }
    }
    public void OpenMenu(Menu _menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (_menu == menus[i])
            {
                menus[i].gameObject.SetActive(true);
            }
            else
            {
                menus[i].gameObject.SetActive(false);
            }
        }
    }
}
