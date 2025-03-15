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
        // Left or Right Keys
        if(Mathf.Abs(xInput) > 0) {
            // Acceleration value, Update speed and y linearVelocity)
            body.linearVelocity = new Vector2(xInput * acceleration, body.linearVelocity.y);
         }
        }
    
    // Jump
    void HandleJump(){
        // If player is grounded and pressed space, use linearVelocity speed upwards
        if(Input.GetButtonDown("Jump") && grounded) { 
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);  
        }
    }

    // Check collison on the ground
    void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }


    
}

