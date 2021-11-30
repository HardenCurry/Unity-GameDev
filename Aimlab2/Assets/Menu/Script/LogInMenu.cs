using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LogInMenu : Menu
{
    EventSystem system;
    public TMP_InputField email;
    public TMP_InputField password;
    public Selectable firstInput;
    public Button loginbtn;
    void Start()
    {
        system = EventSystem.current;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next;
            if (system.currentSelectedGameObject == null)
            {
                next = firstInput;
                next.Select();
            }
            else
            {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (next != null)
                {
                    next.Select();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            loginbtn.onClick.Invoke();
        }
    }
    public void LogIn()
    {
        if (string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(password.text))
        {
            return;
        }
        PlayerPrefs.SetString("Email", email.text);
        PlayerPrefs.SetString("Password", password.text);

        Menu_Manager.Instance.OpenMenu("Select");
    }
}
