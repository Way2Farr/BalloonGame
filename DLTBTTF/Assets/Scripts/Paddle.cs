using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{

    private Camera mainCamera;
    private Vector2 mousePos;

    // Update is called once per frame

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

        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y, mainCamera.nearClipPlane));

        Vector3 direction = worldMousePos - transform.position;


        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0, rotZ);

    }
}
