using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : MonoBehaviour {
    public float _Speed;
    private float _currDist, _dist;

    [SerializeField] private AudioClip hitClip;

    void Update()
    {
        if(Mathf.Round(_currDist) == Mathf.Round(_dist) || _currDist == 0)
        {
            _dist = Random.Range(0, 2.7f);
        }
        _currDist = Mathf.Lerp(_currDist, _dist, Time.deltaTime*0.3f);
        transform.Rotate(0, 0, _Speed * Time.deltaTime + _currDist);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
        SoundManager.instance.PlaySoundClip(hitClip, transform, 1f);
        }
        
    }


}
