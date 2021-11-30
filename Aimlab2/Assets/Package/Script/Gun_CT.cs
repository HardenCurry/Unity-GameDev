using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_CT : MonoBehaviour
{
    RaycastHit Hit;
    Transform P_Camera;
    float MaxDistance = 30f;
    void Start()
    {
        P_Camera = Camera.main.transform;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            Target_Manager.Instance.mouse_ct++;
        }
    }
    void Shoot()
    {
        if (Physics.Raycast(P_Camera.position, P_Camera.forward, out Hit, MaxDistance))
        {
            if (Hit.collider.CompareTag("Target"))
            {
                Target tar = Hit.transform.GetComponent<Target>();
                tar.Return_Target();
                Target_Manager.Instance.shoot_ct++;
            }
            else if (Hit.collider.CompareTag("Aimlab"))
            {
                Target_Manager.Instance.score -= 500;
                if (Target_Manager.Instance.score < 0)
                {
                    Target_Manager.Instance.score = 0;
                }

            }
        }
    }
}
