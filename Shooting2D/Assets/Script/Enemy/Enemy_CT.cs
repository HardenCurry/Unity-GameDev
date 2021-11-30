using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Enemy_Type
{
    Enemy_S,
    Enemy_M,
    Enemy_L,
    Boss
}
public class Enemy_CT : MonoBehaviour
{
    Animator Anim;
    Transform Player;
    SpriteRenderer SR;
    Enemy_Manager Manager;
    [Header("Assets")]
    public GameObject DestroyEffect;
    public Enemy_Type E_Type;
    public Enemy Info;    
    [Header("UI")]
    Slider HP_Bar;
    void Start()
    {
        Anim = GetComponent<Animator>();
        Manager = GameObject.Find("Enemy_Manager").GetComponent<Enemy_Manager>();
        HP_Bar = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        if (GameObject.Find("Player") != null)
        {
            Player = GameObject.Find("Player").GetComponent<Transform>();
        }
        SR = GetComponent<SpriteRenderer>();
        SR.sprite = Info.E_Image;
        Info.E_HP = Info.E_MaxHp;
        Set_HP();
    }
    void Update()
    {
        if (Player == null)
        {
            if (GameObject.Find("Player")!=null)
            {
                Player = GameObject.Find("Player").GetComponent<Transform>();
            }
        }
        if (E_Type == Enemy_Type.Boss && transform.position.y <= 2.7f)
        {
            Info.E_Speed = 0;
            transform.position = new Vector2(0, 2.7f);
        }
        transform.Translate(Vector2.down * Info.E_Speed * Time.deltaTime);
        if (Info.Shooting_Ready&&Player)
        {
            Info.Shooting_Ready = false;
            Shoot_Bullet();
            Invoke("Shoot_Cool", Info.Shooting_Cool);
        }
        Enemy_Dead();
    }
    void Set_HP()
    {
        HP_Bar.value = ((float)Info.E_HP / Info.E_MaxHp) * 100;
    }
    void Shoot_Cool()
    {
        Info.Shooting_Ready = true;
    }
    void Shoot_Bullet()
    {
        //*목표-자신=바라보는방향 벡터
        Vector3 Direction = Player.position - transform.position;
        float Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;//Rad2Deg=180/파이
        //Mathf.Atan2(float,float) y,x값을 받아서,해당 오브젝트를 바라보는 라디안 값을 출력하는 함수
        //Mathf.Rad2Deg
        //Atan,Asin,Acos 함수를 사용하여 도출한 라디안 값을 좌표값으로 변환해주는 Mathf 파생명령어
        if (Angle >= 90 && Angle <= 172) Angle = 172f;
        else if (Angle >= 8 && Angle < 90) Angle = 8;

        GameObject OBJ;
        switch (E_Type)
        {            
            case Enemy_Type.Enemy_L:
                OBJ = Pooling_Manager.E_Bullet_GetObj(E_Bullet_Type.E_Bullet_L);
                OBJ.transform.position = new Vector2(transform.position.x + 0.35f, transform.position.y - 0.5f);
                OBJ.transform.rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);
                OBJ= Pooling_Manager.E_Bullet_GetObj(E_Bullet_Type.E_Bullet_L);
                OBJ.transform.position = new Vector2(transform.position.x - 0.35f, transform.position.y - 0.5f);
                OBJ.transform.rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);
                //I0nstantiate(Info.E_Bullet, new Vector2(transform.position.x + 0.35f, transform.position.y - 0.5f), Quaternion.AngleAxis(Angle + 90, Vector3.forward));
                //Instantiate(Info.E_Bullet, new Vector2(transform.position.x - 0.35f, transform.position.y - 0.5f), Quaternion.AngleAxis(Angle + 90, Vector3.forward));
                break;
            case Enemy_Type.Enemy_M:
                OBJ = Pooling_Manager.E_Bullet_GetObj(E_Bullet_Type.E_Bullet_M);
                OBJ.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
                OBJ.transform.rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);
                //Instantiate(Info.E_Bullet, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.AngleAxis(Angle + 90, Vector3.forward));
                break;
            case Enemy_Type.Enemy_S:
                OBJ = Pooling_Manager.E_Bullet_GetObj(E_Bullet_Type.E_Bullet_S);
                OBJ.transform.position = new Vector2(transform.position.x, transform.position.y - 0.35f);
                OBJ.transform.rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);
                //Instantiate(Info.E_Bullet, new Vector2(transform.position.x, transform.position.y - 0.35f), Quaternion.AngleAxis(Angle + 90, Vector3.forward));
                break;
            case Enemy_Type.Boss:
                int randint = Random.Range(1, 3);//Range int [inclusive,exclusive],float [inclusive,inclusive] 
                if (randint == 1)
                {
                    OBJ = Pooling_Manager.E_Bullet_GetObj(E_Bullet_Type.E_Bullet_Boss);
                    OBJ.transform.position = new Vector2(transform.position.x + 1.5f, transform.position.y - 0.5f);
                    OBJ.transform.rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);
                    OBJ = Pooling_Manager.E_Bullet_GetObj(E_Bullet_Type.E_Bullet_Boss);
                    OBJ.transform.position = new Vector2(transform.position.x - 1.5f, transform.position.y - 0.5f);
                    OBJ.transform.rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);

                    OBJ = Pooling_Manager.E_Bullet_GetObj(E_Bullet_Type.E_Bullet_Boss);
                    OBJ.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f);
                    OBJ.transform.rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);
                    OBJ = Pooling_Manager.E_Bullet_GetObj(E_Bullet_Type.E_Bullet_Boss);
                    OBJ.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
                    OBJ.transform.rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);
                    //Instantiate(Info.E_Bullet, new Vector2(transform.position.x + 1.1f, transform.position.y - 0.5f), Quaternion.AngleAxis(Angle + 90, Vector3.forward));
                    //Instantiate(Info.E_Bullet, new Vector2(transform.position.x - 1.1f, transform.position.y - 0.5f), Quaternion.AngleAxis(Angle + 90, Vector3.forward));
                    //Instantiate(Info.E_Bullet, new Vector2(transform.position.x + 0.4f, transform.position.y - 0.5f), Quaternion.AngleAxis(Angle + 90, Vector3.forward));
                    //Instantiate(Info.E_Bullet, new Vector2(transform.position.x - 0.4f, transform.position.y - 0.5f), Quaternion.AngleAxis(Angle + 90, Vector3.forward));
                }
                else if (randint == 2)
                {
                    for (int A = 0; A <= 360; A += 10)
                    {
                        OBJ = Pooling_Manager.E_Bullet_GetObj(E_Bullet_Type.E_Bullet_Boss);
                        OBJ.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
                        OBJ.transform.rotation = Quaternion.AngleAxis(Angle + A, Vector3.forward);
                        //Instantiate(Info.E_Bullet, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.AngleAxis(Angle + A, Vector3.forward));
                    }
                }
                break;
            default:
                break;
        }
        //Quaternion.AngleAxis(float Angle,Vector Dir)
        //Dir축 기준 Angle만큼 회전
    }
    public void Hit_Function()//E_HP감소
    {
        Info.Enemy_Attacked();
        Set_HP();

        StartCoroutine(Hit_Effect());        
    }    
    IEnumerator Hit_Effect()//적 맞으면 깜빡이는거
    {
        SR.sprite = Info.E_Hit;
        yield return new WaitForSeconds(0.05f);
        SR.sprite = Info.E_Image;
        StopCoroutine(Hit_Effect());
    }
    public void Enemy_Dead()//Item떨굼
    {
        if(Info.E_HP <= 0)
        {
            StopAllCoroutines();
            Manager.Dead_Enemy(gameObject,0);
            Pooling_Manager.Enemy_ReturnObj(gameObject,E_Type);
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);//폭발이벤트

            GameObject OBJ;
            if (E_Type == Enemy_Type.Boss)
            {
                OBJ = Pooling_Manager.Item_GetObj(0);
                OBJ.transform.position = transform.position;
                OBJ = Pooling_Manager.Item_GetObj(1);
                OBJ.transform.position = transform.position;
                OBJ = Pooling_Manager.Item_GetObj(2);
                OBJ.transform.position = transform.position;
            }
            //아이템 확률
            int rand = Random.Range(1, 101);
            if (rand <= 25)
            {
                OBJ = Pooling_Manager.Item_GetObj(0);
                OBJ.transform.position = transform.position;
                //Instantiate(Items[0], transform.position, Quaternion.identity);
            }
            else if (rand <= 45)
            {
                OBJ = Pooling_Manager.Item_GetObj(1);
                OBJ.transform.position = transform.position;
                //Instantiate(Items[1], transform.position, Quaternion.identity);
            }
            else if (rand <= 75)
            {
                OBJ = Pooling_Manager.Item_GetObj(2);
                OBJ.transform.position = transform.position;
                //Instantiate(Items[2], transform.position, Quaternion.identity);
            }
        }
        else if (transform.position.y<=-6)
        {
            StopAllCoroutines();
            Manager.Dead_Enemy(gameObject,1);
            Pooling_Manager.Enemy_ReturnObj(gameObject, E_Type);
        }
    }
}
