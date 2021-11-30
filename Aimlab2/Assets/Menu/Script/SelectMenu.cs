using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectMenu : Menu
{
    public void OpenScene(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }
}