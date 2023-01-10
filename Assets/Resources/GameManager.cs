using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string playerPrefab;
    [SerializeField] public string playerId;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Conectando...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado");
        PhotonNetwork.JoinOrCreateRoom("room1", new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("En room1, somos " + PhotonNetwork.CurrentRoom.PlayerCount);
        playerPrefab = "Prefabs/Player" + PhotonNetwork.CurrentRoom.PlayerCount;
        playerId = PhotonNetwork.CurrentRoom.PlayerCount.ToString(); 
        GameObject go = GameObject.Find("id");
        go.GetComponent<TMPro.TextMeshProUGUI>().text = playerId;
        //GameObject.FindWithTag("id").GetComponent<TextMeshPro>().text = playerId;
        float playerX = -11.0f + (PhotonNetwork.CurrentRoom.PlayerCount - 1) * 22.0f;
        Vector3 camPosition = Camera.main.transform.position;
        camPosition.x = playerX;
        Camera.main.transform.position = camPosition;
        PhotonNetwork.Instantiate(playerPrefab, new Vector3(playerX, 0,0), Quaternion.identity);

    }
}
