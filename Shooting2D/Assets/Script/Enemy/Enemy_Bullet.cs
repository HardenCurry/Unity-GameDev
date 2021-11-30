using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum E_Bullet_Type
{
    E_Bullet_S,
    E_Bullet_M,
    E_Bullet_L,
    E_Bullet_Boss
}
public class Enemy_Bullet : MonoBehaviour
{
    SpriteRenderer My_Renderer;
    [Header("ÃÑ¾Ë Á¤º¸")]
    public E_Bullet_Type EB_Type;
    public float Speed = 5f;
    public float lifespan = 3f;
    void Start()
    {
        My_Renderer = GetComponent<SpriteRenderer>();
        My_Renderer.flipY = true;
        Invoke("DestroyObj", lifespan);
    }

    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }
    void DestroyObj()
    {
        if (gameObject != null)
        {
            Pooling_Manager.E_Bullet_ReturnObj(gameObject, EB_Type);
            //Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !collision.GetComponent<Player_Controller>().Invincibility)
        {
            DestroyObj();
            collision.GetComponent<Player_Controller>().Invincibility = true;
            collision.GetComponent<Player_Controller>().LifeImageUpdate();
            if (collision.GetComponent<Player_Controller>().Player_Life >= 1)
            {
                collision.GetComponent<Player_Controller>().Respawn();
            }
            else if (collision.GetComponent<Player_Controller>().Player_Life == 0)
            {
                collision.GetComponent<Player_Controller>().GameOver();
            }
        }
    }
}
