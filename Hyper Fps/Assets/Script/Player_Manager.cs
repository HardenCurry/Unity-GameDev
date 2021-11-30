using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class Player_Manager : MonoBehaviour
{
    PhotonView PV;
    GameObject controller;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (PV.IsMine)  //IS_Owner? 
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Transform spawnpoint = Spawn_Manager.Instance.GetSpawnpoint();
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID });
    }
    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}