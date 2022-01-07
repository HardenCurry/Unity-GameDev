using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Player_CT player;
    private void Awake()
    {
        player = transform.parent.GetComponent<Player_CT>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Head")) //When GroundCheck -> 
        {
            player.Isjump = false;
            if (other.gameObject.CompareTag("Head"))
            {
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Head"))
        {
            player.Isjump = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Head"))
        {
            player.Isjump = true;
        }
    }
}
