using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int Posx;
    public int Posy;
    public void Return_Target()
    {
        Target_Manager.ReturnObj(gameObject);
    }
}
