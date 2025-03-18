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

    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip jumpClip;

    private bool isWalkPlaying = false;
    private bool isJumpPlaying = false;

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
                if (!isWalkPlaying && grounded) {
                SoundManager.instance.PlaySoundClip(walkClip, transform, 0.3f);
                isWalkPlaying = true;
                }
    
            }
            else {
                if(isWalkPlaying) {
              
                    isWalkPlaying = false;
                }
            }
        }
    
    // Jump
    void HandleJump(){
        // If player is grounded and pressed space, use linearVelocity speed upwards
        if(jumped && grounded) { 
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);  
            _Legs.clip = _jump;
            _Legs.Play();
            
            if (!isJumpPlaying) {
            SoundManager.instance.PlaySoundClip(jumpClip, transform, 0.8f);
            isJumpPlaying = true;
        }
        else {
                if(isJumpPlaying) {
                   
                    isJumpPlaying = false;
                }
            }
        }

    }

    // Check collison on the ground
    void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    
}

