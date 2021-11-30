using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom_Attack_CT : MonoBehaviour
{
    [Header("ÆøÅº Á¤º¸")]
    public float AliveTime=2.3f;
    public float Speed = 0.6f;
    public float turn_Speed = 60f;
    void Start()
    {
        Invoke("Remove_Boom",AliveTime);
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + Speed * Time.deltaTime);
        transform.Rotate(Vector3.forward, turn_Speed * Time.deltaTime,Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy_Bullet")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Boss")
        {
            collision.GetComponent<Enemy_CT>().Info.E_HP -= 20;
        }
    }
    void Remove_Boom()
    {
        Destroy(gameObject);
    }
}
