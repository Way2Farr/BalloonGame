using Unity.VisualScripting;
using UnityEngine;

public class BallMechanics : MonoBehaviour
{
    public Rigidbody2D body;

    public CircleCollider2D col;

    public LayerMask ground;

    public bool touchedGround;

    // Update is called once per frame
    void Update()
    {
        
    TouchGround();
    }


    void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.CompareTag("Player")){
            Pointcounter.instance.AddPoint();
        }
    }

    void TouchGround(){

    touchedGround = Physics2D.OverlapCircleAll(col.transform.position, 1.7f, ground).Length > 0;

    if(touchedGround == true) {
        Destroy(gameObject);
    }

    }
    
}
