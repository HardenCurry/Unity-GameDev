using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class ScrollMenu : MonoBehaviour
{
    public List<VideoPlayer> VP = new List<VideoPlayer>();
    public GameObject scrollbar;
    float scroll_pos;
    float[] pos;
    int current;
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1 / (pos.Length - 1);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + distance / 2 && scroll_pos > pos[i] - distance / 2)
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.05f);
                }
            }
        }
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + distance / 2 && scroll_pos > pos[i] - distance / 2)
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1, 1), 0.1f);
                VP[i].Play();
            }
            else
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                VP[i].Pause();
            }
        }
    }
}
