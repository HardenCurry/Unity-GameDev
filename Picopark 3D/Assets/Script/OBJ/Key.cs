using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Player_CT player;
    public Vector3 offset;
    public float speed;
    bool IsOwned;
    float Xx = 0;
    private void Update()
    {
        if (IsOwned)
        {
            Vector3 vec3 = Vector3.zero;
            if (player.x != 0)
            {
                Xx = player.x;
            }
            vec3.x = -offset.x * Xx;
            vec3.y = offset.y;
            transform.position = Vector3.Lerp(transform.position, player.transform.position + vec3, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsOwned = true;
            player = other.transform.parent.GetComponent<Player_CT>();
        }
    }
}
