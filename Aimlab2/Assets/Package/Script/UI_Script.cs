using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Script : MonoBehaviour
{
    public TextMeshProUGUI Timer;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Accuracy;
    public Button Start_btn;
    public Gun_CT Gun_script;
    float startingTime = 30;
    float currentTime;
    void Awake()
    {
        Time.timeScale = 0;
    }
    void Start()
    {
        currentTime = startingTime;
        Score.text = "0";
        Accuracy.text = "0";
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        Timer.text = string.Format("0:{0:D2}", ((int)currentTime));
        Score.text = Target_Manager.Instance.score.ToString();
        if (Target_Manager.Instance.mouse_ct > 0)
        {
            Debug.Log(Target_Manager.Instance.shoot_ct + " " + Target_Manager.Instance.mouse_ct);
            Accuracy.text = ((int)((Target_Manager.Instance.shoot_ct * 100 / Target_Manager.Instance.mouse_ct))).ToString();
        }
        if (currentTime <= 0)
        {
            Time.timeScale = 0;
            currentTime = 0;
        }
    }
    public void Start_Game()
    {
        Start_btn.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; //커서안보이게
        Time.timeScale = 1;
        Gun_script.enabled = true;
    }
}
