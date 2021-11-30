using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Player_Controller player;
    private void Awake()
    {
        player = GetComponentInParent<Player_Controller>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("Ground"))
        // {
        //     player.SetGrondedState(true);
        // }
        if (other.gameObject == player.gameObject) return;
        player.SetGrondedState(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player.gameObject) return;
        player.SetGrondedState(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject) return;
        player.SetGrondedState(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player.gameObject) return;
        player.SetGrondedState(true);
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject == player.gameObject) return;
        player.SetGrondedState(true);
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player.gameObject) return;
        player.SetGrondedState(false);
    }
}
