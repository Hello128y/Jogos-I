using UnityEngine;
using System.Collections; // <--- ESTE USING É ESSENCIAL PARA O KNOCKBACKROUTINE

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
    
    // VARIÁVEL DE ESTADO
    private bool isControlEnabled = true; 
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = visual.GetComponent<Animator>();
    }

    // O MÉTODO QUE O PLAYERHEATH CHAMA PARA PAUSAR O CONTROLE (PUBLIC!)
    public void DisableControlForDuration(float duration)
    {
        if (!isControlEnabled) 
        {
            // Se já estiver em knockback, reinicia o tempo
            StopAllCoroutines(); 
        }

        StartCoroutine(KnockbackRoutine(duration));
    }

    // A ROTINA QUE GERENCIA O TEMPO DE PAUSA
    private IEnumerator KnockbackRoutine(float duration)
    {
        isControlEnabled = false; 
        
        yield return new WaitForSeconds(duration);

        isControlEnabled = true; 
    }

    
    void Update()
    {
        // === PONTO DE CHECAGEM DO KNOCKBACK ===
        if (!isControlEnabled)
        {
            // Lógica visual para virar o sprite durante o empurrão
            if (rb2d.linearVelocity.x > 0.01f)
            {
                visual.localScale = new Vector3(4, 4, 4);
            }
            else if(rb2d.linearVelocity.x < -0.01f)
            {
                visual.localScale = new Vector3(-4, 4, 4);
            }
            return; // Impede que o código de movimento rode
        }

        // === LÓGICA DE MOVIMENTO NORMAL ===
        
        movex = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        anim.SetBool("run", Mathf.Abs(movex) > 0f && isGrounded);

        // Lógica visual
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
            visual.localScale = new Vector3(visual.localScale.x, 4, 4); 
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