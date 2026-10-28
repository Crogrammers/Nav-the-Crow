using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // cache variable for rigid body
    // prevent it to get every frame
    private Rigidbody2D rb;

    public float movementSpeed;
    public float sprintSpeed;
    public float jumpSpeed;
    private bool isGrounded;

    public float inGroundDumping;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            rb.linearVelocity = new Vector2(-movementSpeed, rb.linearVelocity.y);
        else if (Input.GetKey(KeyCode.D))
            rb.linearVelocity = new Vector2(movementSpeed, rb.linearVelocity.y);

        // Jumping
        if (isGrounded &&
                 (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)))
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x, jumpSpeed);
                isGrounded = false;
        }

        if (isGrounded)
            rb.linearVelocity = new Vector2(
               rb.linearVelocity.x * inGroundDumping,
               rb.linearVelocity.y);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
            isGrounded = true;
        else if (other.gameObject.CompareTag("Build Material"))
            isGrounded = true;
    }
}
