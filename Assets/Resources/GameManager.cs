using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Conectando...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado");
        PhotonNetwork.JoinOrCreateRoom("room1", new RoomOptions() { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("En room1, somos " + PhotonNetwork.CurrentRoom.PlayerCount);
        playerPrefab = "Prefabs/Player" + PhotonNetwork.CurrentRoom.PlayerCount;
        //playerPrefab = "Prefabs/Player1";
        PhotonNetwork.Instantiate(playerPrefab, new Vector3(PhotonNetwork.CurrentRoom.PlayerCount - 2.0f, 0,0), Quaternion.identity);
    }
}
