using UnityEngine;

public class Paddle : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 mousePos;

    // Update is called once per frame

    void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
      MousePos();
    }


    void MousePos(){

        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0, rotZ);

    }
}
