using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento Horizontal")]
    public float moveSpeed;
    public Vector2 direction;

    [Header("Movimiento Vertical")]
    public float jumpSpeed = 5f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Dash")]
    public bool hasDashed = false;
    public float dash;
    public float dashStrength;
    private float dashTimer;

    [Header("Componentes")]
    public Rigidbody2D rb;
    public LayerMask groundLayer;

    [Header("Fisicas")]
    public float maxSpeed;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Colisiones")]
    public bool onGround = false;
    public float groundLenght = 0.6f;
    public Vector3 colliderOffset;

    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            dashTimer = Time.time + jumpDelay;
        }


        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLenght, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLenght, groundLayer);

        if (onGround)
        {
            hasDashed = false;
        }
    }
    private void FixedUpdate()
    {
        MoveCharacter(direction.x);
        ModifyPhysics();
        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }

        if (dashTimer > Time.time && onGround)
        {
            Dash(direction.x, direction.y);
        }
        else if (dashTimer > Time.time && !onGround)
        {
            if (hasDashed == false)
            {
                Dash(direction.x, direction.y);
            }
        }
    }
    void MoveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }
    void ModifyPhysics()
    {
        if (onGround)
        {
            bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0f;
    }

    void Dash(float x, float y)
    {
        if (hasDashed == false)
        {
            rb.velocity = Vector2.zero;
            rb.velocity += new Vector2(direction.x, direction.y) * dashStrength;
            hasDashed = true;
            dashTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            print("We've collected the thing");
        }
        Destroy(collision.gameObject);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLenght);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLenght);
    }
}