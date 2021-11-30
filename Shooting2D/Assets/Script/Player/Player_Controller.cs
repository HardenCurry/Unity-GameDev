using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator Anim;
    GameObject OBJ_F;
    [Header("Asset")]
    Follower_CT Follower_CT;
    public GameObject Follower;    
    public GameObject Boom_Attack_Prefab;
    public Image[] Life;
    public Image[] Boom;
    public Text Coins;
    public TextMeshProUGUI PowerLV;
    public Image Boom_Cool_Image;
    public GameObject GameoverUI;
    [Header("플레이어 스텟")]
    float Nom_Speed_X = 3f;//기본속도
    float Slow_Speed_X; //느려짐
    public float Speed_X;
    float Speed_Y;

    int Player_MaxLife = 3;
    float Player_RespawnCool = 1f;
    float Player_InvicibilityCool = 1.55f;
    public int Player_Life;
    [Header("플레이어 상태")]
    public bool Can_Shooting=true;
    public bool Invincibility = false;
    public bool Is_Moving = false;
    public bool Player_Die = false;
    bool Can_Booming = true;
    [Header("아이템 관련")]
    public int Power_max =5;
    public int Power_Lv = 1;
    public int Boom_Max = 3;
    public int Boom_Ct = 0;
    public int Coin = 0;
    [Header("슈팅 관련")]
    float Shooting_Cool_1 = 0.13f;
    float Shooting_Cool_2 = 0.1f;
    float Shooting_Cool_3 = 0.12f;
    float Shooting_Cool_4 = 0.12f;
    float Boom_Cool = 5f;
    void Start()
    {
        Follower_CT = Follower.GetComponent<Follower_CT>();
        rigid = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Speed_X = Nom_Speed_X;
        Speed_Y = Speed_X *0.78f;
        Slow_Speed_X = Nom_Speed_X / 2;
        Anim.SetInteger("P_X", 0);
        Player_Life = Player_MaxLife;
        BoomImageUpdate();
        CoinUpdate();
        PowerLvUpdate();
        GameoverUI.SetActive(false);
    }
    void Update()
    {
        Limit_Pos();
        Move();
        Shift_slow();
        Shooting();
        Boom_Attack();   
    }
    //UI업데이트
    public void LifeImageUpdate()
    {
        if (Player_Life >= 2)
        {
            Player_Life--;
        }
        else if (Player_Life <= 1)
        {
            Player_Life--;
        }
        for (int i = 0; i <= Player_MaxLife-1; i++)//원래 이미지가 있어서
        {
            Life[i].color = new Color(1, 1, 1, 0);
        }
        for (int i = 0; i <= Player_Life-1; i++)//오류나나?
        {
            Life[i].color = new Color(1, 1, 1, 1);
        }
    }
    public void BoomImageUpdate()
    {
        for (int i = 0; i <= Boom_Max - 1; i++)
        {
            Boom[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i <= Boom_Ct - 1; i++)
        {
            Boom[i].color = new Color(1, 1, 1, 1);
        }
    }    
    public void CoinUpdate()
    {
        Coins.text = Coin.ToString("N0");//string.Format("{0}",Coin)
    }
    public void PowerLvUpdate()
    {
        PowerLV.text = string.Format("LV.{0}", Power_Lv);
    }
    //리스폰+무적+팔로워
    public void Respawn()
    {
        Player_Die = true;
        gameObject.SetActive(false);
        Destroy_Follower(OBJ_F);
        Invoke("Respawn_Cool",Player_RespawnCool);
    }
    void Respawn_Cool()
    {
        gameObject.transform.position = new Vector2(0, -4.3f);
        gameObject.SetActive(true);
        if (Power_Lv >= 2)
        {
            Add_Follower();
        }
        Anim.SetTrigger("T_INV");
        Invoke("InvicibilityCool", Player_InvicibilityCool);
    }
    void InvicibilityCool()
    {
        Invincibility = false;
    }
    //겜오버+Retry
    public void GameOver()
    {
        gameObject.SetActive(false);
        GameoverUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    //update 함수
    void Move()
    {
        float Offset_X = Input.GetAxisRaw("Horizontal");
        float Offset_Y = Input.GetAxisRaw("Vertical");
        rigid.velocity = new Vector2(Offset_X * Speed_X, Offset_Y * Speed_Y);
        if (rigid.velocity == Vector2.zero)
        {
            Is_Moving = false;
        }
        else
        {
            Is_Moving = true;
        }
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            Anim.SetInteger("P_X", (int)Offset_X);
        }
    }    
    void Shift_slow()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed_X = Slow_Speed_X;
            Speed_Y = Speed_X * 0.75f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed_X = Nom_Speed_X;
            Speed_Y = Speed_X * 0.75f;
        }
    }
    void Shooting()
    {
        if (Input.GetMouseButton(0) && Can_Shooting)
        {
            GameObject OBJ0;
            GameObject OBJ1;
            GameObject OBJ2;
            Can_Shooting = false;
            switch (Power_Lv)
            {
                case 1:
                    OBJ0 = Pooling_Manager.P_Bullet_GetObj(Bullet_Type.Player_Bullet_1);
                    OBJ0.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
                    //Instantiate(Bullet_Prefab_1, new Vector2(transform.position.x, transform.position.y + 0.5f)
                    //    , Quaternion.identity);
                    Invoke("Shooting_Delay", Shooting_Cool_1);
                    break;
                case 2://follower추가
                    OBJ0 = Pooling_Manager.P_Bullet_GetObj(Bullet_Type.Player_Bullet_1);
                    OBJ0.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
                    //Instantiate(Bullet_Prefab_1, new Vector2(transform.position.x, transform.position.y + 0.5f)
                    //    , Quaternion.identity);
                    Invoke("Shooting_Delay", Shooting_Cool_1);
                    break;
                case 3:
                    OBJ0 = Pooling_Manager.P_Bullet_GetObj(Bullet_Type.Player_Bullet_1);
                    OBJ0.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
                    //Instantiate(Bullet_Prefab_1, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
                    Invoke("Shooting_Delay", Shooting_Cool_2);
                    break;
                case 4:
                    OBJ0 = Pooling_Manager.P_Bullet_GetObj(Bullet_Type.Player_Bullet_1);
                    OBJ0.transform.position = new Vector2(transform.position.x + 0.22f, transform.position.y + 0.5f);
                    OBJ1 = Pooling_Manager.P_Bullet_GetObj(Bullet_Type.Player_Bullet_1);
                    OBJ1.transform.position = new Vector2(transform.position.x - 0.22f, transform.position.y + 0.5f);
                    //Instantiate(Bullet_Prefab_1, new Vector2(transform.position.x + 0.22f, transform.position.y + 0.5f)
                    //    , Quaternion.identity);//R
                    //Instantiate(Bullet_Prefab_1, new Vector2(transform.position.x - 0.22f, transform.position.y + 0.5f)
                    //    , Quaternion.identity);//L
                    Invoke("Shooting_Delay", Shooting_Cool_3);
                    break;
                case 5:
                    OBJ0 = Pooling_Manager.P_Bullet_GetObj(Bullet_Type.Player_Bullet_1);
                    OBJ0.transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.5f);
                    OBJ1 = Pooling_Manager.P_Bullet_GetObj(Bullet_Type.Player_Bullet_2);
                    OBJ1.transform.position = new Vector2(transform.position.x, transform.position.y + 0.7f);
                    OBJ2 = Pooling_Manager.P_Bullet_GetObj(Bullet_Type.Player_Bullet_1);
                    OBJ2.transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.5f);
                    //Instantiate(Bullet_Prefab_1, new Vector2(transform.position.x + 0.3f, transform.position.y + 0.5f)
                    //    , Quaternion.identity);
                    //Instantiate(Bullet_Prefab_2, new Vector2(transform.position.x, transform.position.y + 0.7f)
                    //    , Quaternion.identity);
                    //Instantiate(Bullet_Prefab_1, new Vector2(transform.position.x - 0.3f, transform.position.y + 0.5f)
                    //    , Quaternion.identity);
                    Invoke("Shooting_Delay", Shooting_Cool_4);
                    break;
            }
        }
    }
    void Shooting_Delay()
    {
        Can_Shooting = true;
    }
    //폭탄"Z"
    void Boom_Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Can_Booming)
        {
            if (Boom_Ct > 0)
            {
                Boom_Cool_Image.fillAmount = 0;
                Can_Booming = false;
                Instantiate(Boom_Attack_Prefab, new Vector2(transform.position.x, transform.position.y + 2f), Quaternion.identity);
                Boom_Ct--;
                BoomImageUpdate();
            }
        }
        if (!Can_Booming)
        {
            Boom_Cool_Image.fillAmount += 1 / Boom_Cool * Time.deltaTime;
            if (Boom_Cool_Image.fillAmount >= 1)
            {
                Boom_Cool_Image.fillAmount = 1;
                Can_Booming = true;
            }
        }
    }
    //팔로워
    public void Add_Follower()
    {
        OBJ_F = Instantiate(Follower, transform.position, Quaternion.identity);
    }
    public void Destroy_Follower(GameObject OBJ_F)
    {
        Destroy(OBJ_F);
    }
    
    void Limit_Pos()
    {
        if (transform.position.x >= 2.5f)
        {
            transform.position = new Vector2(2.5f, transform.position.y);
        }
        else if (transform.position.x <= -2.5f)
        {
            transform.position = new Vector2(-2.5f, transform.position.y);
        }

        if (transform.position.y >= 1.5f)
        {
            transform.position = new Vector2(transform.position.x, 1.5f);
        }
        else if (transform.position.y <= -4.5f)
        {
            transform.position = new Vector2(transform.position.x, -4.5f);  
        }
    }    
}
