using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movimiento")]
    public float movementSpeed;
    public float maxSpeed;
    public float jumpForce;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public float jumpHigh;
    public float jumpLow;




    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var movement = Input.GetAxisRaw("Horizontal");
         if(movement != 0)
        {
            rb.AddForce(new Vector2(movementSpeed, 0) * movement * Time.deltaTime, ForceMode2D.Impulse);
        }
        else
        {
            rb.velocity = Vector2.zero * Time.deltaTime;
        }




        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);    
    }
    void Update()
    {
        //Check if on ground.
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        //Basic jump with checks, resets timer.
        if(isGrounded == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.AddForce(new Vector2 (0, jumpForce));
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.AddForce(new Vector2(0, jumpForce));
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            
        }

        if(rb.velocity.y < 0)
        {
            rb.AddForce( new Vector2 (0, Physics2D.gravity.y * (jumpHigh - 1) * Time.deltaTime));
        }else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.AddForce(new Vector2(0, Physics2D.gravity.y * (jumpLow - 1) * Time.deltaTime));
        }
    }
}
