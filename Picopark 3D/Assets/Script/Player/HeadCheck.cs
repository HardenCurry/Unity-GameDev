using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{
    Player_CT player;
    ElevatorMove Em;
    private void Awake()
    {
        player = transform.parent.GetComponent<Player_CT>();
        if (Em) GameObject.Find("Elevator").GetComponent<ElevatorMove>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GroundCheck")) //When Head->
        {
            player.jumpPow = 0;
            player.playerSum += other.transform.parent.GetComponent<Player_CT>().playerSum;
            if (Em) Em.RecalculatePlayerCt();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GroundCheck"))
        {
            player.jumpPow = player.originjumpPow;
            player.playerSum -= other.transform.parent.GetComponent<Player_CT>().playerSum;
            if (Em) Em.RecalculatePlayerCt();
        }
    }
}
