using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LocalLauncher : MonoBehaviour
{

    // Configurable Varaibles -----------------------------------------------
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private Slider powerSlider;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float decay = 5f;

    // Audio Variables -----------------------------------------------
    [SerializeField] private AudioClip paddleClip;
    [SerializeField] private AudioClip chargeClip;
    [SerializeField] private AudioClip depletedClip;
    private bool isChargePlaying;
    private bool isPaddlePlaying;

    private bool isDepletedPlaying;

    // Stat Changer Variables -----------------------------------------------
    public static Launcher stats;

    private float power;

    private bool mouseInput;


     private void Awake()
    {
     //stats = this;
    }

    private void Start()
    {
        // Create Slider -----------------------------------------------
        GameObject ui = GameObject.Find("Canvas");
        powerSlider = ui.transform.Find("Slider").GetComponent<Slider>();
        powerSlider.minValue = 0f;
        powerSlider.maxValue = maxPower;
    }

    // Player Input Editor actions -----------------------------------------------
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

    // Charge slider based on power value -----------------------------------------------
    private void Charging()
    {
        powerSlider.gameObject.SetActive(true);
        powerSlider.value = power;  

        if (mouseInput) {
            power = Mathf.Clamp(power + 20 * Time.deltaTime, 0, maxPower);

            // Sound Effect Portion (Sound when fully Charge)-----------------------------------------------
                if (!isChargePlaying && power >= powerSlider.maxValue) {
                    SoundManager.instance.PlaySoundClip(chargeClip, transform, 0.1f);
                    isChargePlaying = true;
                    isDepletedPlaying = true;
                }
                else if (isChargePlaying && power < maxPower) {
                        isChargePlaying = false;
                }
                
        }
        else {
            // Decay power, play sound when fully depleted -----------------------------------------------
                power = Mathf.Max(power - decay * Time.deltaTime, 0);
                    if (power <= 0.1f && isDepletedPlaying ) {
                    SoundManager.instance.PlaySoundClip(depletedClip, transform, 0.1f);
                    isDepletedPlaying = false;
            }
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Check if paddle hit the ball -----------------------------------------------
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                // Based on the charged power, increase force against the ball -----------------------------------------------
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                ballRb.linearVelocity = Vector2.zero; // Reset ball velocity
                ballRb.AddForce(direction * power * speed, ForceMode2D.Impulse);

                // Sound Effect Portion (Hit)
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

    // For each point gained increase player stats
    public void IncreaseStats() {
        maxPower += 0.5f;
        speed += 0.1f;
        powerSlider.maxValue = maxPower;
        Debug.Log("Stats Increasing" + maxPower);

    }
}
