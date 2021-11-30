using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    [SerializeField] TMP_InputField roomNameIF;
    [SerializeField] TMP_Text roomName;
    [SerializeField] TMP_Text errorText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject roomListPrefab;
    [SerializeField] GameObject playerListPrefab;
    [SerializeField] GameObject StartGameBtn;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Debug.Log("Connectting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;  //Host change scene,other client scene changed
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        Menu_Manager.Instance.OpenMenu("MainMenu");
        // PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameIF.text))  //**IsNullOrEmpty
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameIF.text);
        Menu_Manager.Instance.OpenMenu("LoadingMenu");
    }
    public override void OnJoinedRoom()
    {
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        Menu_Manager.Instance.OpenMenu("RoomMenu");

        foreach (Transform trans in playerListContent) //따로추가한거
        {
            Destroy(trans.gameObject);
        }
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        StartGameBtn.SetActive(PhotonNetwork.IsMasterClient); //only Host can see
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartGameBtn.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        Menu_Manager.Instance.OpenMenu("ErrorMenu");
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);//Game
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Menu_Manager.Instance.OpenMenu("LoadingMenu");
    }
    public void JoinRoom(RoomInfo info)//
    {
        PhotonNetwork.JoinRoom(info.Name);
        Menu_Manager.Instance.OpenMenu("LoadingMenu");
    }
    public override void OnLeftRoom()
    {
        Menu_Manager.Instance.OpenMenu("MainMenu");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)//
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList) continue;  //when room was removed, RemovedFromList turn true
            Instantiate(roomListPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}