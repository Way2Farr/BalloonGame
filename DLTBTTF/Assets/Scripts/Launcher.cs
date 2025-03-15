using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    float power = 0;
    public float maxPower = 10f;
    public float minPower = 0.1f;
    public Slider pSlide;
    public Rigidbody2D rb;
    private float bounce;
    public float decay = 5;
    public float speed = 5;
    public PhysicsMaterial2D bounceMaterial;
    private PhysicsMaterial2D defaultMaterial;
    private Collider2D paddleCollider;

    void Start()
    {
        pSlide.minValue = 0f;
        pSlide.maxValue = maxPower;

        rb = GetComponent<Rigidbody2D>();
        paddleCollider = GetComponent<Collider2D>();

        // Store the default material and create a new instance of the bounce material
        defaultMaterial = paddleCollider.sharedMaterial;
        bounceMaterial = new PhysicsMaterial2D();
        bounceMaterial.bounciness = 1; // Default bounciness
        paddleCollider.sharedMaterial = bounceMaterial;
    }

    void Update()
    {
        Charging();
    }

    public void Charging()
    {
        pSlide.gameObject.SetActive(true);
        pSlide.value = power;

        if (Input.GetMouseButton(0))
        {
            if (power <= maxPower)
            {
                power += 50 * Time.deltaTime;
                power = Mathf.Min(power, maxPower);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Apply bounciness based on power
            bounce = Mathf.Min(2, 1 + power * 0.1f); // Cap bounciness at 2
            bounceMaterial.bounciness = bounce;

            Debug.Log("Bounciness: " + bounceMaterial.bounciness);

            // Launch the paddle upward
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * power * speed, ForceMode2D.Impulse);

            // Reset power after launch
            power = 0;
        }
        else
        {
            // Decay power over time
            if (power > 0)
            {
                power -= decay * Time.deltaTime;
                power = Mathf.Max(power, 0);
            }

            // Reset bounciness to default when not charging
            bounceMaterial.bounciness = 1;
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
                // Calculate the direction to launch the ball
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                ballRb.linearVelocity = Vector2.zero; // Reset ball velocity
                ballRb.AddForce(direction * power * speed, ForceMode2D.Impulse);
            }
        }
    }
}