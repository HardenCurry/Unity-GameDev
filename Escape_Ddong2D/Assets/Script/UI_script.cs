using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_script : MonoBehaviour
{
    public static UI_script Instance;
    public Image[] health;
    public Sprite[] hp_sprite;//0 empty, 1 full
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        foreach (Image hp in health)
        {
            hp.sprite = hp_sprite[1];
        }
    }
    public void Gethurt(int hp)
    {
        for (int i = 0; i < health.Length; i++)
        {
            health[i].sprite = hp_sprite[0];
        }
        for (int i = 0; i < hp; i++)
        {
            health[i].sprite = hp_sprite[1];
        }
    }
}
