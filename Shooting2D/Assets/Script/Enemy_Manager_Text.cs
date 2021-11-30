using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Enemy_Manager_Text : MonoBehaviour
{
    public float nextSpawnDelay;
    public float SpawnDelay;

    public List<Spawn_Class> SpawnList;
    public int spawnIndex;
    public bool spawnEnd;
    void Awake()
    {
        SpawnList = new List<Spawn_Class>();
        ReadFile();
    }
    void Update()
    {
        int enemyIndex = 0;
        switch (SpawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
        }
        int enemyPoint = SpawnList[spawnIndex].point;
    }
    void ReadFile()
    {
        SpawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage 0") as TextAsset;
        StringReader reader = new StringReader(textFile.text);
        while (reader != null)
        {
            string line = reader.ReadLine();//한줄씩
            Debug.Log(line);
            if (line == null) break;

            Spawn_Class spawnData = new Spawn_Class();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            SpawnList.Add(spawnData);
        }
        reader.Close();//무조건닫기        

        //nextSpawnDelay = SpawnList[0].delay;
    }        

}
