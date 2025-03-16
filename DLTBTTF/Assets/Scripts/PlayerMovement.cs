using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
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

    private Vector2 movementInput = Vector2.zero;

    private bool jumped = false;

    private void Start()
    {
     controller = gameObject.GetComponent<CharacterController>();   
    }


    public void OnMove(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context){
        jumped = context.action.triggered;
    }

    void Update() {
        CheckInput();
        HandleJump();
    }

    void FixedUpdate() {
        CheckGround();
        HandleXInput();
    }
    void CheckInput() {
        xInput = movementInput.x;
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
        if(jumped && grounded) { 
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

