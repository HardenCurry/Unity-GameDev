using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class_Script
{
    
}
[System.Serializable]//인스펙터에 표기가능
public class Enemy
{
    public float Shooting_Cool;
    public bool Shooting_Ready = true;

    public int E_HP;
    public int E_MaxHp;
    public float E_Speed;
    public Sprite E_Image;
    public Sprite E_Hit;
    public GameObject E_Bullet;

    public void Enemy_Attacked()
    {
        E_HP--;
    }
}
