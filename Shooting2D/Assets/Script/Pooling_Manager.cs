using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling_Manager : MonoBehaviour
{
    //Destroy effect
    //Boom 1개
    //Follower 1개
    public static Pooling_Manager PM;
    [Header("적")]
    public GameObject P_Enemy_L;
    public GameObject P_Enemy_M;
    public GameObject P_Enemy_S;
    public GameObject P_Enemy_Boss;
    [Header("아이템")]
    public GameObject P_Power;
    public GameObject P_Boom;
    public GameObject P_Coin;
    [Header("플레이어 총알")]
    public GameObject P_Player_Bullet_1;
    public GameObject P_Player_Bullet_2;
    public GameObject P_Follower_Bullet;
    [Header("적 총알")]
    public GameObject P_Enemy_Bullet_S;
    public GameObject P_Enemy_Bullet_M;
    public GameObject P_Enemy_Bullet_L;
    public GameObject P_Enemy_Bullet_Boss;

    Queue<GameObject> Enemy_L = new Queue<GameObject>();
    Queue<GameObject> Enemy_M = new Queue<GameObject>();
    Queue<GameObject> Enemy_S = new Queue<GameObject>();
    Queue<GameObject> Enemy_Boss = new Queue<GameObject>();

    Queue<GameObject> Power = new Queue<GameObject>();
    Queue<GameObject> Boom = new Queue<GameObject>();
    Queue<GameObject> Coin = new Queue<GameObject>();

    Queue<GameObject> Player_Bullet_1 = new Queue<GameObject>();
    Queue<GameObject> Player_Bullet_2 = new Queue<GameObject>();
    Queue<GameObject> Follower_Bullet = new Queue<GameObject>();

    Queue<GameObject> Enemy_Bullet_S = new Queue<GameObject>();
    Queue<GameObject> Enemy_Bullet_M = new Queue<GameObject>();
    Queue<GameObject> Enemy_Bullet_L = new Queue<GameObject>();
    Queue<GameObject> Enemy_Bullet_Boss = new Queue<GameObject>();

    void Awake()
    {
        PM = this;
        Enemy_Initialize(5);
        Item_Initialize(4);
        P_Bullet_Initialize(50);
        E_Bullet_Initialize(25);
    }
    private GameObject CreateObj(GameObject Prefab)
    {
        GameObject Obj = Instantiate(Prefab, transform);
        Obj.SetActive(false);
        return Obj;
    }
    private void Enemy_Initialize(int ct)
    {
        for (int i = 0; i < ct; i++)
        {
            Enemy_L.Enqueue(CreateObj(P_Enemy_L));
        }
        for (int i = 0; i < ct; i++)
        {
            Enemy_M.Enqueue(CreateObj(P_Enemy_M));
        }
        for (int i = 0; i < ct; i++)
        {
            Enemy_S.Enqueue(CreateObj(P_Enemy_S));
        }
        for (int i = 0; i < 1; i++)
        {
            Enemy_S.Enqueue(CreateObj(P_Enemy_Boss));
        }
    }
    private void Item_Initialize(int ct)
    {
        for (int i = 0; i < ct; i++)
        {
            Power.Enqueue(CreateObj(P_Power));
        }
        for (int i = 0; i < ct; i++)
        {
            Boom.Enqueue(CreateObj(P_Boom));
        }
        for (int i = 0; i < ct; i++)
        {
            Coin.Enqueue(CreateObj(P_Coin));
        }
    }
    private void P_Bullet_Initialize(int ct)
    {
        for (int i = 0; i < ct * 2; i++)
        {
            Player_Bullet_1.Enqueue(CreateObj(P_Player_Bullet_1));
        }
        for (int i = 0; i < ct; i++)
        {
            Player_Bullet_2.Enqueue(CreateObj(P_Player_Bullet_2));
        }
        for (int i = 0; i < ct; i++)
        {
            Follower_Bullet.Enqueue(CreateObj(P_Follower_Bullet));
        }
    }
    private void E_Bullet_Initialize(int ct)
    {
        for (int i = 0; i < ct; i++)
        {
            Enemy_Bullet_S.Enqueue(CreateObj(P_Enemy_Bullet_S));
        }
        for (int i = 0; i < ct; i++)
        {
            Enemy_Bullet_M.Enqueue(CreateObj(P_Enemy_Bullet_M));
        }
        for (int i = 0; i < ct; i++)
        {
            Enemy_Bullet_L.Enqueue(CreateObj(P_Enemy_Bullet_L));
        }
        for (int i = 0; i < ct * 10; i++)
        {
            Enemy_Bullet_Boss.Enqueue(CreateObj(P_Enemy_Bullet_Boss));
        }
    }    
    //Enemy
    public static GameObject Enemy_GetObj(int ET)//0-L,1-M,2-S,3-Boss
    {
        switch (ET)
        {
            case 0:
                if (PM.Enemy_L.Count >= 1)
                {
                    GameObject Obj = PM.Enemy_L.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Enemy_L.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Enemy_L);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            case 1:
                if (PM.Enemy_M.Count >= 1)
                {
                    GameObject Obj = PM.Enemy_M.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Enemy_M.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Enemy_M);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            case 2:
                if (PM.Enemy_S.Count >= 1)
                {
                    GameObject Obj = PM.Enemy_S.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Enemy_S.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Enemy_S);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            case 3:
                if (PM.Enemy_Boss.Count >= 1)
                {
                    GameObject Obj = PM.Enemy_Boss.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Enemy_Boss.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Enemy_Boss);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            default:
                break;
        }        
        return null;
    }
    public static void Enemy_ReturnObj(GameObject Prefab, Enemy_Type E_Type)
    {
        switch (E_Type)
        {
            case Enemy_Type.Enemy_L:
                PM.Enemy_L.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            case Enemy_Type.Enemy_M:
                PM.Enemy_M.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            case Enemy_Type.Enemy_S:
                PM.Enemy_S.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            case Enemy_Type.Boss:
                PM.Enemy_Boss.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            default:
                break;
        }
    }
    //Item
    public static GameObject Item_GetObj(int a)//0-Power,1-Boom,2-Coin
    {
        switch (a)
        {
            case 0:
                if (PM.Power.Count >= 1)
                {
                    GameObject Obj = PM.Power.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Power.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Power);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            case 1:
                if (PM.Boom.Count >= 1)
                {
                    GameObject Obj = PM.Boom.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Boom.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Boom);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            case 2:
                if (PM.Coin.Count >= 1)
                {
                    GameObject Obj = PM.Coin.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Coin.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Coin);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            default:
                break;
        }
        return null;
    }
    public static void Item_ReturnObj(GameObject Prefab, Item_type Item_Type)
    {
        switch (Item_Type)
        {
            case Item_type.Power:
                PM.Power.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            case Item_type.Boom:
                PM.Boom.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            case Item_type.Coin:
                PM.Coin.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
        }
    }
    //P_Bullet
    public static GameObject P_Bullet_GetObj(Bullet_Type a)
    {
        switch (a)
        {
            case Bullet_Type.Player_Bullet_1:
                if (PM.Player_Bullet_1.Count >= 1)
                {
                    GameObject Obj = PM.Player_Bullet_1.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Player_Bullet_1.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Player_Bullet_1);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            case Bullet_Type.Player_Bullet_2:
                if (PM.Player_Bullet_2.Count >= 1)
                {
                    GameObject Obj = PM.Player_Bullet_2.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Player_Bullet_2.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Player_Bullet_2);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            case Bullet_Type.Follower_Bullet:
                if (PM.Follower_Bullet.Count >= 1)
                {
                    GameObject Obj = PM.Follower_Bullet.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Follower_Bullet.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Follower_Bullet);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            default:
                break;
        }
        return null;
    }
    public static void P_Bullet_ReturnObj(GameObject Prefab, Bullet_Type B_Type)
    {
        switch (B_Type)
        {
            case Bullet_Type.Player_Bullet_1:
                PM.Player_Bullet_1.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            case Bullet_Type.Player_Bullet_2:
                PM.Player_Bullet_2.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            case Bullet_Type.Follower_Bullet:
                PM.Follower_Bullet.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
        }
    }
    //E_Bullet
    public static GameObject E_Bullet_GetObj(E_Bullet_Type EB_T)
    {
        switch (EB_T)
        {
            case E_Bullet_Type.E_Bullet_S:
                if (PM.Enemy_Bullet_S.Count >= 1)
                {
                    GameObject Obj = PM.Enemy_Bullet_S.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Enemy_Bullet_S.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Enemy_Bullet_S);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            case E_Bullet_Type.E_Bullet_M:
                if (PM.Enemy_Bullet_M.Count >= 1)
                {
                    GameObject Obj = PM.Enemy_Bullet_M.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Enemy_Bullet_M.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Enemy_Bullet_M);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            case E_Bullet_Type.E_Bullet_L:
                if (PM.Enemy_Bullet_L.Count >= 1)
                {
                    GameObject Obj = PM.Enemy_Bullet_L.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Enemy_Bullet_L.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Enemy_Bullet_L);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            case E_Bullet_Type.E_Bullet_Boss:
                if (PM.Enemy_Bullet_L.Count >= 1)
                {
                    GameObject Obj = PM.Enemy_Bullet_Boss.Dequeue();
                    Obj.transform.SetParent(null);
                    Obj.SetActive(true);
                    return Obj;
                }
                else if (PM.Enemy_Bullet_Boss.Count <= 0)
                {
                    GameObject new_Obj = PM.CreateObj(PM.P_Enemy_Bullet_Boss);
                    new_Obj.transform.SetParent(null);
                    new_Obj.gameObject.SetActive(true);
                    return new_Obj;
                }
                break;
            default:
                break;
        }
        return null;
    }
    public static void E_Bullet_ReturnObj(GameObject Prefab, E_Bullet_Type EB_Type)
    {
        switch (EB_Type)
        {
            case E_Bullet_Type.E_Bullet_L:
                PM.Enemy_Bullet_L.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            case E_Bullet_Type.E_Bullet_M:
                PM.Enemy_Bullet_M.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            case E_Bullet_Type.E_Bullet_S:
                PM.Enemy_Bullet_S.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
            case E_Bullet_Type.E_Bullet_Boss:
                PM.Enemy_Bullet_Boss.Enqueue(Prefab);
                Prefab.SetActive(false);
                Prefab.transform.SetParent(PM.transform);
                break;
        }
    }
}