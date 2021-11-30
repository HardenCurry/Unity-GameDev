using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu_Manager : MonoBehaviour
{
    public static Menu_Manager Instance;
    [SerializeField] Menu[] menus;
    void Awake()
    {
        Instance = this;
    }
    public void OpenMenu(string MenuName)//calling it indenpendently
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuname == MenuName)
            {
                menus[i].Menu_Open();
            }
            else if (menus[i].Is_Open)  //If other menu were on,off them
            {
                CloseMenu(menus[i]);
            }
        }
    }
    public void OpenMenu(Menu menu)//when Using button
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].Is_Open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Menu_Open();
    }
    public void CloseMenu(Menu menu)
    {
        menu.Menu_Close();
    }
}
