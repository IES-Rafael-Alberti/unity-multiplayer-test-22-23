using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    private GameObject myPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("rotatePlayer", OnRotatePlayer);
        EventManager.StartListening("choice", OnChoice);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnRotatePlayer(Dictionary<string, object> message)
    {
        myPlayer = (GameObject) message["player"];
        PhotonView myPhotonView = myPlayer.GetComponent<PhotonView>();
        bool rotate = (bool) message["rotate"];
        // Debug.Log($"{myPlayer.name} rota {rotate}");
        myPhotonView.RPC("RotatePlayer", RpcTarget.All, rotate);
    }

    void OnChoice(Dictionary<string, object> message)
    {
        GameManager.Choice((string) message["choice"], (string) message["player"]);
    }
}
