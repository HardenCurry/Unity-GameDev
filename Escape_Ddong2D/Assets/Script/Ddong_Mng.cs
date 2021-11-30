using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ddong_Mng : MonoBehaviour
{
    public GameObject DdongPrefab;
    public float delayTime = 0.2f;
    float screenhalfwidth;
    float screenhalfheight;
    float currentTime;
    float Isdrop;
    void Start()
    {
        screenhalfheight = Camera.main.orthographicSize;
        screenhalfwidth = Camera.main.aspect * screenhalfheight;
    }

    void Update()
    {
        if (currentTime > delayTime)
        {
            Droping();
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
    void Droping()
    {
        float x = Random.Range(-screenhalfwidth, screenhalfwidth);
        Instantiate(DdongPrefab, new Vector2(x, 5), Quaternion.identity);
    }
}
