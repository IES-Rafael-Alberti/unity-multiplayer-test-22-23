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
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private bool rotate = false;
    
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
            myRigidbody2D.velocity = new Vector2(horizontalVelocity * speed, myRigidbody2D.velocity.y);
            
            // Movimiento horizontal 
            if (horizontalVelocity > 0.0f && rotate) EventManager.RotateEvent(gameObject, false);
            else if (horizontalVelocity < 0.0f && !rotate) EventManager.RotateEvent(gameObject, true);

            // Salto
            if (Input.GetButtonDown("Jump") && myRigidbody2D.velocity.y == 0.0f) myRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            // Animaciones
            GetComponent<Animator>().SetBool("running", Mathf.Abs(horizontalVelocity) > 0.1f);
        }
    }
    
    [PunRPC]
    void RotatePlayer(bool rotation)
    {
        rotate = rotation;
        if(mySpriteRenderer != null)
            mySpriteRenderer.flipX =  rotation;
    }
}
