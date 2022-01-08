using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class Scene_Mng : MonoBehaviour
{
    public static Scene_Mng Instance;
    public int playerClearCt;
    Button[] btns;
    void Start()
    {
        if (FindObjectsOfType<Scene_Mng>().Length > 1)
        {
            Destroy(gameObject);
        }
        else DontDestroyOnLoad(gameObject);

        Instance = this;
        // btns = FindObjectsOfType<Button>();
        // for (int i = 0; i < btns.Length; i++)
        // {
        //     btns[i].onClick.AddListener(delegate { StartStage("1" + "-" + i.ToString()); });
        // }
    }
    void StartStage(string _sceneName)
    {
        SceneManager.LoadScene("Scene" + _sceneName);
    }

    void NextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ChangeStage(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void ClearStageCt() //When Player enter Door
    {
        playerClearCt++;
        if (playerClearCt == Player_Mng.Instance.playersCount)
        {
            NextStage();
        }
    }
}
