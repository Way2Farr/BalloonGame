using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public float damping = 1.5f;
    public Transform _target;

    public Transform _player;

    public float threshold = 5f;

    private bool faceLeft;
    private int lastX;
    private float dynamicSpeed;

    public float minX;
    public float maxX;

    void Start(){
        FindPlayer();
        GameObject ball = GameObject.Find("Ball");

        _target = ball.transform;
    }

    public void FindPlayer()
    {
        lastX = Mathf.RoundToInt(_target.position.x);
        transform.position = new Vector3(Mathf.Clamp(_target.position.x,minX,maxX), _target.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        if (_target && _player)
        {
            int currentX = Mathf.RoundToInt(_target.position.x);
            if (currentX > lastX) faceLeft = false; else if (currentX < lastX) faceLeft = true;
            lastX = Mathf.RoundToInt(_target.position.x);

            Vector3 target;
            if (faceLeft)
            {
                target = new Vector3(Mathf.Clamp(_target.position.x ,minX,maxX), YThreshold(), transform.position.z);
            }
            else
            {
                target = new Vector3(Mathf.Clamp(_target.position.x ,minX,maxX), YThreshold(), transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }


    private float YThreshold() {
        if(_target.position.y > _player.position.y + threshold) {
            return _target.position.y  + dynamicSpeed;
        }
        else {
            return _player.position.y + dynamicSpeed;
        }
    }
}
