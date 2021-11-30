using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Manger : MonoBehaviour
{
    [SerializeField]
    List<SpriteRenderer> B_Ground = new List<SpriteRenderer>();

    public float Scroll_Speed;
    void Start()
    {
        for (int A = 0; A< transform.childCount; A++){
            B_Ground.Add(transform.GetChild(A).GetComponent<SpriteRenderer>());
        }
    }

    void Update()
    {
        for(int A = 1; A <= B_Ground.Count; A++)
        {
            B_Ground[A - 1].material.mainTextureOffset = new Vector2(B_Ground[A-1].material.mainTextureOffset.x, B_Ground[A - 1].material.mainTextureOffset.y+(Scroll_Speed/A)*Time.deltaTime);
        }
    }
}
