using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private Slider powerSlider;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float decay = 5f;
    [SerializeField] private PhysicsMaterial2D bounceMaterial;

    public static Launcher stats;

    private float power;
    private Collider2D paddleCollider;

     private void Awake()
    {
     stats = this;
    }

    void Start()
    {
        powerSlider.minValue = 0f;
        powerSlider.maxValue = maxPower;

        paddleCollider = GetComponent<Collider2D>();

        // Set the bounce material
        paddleCollider.sharedMaterial = bounceMaterial;
    }

    void Update()
    {
        Charging();
    }

    private void Charging()
    {
        powerSlider.gameObject.SetActive(true);
        powerSlider.value = power;

        if (Input.GetMouseButton(0))
        {
            // Increase power while holding the mouse button
            power = Mathf.Clamp(power + 50 * Time.deltaTime, 0, maxPower);
        }
        else
        {
            // Decay power over time when not charging
            power = Mathf.Max(power - decay * Time.deltaTime, 0);
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
            }
        }
    }


    public void IncreaseStats() {
        maxPower += 0.5f;
        speed += 0.1f;

        Debug.Log("Stats Increasing" + maxPower);

    }
}