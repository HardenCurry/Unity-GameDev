using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
enum Menu_State
{
    Enter,
    Menu1,
    Menu2
}
public class StartMenu_Mng : MonoBehaviour
{
    public GameObject Enter;
    public GameObject Menu1;
    public TextMeshProUGUI ModeQuestion_tmp;
    public Image YesOrNoImage;
    public TextMeshProUGUI Mode_tmp;
    string[] mode_arr = { "LOCAL PLAY MODE", "ONLINE PLAY MODE", "OPTION", "EXIT" };
    int page;
    Menu_State menuState;
    public bool IsYes;
    void Start()
    {
        Enter.SetActive(true);
        Menu1.SetActive(false);
        menuState = Menu_State.Enter;
    }

    bool InputFunc()
    {
        if (Input.GetButtonDown("Player1") && Input.GetAxisRaw("Player1") > 0)
        {
            return true;
        }
        else return false;
    }
    void Update()
    {
        //Enter
        if (menuState == Menu_State.Enter)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                menuState = Menu_State.Menu1;
                Enter.SetActive(false);
                Menu1.SetActive(true);
            }
        }
        else if (menuState == Menu_State.Menu1) //모드 고르는 부분
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuState = Menu_State.Enter;
                Enter.SetActive(true);
                Menu1.SetActive(false);
            }
            if (Input.GetButtonDown("Player1") && Input.GetAxisRaw("Player1") > 0)
            {
                Menu1_Btn_R();
            }
            if (Input.GetButtonDown("Player1") && Input.GetAxisRaw("Player1") < 0)
            {
                Menu1_Btn_L();
            }
            if (Input.GetButtonDown("Player1_Jump"))
            {
                menuState = Menu_State.Menu2;
                Menu1.SetActive(false);
                Menu2_Open(page);
            }
        }
        else if (menuState == Menu_State.Menu2)  //Yes or no
        {
            switch (page)
            {
                case 0:
                case 1:
                    if (Input.GetButtonDown("Player1") && Input.GetAxisRaw("Player1") > 0)
                    {
                        Menu2_YesOrNo(IsYes);
                    }
                    if (Input.GetButtonDown("Player1") && Input.GetAxisRaw("Player1") < 0)
                    {
                        Menu2_YesOrNo(IsYes);
                    }
                    if (Input.GetButtonDown("Player1_Jump") && IsYes)
                    {
                        if (page == 0) Scene_Mng.Instance.ChangeStage("Stage Menu");
                        else if (page == 1) Scene_Mng.Instance.ChangeStage("Online Menu");
                    }
                    else if (Input.GetButtonDown("Player1_Jump") && !IsYes)
                    {
                        menuState = Menu_State.Menu1;
                        Menu2_Close(page);
                        Menu1.SetActive(true);
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
    }
    public void Menu1_Btn_R()
    {
        page++;
        if (page >= mode_arr.Length)
        {
            page -= mode_arr.Length;
        }
        Mode_tmp.text = mode_arr[page];
    }
    public void Menu1_Btn_L()
    {
        page--;
        if (page <= -1)
        {
            page += mode_arr.Length;
        }
        Mode_tmp.text = mode_arr[page];
    }
    void Menu2_YesOrNo(bool _IsYes)
    {
        if (_IsYes)
        {
            IsYes = false;
            YesOrNoImage.rectTransform.localPosition = new Vector3(150, -100, 0);
        }
        else
        {
            IsYes = true;
            YesOrNoImage.rectTransform.localPosition = new Vector3(-150, -100, 0);
        }
    }
    void Menu2_Open(int _arrindex)
    {
        switch (_arrindex)
        {
            case 0:
            case 1:
            case 3:
                ModeQuestion_tmp.transform.parent.gameObject.SetActive(true);
                ModeQuestion_tmp.gameObject.SetActive(true);//Menu2_List activate
                ModeQuestion_tmp.text = mode_arr[page] + "?";
                Menu2_YesOrNo(IsYes);
                break;
            case 2:
                break;
        }
    }
    void Menu2_Close(int _arrindex)
    {
        switch (_arrindex)
        {
            case 0:
            case 1:
            case 3:
                ModeQuestion_tmp.transform.parent.gameObject.SetActive(false);
                ModeQuestion_tmp.gameObject.SetActive(false);//Menu2_List deactivate
                break;
            case 2:
                break;
        }
    }
}
