using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("rotatePlayer", OnRotatePlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnRotatePlayer(Dictionary<string, object> message)
    {
        SpriteRenderer mySprite = (SpriteRenderer) message["renderer"];
        bool rotate = (bool) message["rotate"];
        PhotonView myPhotonView = (PhotonView)message["view"];
        myPhotonView.RPC("RotatePlayer", RpcTarget.All, mySprite, rotate);
    }

    [PunRPC]
    void RotatePlayer(SpriteRenderer sprite, bool rotate)
    {
        if(sprite != null)
            sprite.flipX =  rotate;
    }
}
