using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;


public class BallMechanics : MonoBehaviour
{

// General Variables -----------------------------------------------

    public Rigidbody2D body;
    public CircleCollider2D col;
    public LayerMask lava;
    public bool touchedLava;
    public float yDepth;
    

    // Audio -----------------------------------------------


    [SerializeField] private AudioClip destroyClip;

    // Update is called once per frame
    void Update()
    {
        
    TouchLava();
    YPosition();

    }


// Adds a point when collided with the player's paddle -----------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.CompareTag("Player")){
            Pointcounter.instance.AddPoint();
        }
    }


// Destroys itself if touched with lava -----------------------------------------------

    void TouchLava(){

    touchedLava = Physics2D.OverlapCircleAll(col.transform.position, 1.7f, lava).Length > 0;

   if(touchedLava == true) {
        Destroy(gameObject);
        SoundManager.instance.PlaySoundClip(destroyClip, transform, 0.8f);
    }

    }


// Determines Y Position, sends to Pointcounter to update UI -----------------------------------------------

    void YPosition() {
    yDepth = body.position.y;

    Pointcounter.instance.DepthPoints(yDepth);
    }
    
}

/*
using System.Drawing;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;


public class BallMechanics : NetworkBehaviour
{

// General Variables -----------------------------------------------

    public Rigidbody2D body;
    public CircleCollider2D col;
    public LayerMask lava;
    public bool touchedLava;
    public float yDepth;

    private NetworkVariable<float> networkY = new NetworkVariable<float>();

    // Audio -----------------------------------------------


    [SerializeField] private AudioClip destroyClip;

    // Update is called once per frame
    void Update()
    {
        if(IsServer) {
        
    TouchLava();
    YPosition();
        }

    yDepth = networkY.Value;
    Pointcounter.instance.DepthPoints(yDepth);
    }


// Adds a point when collided with the player's paddle -----------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.CompareTag("Player")){
            AddPointServerRpc();
        }
    }


// Destroys itself if touched with lava -----------------------------------------------

    void TouchLava(){

    touchedLava = Physics2D.OverlapCircleAll(col.transform.position, 1.7f, lava).Length > 0;

   if(touchedLava == true) {
        DestroyBallClientRpc();
        PlayDestroyClientRpc();
    }

    }


// Determines Y Position, sends to Pointcounter to update UI -----------------------------------------------

    void YPosition() {
    yDepth = body.position.y;

   networkY.Value = yDepth;
    }

// Netcode methods -----------------------------------------------
    
    [ServerRpc]
    private void AddPointServerRpc() {
    Pointcounter.instance.AddPoint();
    }

    [ClientRpc]
    private void DestroyBallClientRpc() {
        Destroy(gameObject);
    }

    [ClientRpc]
    private void PlayDestroyClientRpc() {
        SoundManager.instance.PlaySoundClip(destroyClip, transform, 0.8f);
    }
}

*/