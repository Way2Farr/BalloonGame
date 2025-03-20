using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
public class Paddle : NetworkBehaviour
{
    private Camera mainCamera;
    private Vector2 mousePos;
    private NetworkVariable<float> networkRotation = new NetworkVariable<float>();

    // Update is called once per frame

    public override void OnNetworkSpawn() {
        if(!IsOwner) {
            gameObject.SetActive(false);
        }
    }
    void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void OnTrack(InputAction.CallbackContext context) {
        mousePos = context.ReadValue<Vector2>();
    }
    void Update()
    {
      MousePos();
    }


    void MousePos(){

        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.transform.position.y - transform.position.y));

        Vector3 direction = worldMousePos - transform.position;


        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0, rotZ);

        RotationUpdateServerRpc(rotZ);


    }


    [ServerRpc]
    private void RotationUpdateServerRpc(float r) {
    networkRotation.Value = r;
    }

} 

/*

using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
public class Paddle : NetworkBehaviour
{

// General Variables -----------------------------------------------

    private Camera mainCamera;
    private Vector2 mousePos;
    private NetworkVariable<float> networkRotation = new NetworkVariable<float>();

// Initializes Camera -----------------------------------------------

    void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

// Input Action Editor for mouse tracking -----------------------------------------------

    public void OnTrack(InputAction.CallbackContext context) {
        if (IsOwner) {
        mousePos = context.ReadValue<Vector2>();
        }
    }
    void Update()
    {
      MousePos();
    }

// Using ScreenToWorldPoint, transform the rotation of the paddle to point towards the mouse  -----------------------------------------------
    void MousePos(){

        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.transform.position.y - transform.position.y));

        Vector3 direction = worldMousePos - transform.position;


        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0, rotZ);

        RotationUpdateServerRpc(rotZ);

    }

// Netcode Methods -----------------------------------------------
    [ServerRpc]
    private void RotationUpdateServerRpc(float r) {
        networkRotation.Value = r;
    }


} 

*/
