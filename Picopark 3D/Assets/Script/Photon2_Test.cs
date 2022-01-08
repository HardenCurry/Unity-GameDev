using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class Photon2_Test : MonoBehaviourPunCallbacks
{
    public Text Network_Status;
    public InputField CreateRoomInput;
    public InputField JoinRoomInput;
    public TextMeshProUGUI DebugError_tmp;
    public GameObject OnlineMode_Panel;
    //Room
    public GameObject Room_Panel;
    public TextMeshProUGUI RoomName_tmp;
    public GameObject StartBtn;
    void Start()
    {
        Open_OnlineMode_Panel(true);
        StartBtn.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;        
    }
    void Update() => Network_Status.text = PhotonNetwork.NetworkClientState.ToString();

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        //temporary setting
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateRoomInput.text == "" ? "Room" + Random.Range(0, 100) : CreateRoomInput.text, new RoomOptions { MaxPlayers = 2 });
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinRoomInput.text);
    }
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Open_OnlineMode_Panel(true);
    }
    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            PhotonNetwork.LoadLevel("Scene1-1");
        }
    }
    public override void OnCreatedRoom()
    {
        print("방 생성 성공");
    }
    public override void OnJoinedRoom()
    {
        print("Joined Success");
        Open_OnlineMode_Panel(false);
        if (PhotonNetwork.IsMasterClient == true)
        {
            StartBtn.SetActive(true);
        }
        else StartBtn.SetActive(false);
        //temp
        //RoomName_tmp.text = PhotonNetwork.CurrentRoom.Name;
        GameObject OBJ = PhotonNetwork.Instantiate("Player_Photon", Vector3.zero, Quaternion.identity);
        Player_CT player = OBJ.GetComponent<Player_CT>();
        int num = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        player.P_num = (Player_Num)num;      
    }
    public override void OnCreateRoomFailed(short returnCode, string message) => DebugError_tmp.text = "CreateRoom Failed" + ", " + message;
    public override void OnJoinRoomFailed(short returnCode, string message) => DebugError_tmp.text = "JoinRoom Failed" + ", " + message;
    public override void OnJoinRandomFailed(short returnCode, string message) => DebugError_tmp.text = "JoinRandomRoom Failed" + ", " + message;

    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            print("인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("최대 인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "플레이어 목록 :";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + " ,";
            print(playerStr);
        }
        else
        {
            print("인원 수 :" + PhotonNetwork.CountOfPlayers);
            print("방 개수 :" + PhotonNetwork.CountOfRooms);
            print("방에있는 모든인원 :" + PhotonNetwork.CountOfPlayersInRooms);
            print("로비에 있는지? :" + PhotonNetwork.InLobby);
            print("연결되었는지 :" + PhotonNetwork.IsConnected);
        }
    }
    void Open_OnlineMode_Panel(bool _b)
    {
        OnlineMode_Panel.SetActive(_b);
        Room_Panel.SetActive(!_b);
    }
}
