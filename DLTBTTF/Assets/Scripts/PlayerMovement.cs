using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public float acceleration;

    public float maxXSpeed;
    public float jumpSpeed;

    public bool grounded;

    public AnimationClip _walk, _jump;

    public Animation _Legs;

    float xInput;

    // Update is called once per frame
    void Update() {
        CheckInput();
        HandleJump();
    }

    void FixedUpdate() {
        CheckGround();
        HandleXInput();
    }
    void CheckInput() {
        xInput = Input.GetAxis("Horizontal");
    }

// Horizontal Movement
    void HandleXInput() {
       
        if(Mathf.Abs(xInput) > 0) {
         
            body.linearVelocity = new Vector2(xInput * acceleration, body.linearVelocity.y);
                _Legs.clip = _walk;
                _Legs.Play();

    
         }
        }
    
    // Jump
    void HandleJump(){
        // If player is grounded and pressed space, use linearVelocity speed upwards
        if(Input.GetButtonDown("Jump") && grounded) { 
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);  
            _Legs.clip = _jump;
            _Legs.Play();
        }
    }

    // Check collison on the ground
    void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    
}

