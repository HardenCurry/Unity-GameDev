using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item_type{
    Power,
    Boom,
    Coin
}
public class Item_CT : MonoBehaviour
{
    Rigidbody2D rigid;
    public float Item_speed;
    public Item_type type;
    float Lifespan=5.5f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        int RandInt = Random.Range(-1, 2);
        rigid.AddForce(new Vector2(RandInt, -0.58f) * Item_speed, ForceMode2D.Impulse);
        Invoke("Destroy_LifeSpan", Lifespan);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            switch (this.type)
            {
                case Item_type.Power:
                    if (collision.gameObject.GetComponent<Player_Controller>().Power_Lv < collision.gameObject.GetComponent<Player_Controller>().Power_max)
                    {
                        collision.gameObject.GetComponent<Player_Controller>().Power_Lv++;
                        collision.gameObject.GetComponent<Player_Controller>().PowerLvUpdate();
                        if (collision.gameObject.GetComponent<Player_Controller>().Power_Lv == 2)
                        {
                            collision.gameObject.GetComponent<Player_Controller>().Add_Follower();
                        }
                    }
                    break;
                case Item_type.Boom:
                    if (collision.gameObject.GetComponent<Player_Controller>().Boom_Ct < collision.gameObject.GetComponent<Player_Controller>().Boom_Max)
                    {
                        if (collision.gameObject.GetComponent<Player_Controller>().Boom_Ct <= collision.gameObject.GetComponent<Player_Controller>().Boom_Max-1)
                        {
                            collision.gameObject.GetComponent<Player_Controller>().Boom_Ct++;
                            collision.gameObject.GetComponent<Player_Controller>().BoomImageUpdate();
                        }
                    }
                    break;
                case Item_type.Coin:
                    collision.gameObject.GetComponent<Player_Controller>().Coin += 100;
                    collision.gameObject.GetComponent<Player_Controller>().CoinUpdate();
                    break;
            }
            Pooling_Manager.Item_ReturnObj(gameObject, type);
            //Destroy(gameObject);
        }
    }
    void Destroy_LifeSpan()
    {
        Destroy(gameObject);
    }
}

