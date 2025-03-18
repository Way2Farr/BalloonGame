using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static SoundManager instance;

    [SerializeField] private AudioSource soundObject;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform, float volume) {

        //spawn
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
        //assign audio

        audioSource.clip = audioClip;
        //assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        // get length
        float clipLength = audioSource.clip.length;
        //destroy clip

        Destroy(audioSource.gameObject, clipLength);
    }


}
