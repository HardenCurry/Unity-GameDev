using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen;
    void Start()
    {
        IsOpen = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            IsOpen = true;
            transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.black;
            Destroy(other.gameObject);
        }
    }
}
