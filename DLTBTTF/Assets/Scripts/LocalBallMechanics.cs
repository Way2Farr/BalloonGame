using Unity.VisualScripting;
using UnityEngine;


public class LocalBallMechanics : MonoBehaviour
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
