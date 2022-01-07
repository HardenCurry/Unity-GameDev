using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Player_Mng : MonoBehaviourPunCallbacks
{
    public static Player_Mng Instance;
    Player_CT[] Players;
    public int playersCount => Players.Length;
    Transform spawnPos;
    Camera cam;
    void Awake()
    {
        Instance = this;
        // Players = FindObjectsOfType<Player_CT>();
        spawnPos = GameObject.Find("SpawnPos").transform;
        cam = Camera.main;
    }
    void Start()
    {
        // StartStage();
        Photon_StartStage();
    }
    void StartStage()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].Startspawn(spawnPos.position + new Vector3(i * 1.5f, 0, 0));
        }
        cam.transform.position = new Vector3(0, 3, -10) + spawnPos.position; //x,z=0, y=-2 default setting
    }
    void Photon_StartStage()
    {
        // for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        // {
        //     PhotonNetwork.Instantiate("Player_Photon", spawnPos.position + new Vector3(-2 * i, 0, 0), Quaternion.identity);
        // }
        cam.transform.position = new Vector3(0, 3, -10) + spawnPos.position;
    }
}
