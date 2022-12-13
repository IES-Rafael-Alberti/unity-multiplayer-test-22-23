using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
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
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Soy el master");
            PhotonNetwork.InstantiateRoomObject("Prefabs/Player", Vector3.zero, Quaternion.identity);
        }
    }
}
