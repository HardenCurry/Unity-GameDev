using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_CT : MonoBehaviour
{
    Animator anim;
    Animator G4_anim;
    bool Ispressed;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        G4_anim = GameObject.Find("G4").GetComponent<Animator>();
        Ispressed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!Ispressed)
            {
                anim.SetTrigger("Ispressed");
                G4_anim.SetTrigger("G4_btnpressed");
            }
            Ispressed = true;
        }
    }
}
