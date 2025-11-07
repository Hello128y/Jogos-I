using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float movex;
    private bool isGrounded;
    public Transform visual;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = visual.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        movex = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        anim.SetBool("run", Mathf.Abs(movex) > 0f && isGrounded);
        if (rb2d.linearVelocity.x > 0.01f)
        {
            visual.localScale = new Vector3(4, 4, 4);
        }
        else if(rb2d.linearVelocity.x < -0.01f)
        {
            visual.localScale = new Vector3(-4, 4, 4);
        }
        else
        {
            visual.localScale = new Vector3(-0.9f, 0.9f, 0.9f);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        Move();
    }
    void Move()
    {
        rb2d.linearVelocity = new Vector2(movex * moveSpeed, rb2d.linearVelocity.y);
    }
    void Jump()
    {
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
    }
}
