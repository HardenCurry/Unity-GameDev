using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower_CT : MonoBehaviour
{
    Transform Parent;
    Player_Controller Player;
    [Header("정보")]
    public float Shooting_Cool_F_Max = 0.26f;
    public float Shooting_Cool_F;
    public int Follow_Delay = 33;
    public Vector2 Follow_Pos;
    public Queue<Vector2> Parent_Pos;

    void Awake()
    {
        Parent = GameObject.Find("Player").GetComponent<Transform>();
        Parent_Pos = new Queue<Vector2>();
        Player = Parent.GetComponent<Player_Controller>();
    }
    void Update()
    {
        Limit_Pos();
        WatchParent();
        Follow();
        Shooting();
        Reload();
    }

    void WatchParent()
    {
        if (Player.Is_Moving == true)
        {
            Parent_Pos.Enqueue(Parent.position);
        }
        if (Parent_Pos.Count > Follow_Delay)//>=하면 두개생김,delta.time이 너무짧아서0.01
        {
            Follow_Pos = Parent_Pos.Dequeue();
        }
        else if (Parent_Pos.Count < Follow_Delay)
        {
            Follow_Pos = new Vector2(Parent.position.x + 0.7f, Parent.position.y);
        }
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
    void Follow()
    {
        transform.position = Follow_Pos;
    }
    void Shooting()
    {
        if (Input.GetMouseButton(0) && Shooting_Cool_F >= Shooting_Cool_F_Max)
        {
            GameObject OBJ= Pooling_Manager.P_Bullet_GetObj(Bullet_Type.Follower_Bullet);
            OBJ.transform.position = new Vector2(transform.position.x, transform.position.y + 0.25f);
            //Instantiate(F_Bullet, new Vector2(transform.position.x, transform.position.y + 0.25f), Quaternion.identity);
            Shooting_Cool_F = 0;
        }
    }
    void Reload()
    {
        Shooting_Cool_F += Time.deltaTime;
    }
}
