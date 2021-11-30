using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
public class Player_CT : MonoBehaviour
{
    public JoyStick JS;
    public int life = 3;
    float speed = 5;
    public float x;
    Animator anim;
    SpriteRenderer Sr;
    float Originy;
    public bool Isalive;
    void Start()
    {
        anim = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
        Originy = transform.position.y;
        Isalive = true;
    }

    void Update()
    {
        if (!JS.isInput)
        {
            Move();
        }
        else
        {
            transform.position = new Vector2(transform.position.x, Originy);
        }
    }
    public void Move()
    {
        x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.position += Vector3.right * x;
        if (x == 0)
        {
            anim.SetBool("IsRun", false);
        }
        else if (x > 0)
        {
            anim.SetBool("IsRun", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (x < 0)
        {
            anim.SetBool("IsRun", true);
            // transform.eulerAngles = new Vector3(0, 180, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void JoystickMove(Vector3 movedir)
    {
        transform.position += movedir * speed * Time.deltaTime;
        if (movedir.x == 0)
        {
            anim.SetBool("IsRun", false);
        }
        else if (movedir.x > 0)
        {
            anim.SetBool("IsRun", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (movedir.x < 0)
        {
            anim.SetBool("IsRun", true);
            // transform.eulerAngles = new Vector3(0, 180, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void LoadScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Die()
    {
        Isalive = false;
        anim.SetTrigger("Die");
        GetComponent<Player_CT>().enabled = false;
        Invoke("LoadScene", 3f);
    }
}