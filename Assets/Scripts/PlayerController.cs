using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // cache variable for rigid body
    // prevent it to get every frame
    private Rigidbody2D rb;

    public float movementSpeed;
    public float sprintSpeed;
    public float jumpSpeed;
    [SerializeField] private BoolSO buildMode, isPaused;
    private bool isGrounded;
    private bool firstJump = true;
    [SerializeField] private float floatVelocity=2;

    public Collectible cm;
    public Vector3 startPosition;

    private SpriteRenderer spriteRenderer;

    public float inGroundDumping;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            resetCharacter();
        }
        if (!buildMode.boolean && !isPaused.boolean)
        {
            Move();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void resetCharacter()
    {
        var controller = FindAnyObjectByType<LevelUIController>();
        controller.Kill();
        transform.position = startPosition;
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
             rb.linearVelocity = new Vector2(-movementSpeed, rb.linearVelocity.y);
        else if (Input.GetKey(KeyCode.D))
             rb.linearVelocity = new Vector2(movementSpeed, rb.linearVelocity.y);

        // Jumping
        if ((isGrounded) &&
                 (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)))
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x, jumpSpeed);
            //isGrounded = false;
            /*if (isGrounded)
            {
                isGrounded = false;
                firstJump = true;
            }
            else if (firstJump && !isGrounded)
            {
                firstJump = false;
            }*/
        }
        else if (firstJump &&
                 (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x, jumpSpeed);
            //isGrounded = false;
            /*if (isGrounded)
            {
                isGrounded = false;
                firstJump = true;
            }
            else if (firstJump && !isGrounded)
            {*/
                firstJump = false;
            //}
        }

        if (isGrounded)
            rb.linearVelocity = new Vector2(
               rb.linearVelocity.x * inGroundDumping,
               rb.linearVelocity.y);

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && !isGrounded)
            rb.linearVelocity = new Vector2(
              rb.linearVelocity.x,
              rb.linearVelocity.y + floatVelocity); // todoZ count energy given to the bird

            float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput < 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            firstJump = false;
        }
        else if (other.gameObject.CompareTag("Build Material"))
        {
            isGrounded = true;
            firstJump = false;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
            firstJump = true;
        }
        else if (other.gameObject.CompareTag("Build Material"))
        {
            isGrounded = false;
            firstJump = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            cm.coinCount += 5;
        }
    }
}
