// Network Transform tests
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
//General Variables -----------------------------------------------
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public float acceleration;

    public float maxXSpeed;
    public float jumpSpeed;

    public bool grounded;

// Animation -----------------------------------------------
    public AnimationClip _walk, _jump;

    public Animation _Legs;

    float xInput;

// Input Action Editor -----------------------------------------------

    private Vector2 movementInput = Vector2.zero;

    private bool jumped = false;

    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip jumpClip;

    private bool isWalkPlaying = false;
    private bool isJumpPlaying = false;
    private bool wasMoving = false;


    // OnMove/Jump checks play inputs for movement and jumping

    public override void OnNetworkSpawn() {
        if(!IsOwner) {
            enabled = false;
            return;
        }
    }

    public void OnMove(InputAction.CallbackContext context){
        if(IsOwner) {
            movementInput = context.ReadValue<Vector2>();
            xInput = movementInput.x;
        }

    }

    public void OnJump(InputAction.CallbackContext context){
        if(IsOwner) {
        jumped = context.performed;
        }
    }



    void Update() {
        if(IsOwner) {
            HandleJump();
        }
    }

    void FixedUpdate() {

        if(IsOwner) {
            CheckGround();
            HandleXInput();
        }
    }

// Horizontal Movement -----------------------------------------------
    void HandleXInput() {
       
        bool isMoving = Mathf.Abs(xInput) > 0;
        if(isMoving) {   
            body.linearVelocity = new Vector2(xInput * acceleration, body.linearVelocity.y);

            _Legs.clip = _walk;
            _Legs.Play();

            // Sound Effect Portion
            if (!wasMoving && grounded) {
                WalkSoundClientRpc();
                isWalkPlaying = true;
            }
        }
        else {
                if(isWalkPlaying) {
                    isWalkPlaying = false;
                }
            }
        wasMoving = isMoving;
        RequestMoveServerRpc(xInput, body.linearVelocity.y);
        
        
    }
    
    // Jumping -----------------------------------------------
    void HandleJump(){
        
        if(jumped && grounded) { 
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);  
            _Legs.clip = _jump;
            _Legs.Play();
            
            // Sound Effect Portion
            if (!isJumpPlaying) {
                JumpSoundClientRpc();
                isJumpPlaying = true;
        }
            jumped = false;
        }
        else {
            if(!grounded) {
                isJumpPlaying = false;
            }
        }
    }
    // Check if grounded
    void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

// NetCode Methods -----------------------------------------------
    [ServerRpc]
    private void RequestMoveServerRpc(float xInput, float speed)  {

        body.linearVelocity = new Vector2(xInput * acceleration, speed);
        SynchronizeClientRpc(xInput, speed);

    }

    [ClientRpc] 
    private void SynchronizeClientRpc (float xInput, float speed) {
        if(!IsOwner) {
            body.linearVelocity = new Vector2(xInput * acceleration, speed);
        }
    }

    [ClientRpc]
    private void WalkSoundClientRpc() {
        SoundManager.instance.PlaySoundClip(walkClip, transform, 0.1f);
    }

    [ClientRpc]
    private void JumpSoundClientRpc() {
        SoundManager.instance.PlaySoundClip(jumpClip, transform, 0.3f);
    }


}


