using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextBlink : MonoBehaviour
{
    public TextMeshProUGUI tmptext;
    float currentTime;
    float blinkTime = 1.5f;
    bool isHide;
    void Start()
    {
        currentTime = 0;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (blinkTime <= currentTime)
        {
            if (isHide)
            {
                Blinktext();
            }
            else
            {
                Hidetext();
            }
        }
    }
    void Hidetext()
    {
        isHide = true;
        currentTime = 0;
        tmptext.enabled = true;
    }
    void Blinktext()
    {
        isHide = false;
        currentTime = 0;
        tmptext.enabled = false;
    }
}
