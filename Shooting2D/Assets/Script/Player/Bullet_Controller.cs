using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bullet_Type
{
    Player_Bullet_1,
    Player_Bullet_2,
    Follower_Bullet
}
public class Bullet_Controller : MonoBehaviour
{

    [Header("ÃÑ¾Ë Á¤º¸")]
    public float speed = 2.7f;
    public float Alive_Time = 3f;
    public Bullet_Type B_type;
  
    void Start()
    {
        Invoke("Remove_OBJ", Alive_Time);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")||collision.CompareTag("Boss"))
        {
            collision.GetComponent<Enemy_CT>().Hit_Function();
            Remove_OBJ();
        }
    }
    void Remove_OBJ()
    {
        if (gameObject != null)
        {
            Pooling_Manager.P_Bullet_ReturnObj(gameObject, B_type);
        }
    }
}
