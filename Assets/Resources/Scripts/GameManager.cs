using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static Dictionary<string, string> choices = new Dictionary<string, string>();
    
    [SerializeField] private string playerPrefab;
    [SerializeField] public static string playerId;
    [SerializeField] public GameObject myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Conexión Photon
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Conectando...");
    }

    public override void OnConnectedToMaster()
    {
        // Callback (método que se llama al producirse un evento)
        Debug.Log("Conectado");
        PhotonNetwork.JoinOrCreateRoom("room1", new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        // Callback tras entrar en la sala
        Debug.Log("En room1, somos " + PhotonNetwork.CurrentRoom.PlayerCount);
        // Dependiendo del número de jugadores, se elige la id del jugador TODO 
        playerId = PhotonNetwork.CurrentRoom.PlayerCount.ToString(); 
        // Elige prefab según id
        playerPrefab = "Prefabs/Player" + playerId;
        // Busca id para reflejarlo en el canvas
        GameObject go = GameObject.Find("id");
        go.GetComponent<TMPro.TextMeshProUGUI>().text = playerId;
        // Posición de cámara y jugadores
        float playerX = -11.0f + (PhotonNetwork.CurrentRoom.PlayerCount - 1) * 22.0f;
        // Ajusta cámara
        Vector3 camPosition = Camera.main.transform.position;
        camPosition.x = playerX;
        Camera.main.transform.position = camPosition;
        // Crea jugador
        myPlayer = PhotonNetwork.Instantiate(playerPrefab, new Vector3(playerX, 0,0), Quaternion.identity);
    }

    public static void Choice(string choice, string player)
    {
        choices[player] = choice;
        Debug.Log(choices.ToStringFull());
        if(choices.Count == 2)
            EventManagerCustom.EndGameEvent();
    }
}
