using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ddong : MonoBehaviour
{
    private void Start()
    {
        Invoke("destroy_self", 2f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player_CT>().life--;
            UI_script.Instance.Gethurt(other.GetComponent<Player_CT>().life);
            if (other.GetComponent<Player_CT>().life == 0)
            {
                other.GetComponent<Player_CT>().Die();
            }
        }
    }
    void destroy_self()
    {
        Destroy(gameObject);
    }
}
