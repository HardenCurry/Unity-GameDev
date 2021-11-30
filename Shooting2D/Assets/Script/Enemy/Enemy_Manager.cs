using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_Manager : MonoBehaviour
{
    float Min_X = -2;
    float Max_X = 2;
    public Text BossCt_Text;
    [Header("����ü prefab")]
    public GameObject E_Boss;
    [Header("������ ��ü"),SerializeField]
    List<GameObject> E_List = new List<GameObject>();
    List<GameObject> Boss_List = new List<GameObject>();
    [Header("��Ÿ ������")]
    int Enemy_Type_Ct=3;
    public bool Is_Spawn;
    public int Max_Count;
    public int Boss_Count;
    public bool Is_Boss;
    void Update()
    {
        if (!Is_Spawn&&E_List.Count<Max_Count&&!Is_Boss)
        {
            Is_Spawn = true;
            Spawn_Enemy();
        }
        if (Boss_Count <= 0&&Boss_List.Count==0)
        {
            GameObject Boss = Pooling_Manager.Enemy_GetObj(3);
            Boss.transform.position = new Vector2(0, 6.5f);
            Boss_List.Add(Boss);
            Is_Boss = true;
        }
        BossCt_Text.text = Boss_Count.ToString();
    }
    void Spawn_Enemy()
    {
        int Rand_Count = Random.Range(0, Enemy_Type_Ct);
        float Spawn_Cool = Random.Range(2.5f,3.0f);
        Vector2 Pos = new Vector2(Random.Range(Min_X, Max_X), 6);
        //Ǯ������
        GameObject OBJ;
        OBJ = Pooling_Manager.Enemy_GetObj(Rand_Count);
        OBJ.transform.parent = gameObject.transform;//�θ�����
        OBJ.transform.position = Pos;
        E_List.Add(OBJ);
        Invoke("Set_Spawn", Spawn_Cool);
    }
    void Set_Spawn()
    {
        Is_Spawn = false;
    }
    public void Dead_Enemy(GameObject OBJ,int a)//a=0�� E.HP=0,a=1�� y�Ѿ��
    {
        Enemy_CT CT = OBJ.GetComponent<Enemy_CT>();
        if (Boss_List.Count == 0)
        {
            if (a == 0)
            {
                Boss_Count--;
            }
        }
        if (CT.E_Type != Enemy_Type.Boss)
        {
            E_List.Remove(OBJ);            
        }
        else if(CT.E_Type==Enemy_Type.Boss)
        {
            Is_Boss = false;
            Boss_List.Remove(OBJ);
            Boss_Count = 15;
        }        
    }
}