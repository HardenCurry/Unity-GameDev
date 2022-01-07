using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_CT : MonoBehaviour
{
    Player_CT[] Players;
    List<Transform> Players_trans = new List<Transform>(); // Transform[] Players_trans;

    public float camspeed = 6;

    void Start()
    {
        Vector3 avg_offset = new Vector3(0, 0, -10);
        Players = FindObjectsOfType<Player_CT>();

        // for (int i = 0; i < Players.Length; i++)
        // {
        //     Players_trans.Add(Players[i].transform);
        // }
        int b = 0;
        foreach (Player_CT PC in Players)
        {
            Players_trans.Add(Players[b].transform);
            b++;
        }

        for (int a = 0; a < Players_trans.Count; a++)
        {
            avg_offset += Players_trans[a].position;
        }
        transform.position = new Vector3(avg_offset.x / Players_trans.Count, transform.position.y, avg_offset.z);
    }

    void FixedUpdate()
    {
        Vector3 avg_offset = new Vector3(0, 0, -10);
        for (int a = 0; a < Players_trans.Count; a++)
        {
            avg_offset += Players_trans[a].position;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(avg_offset.x / Players_trans.Count, transform.position.y, avg_offset.z), Time.deltaTime * camspeed);
        if (transform.position.x <= 0)
        {
            transform.position = new Vector3(0, transform.position.y, avg_offset.z);
        }
    }
}
