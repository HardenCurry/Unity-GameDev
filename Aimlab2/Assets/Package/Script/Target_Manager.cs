using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Manager : MonoBehaviour
{
    public static Target_Manager Instance;
    public GameObject P_Target;
    Queue<GameObject> Q_Target = new Queue<GameObject>();
    [SerializeField]
    public int score = 0;
    [HideInInspector]
    public int mouse_ct = 0;
    public int shoot_ct = 0;
    int T_Count = 3;
    bool[,] T_Pos = new bool[5, 5];
    int randx;
    int randy;
    void Start()
    {
        Instance = this;
        T_Instantiate();
        for (int i = 0; i < T_Count; i++)
        {
            GetObj();
        }
        for (int a = 0; a <= 4; a++)
        {
            for (int b = 0; b <= 4; b++)
            {
                T_Pos[a, b] = false;
            }
        }
    }
    GameObject Create_Target()
    {
        GameObject OBJ = Instantiate(P_Target, transform);
        OBJ.SetActive(false);
        return OBJ;
    }
    void T_Instantiate()
    {
        for (int i = 0; i <= 4; i++)
        {
            Q_Target.Enqueue(Create_Target());
        }
    }
    void GetObj()
    {
        if (Q_Target.Count > 0)
        {
            GameObject OBJ = Q_Target.Dequeue();
            do
            {
                randx = Random.Range(0, 5);
                randy = Random.Range(0, 5);
            } while (T_Pos[randx, randy]);
            T_Pos[randx, randy] = true;
            OBJ.GetComponent<Target>().Posx = randx;
            OBJ.GetComponent<Target>().Posy = randy;
            OBJ.transform.localPosition = new Vector3(randx, randy) * 1.3f;
            OBJ.SetActive(true);
        }
        else
        {
            GameObject OBJ = Create_Target();
            do
            {
                randx = Random.Range(0, 5);
                randy = Random.Range(0, 5);
            } while (T_Pos[randx, randy]);
            T_Pos[randx, randy] = true;
            OBJ.GetComponent<Target>().Posx = randx;
            OBJ.GetComponent<Target>().Posy = randy;
            OBJ.transform.localPosition = new Vector3(randx, randy) * 1.3f;
            OBJ.SetActive(true);
        }
    }
    public static void ReturnObj(GameObject OBJ)
    {
        Instance.T_Pos[OBJ.GetComponent<Target>().Posx, OBJ.GetComponent<Target>().Posy] = false;
        Instance.score += 1000;
        Instance.GetObj();
        Instance.Q_Target.Enqueue(OBJ);
        OBJ.SetActive(false);
    }
}
