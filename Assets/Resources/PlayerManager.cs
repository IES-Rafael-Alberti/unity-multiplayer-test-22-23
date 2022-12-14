using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody2D;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] private PhotonView myPhotonView;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private float speed = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myPhotonView = GetComponent<PhotonView>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPhotonView.IsMine)
        {
            float horizontalVelocity = Input.GetAxis("Horizontal");
            myRigidbody2D.velocity = new Vector2(horizontalVelocity * speed, 0);
            
            // Movimiento horizontal 
            if (horizontalVelocity > 0.0f) myPhotonView.RPC("RotatePlayer", RpcTarget.All, false);
            else if(horizontalVelocity < 0.0f) myPhotonView.RPC("RotatePlayer", RpcTarget.All, true);
            
            // Animaciones
            GetComponent<Animator>().SetBool("running", Mathf.Abs(horizontalVelocity) > 0.1f);
        }
    }

    [PunRPC]
    void RotatePlayer(bool rotate)
    {
        if(mySpriteRenderer != null)
            mySpriteRenderer.flipX =  rotate;
    }
}
