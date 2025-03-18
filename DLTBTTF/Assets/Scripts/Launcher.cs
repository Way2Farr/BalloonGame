using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private Slider powerSlider;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float decay = 5f;

    [SerializeField] private AudioClip paddleClip;
    [SerializeField] private AudioClip chargeClip;

    private bool isChargePlaying;

    private bool isPaddlePlaying;

    public static Launcher stats;

    private float power;


    private bool mouseInput;

     private void Awake()
    {
     stats = this;
    }

    private void Start()
    {
        powerSlider.minValue = 0f;
        powerSlider.maxValue = maxPower;
        powerSlider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
    
    
        if (powerSlider == null)
    {
        Debug.LogError("Slider not found or Slider component missing!");
    }
    }

    public void OnClick(InputAction.CallbackContext context){
        if (context.performed) {
            mouseInput = true;
        }
        else if (context.canceled) {
            mouseInput = false;
        }
    }

    void Update()
    {
        Charging();
    }

    private void Charging()
    {
        powerSlider.gameObject.SetActive(true);
        powerSlider.value = power;

        if (mouseInput) {
            power = Mathf.Clamp(power + 50 * Time.deltaTime, 0, maxPower);
                if (!isChargePlaying) {
                    SoundManager.instance.PlaySoundClip(chargeClip, transform, 0.1f);
                    isChargePlaying = true;
                }
                else if (isChargePlaying) {
                        isChargePlaying = false;
                }
                
        else {
            // Decay power over time when not charging
            power = Mathf.Max(power - decay * Time.deltaTime, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Apply force to the ball based on the paddle's power
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                ballRb.linearVelocity = Vector2.zero; // Reset ball velocity
                ballRb.AddForce(direction * power * speed, ForceMode2D.Impulse);
                    if (!isPaddlePlaying){
                    SoundManager.instance.PlaySoundClip(paddleClip, transform, 0.5f);
                    isPaddlePlaying = true;
                    }
                else {
                    if (isPaddlePlaying) {
                        isPaddlePlaying = false;
                    }
                }
            }
        }
    }


    public void IncreaseStats() {
        maxPower += 0.5f;
        speed += 0.1f;
        powerSlider.maxValue = maxPower;
        Debug.Log("Stats Increasing" + maxPower);

    }
}