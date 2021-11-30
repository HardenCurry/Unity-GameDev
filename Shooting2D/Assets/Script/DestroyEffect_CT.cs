using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect_CT : MonoBehaviour
{
    Animator Anim;
    void Start()
    {
        Anim = GetComponent<Animator>();
        Invoke("Destroy",0.7f);
    }

    void Destroy()
    {
        Anim.StopPlayback();
        Destroy(gameObject);
    }
}
