using UnityEngine;
using System.Collections; // Necessário para referenciar Coroutines

public class PlayerHeath : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar;
    private Rigidbody2D rb;
    
    // NOVO: Referência ao script de movimento
    private PlayerMovement playerMovement; 

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        
        // NOVO: Obter a referência ao script PlayerMovement
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component not found on the player GameObject!");
        }
    }

    public void TakeDamage(float damage, Vector2 knockbackDirection, float knockbackForce = 10f)
    {
        currentHealth -= damage;
        Debug.Log("Player levou dano! Vida atual: " + currentHealth);

        // Certifique-se de que o Rigidbody esteja presente e não seja nulo
        if (rb != null)
        {
            // 1. Aplica o knockback (a força)
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
        
        // 2. Chama a função de desativação de controle
        if (playerMovement != null)
        {
            // Define a duração do empurrão
            float knockbackDuration = 0.3f; 
            playerMovement.DisableControlForDuration(knockbackDuration);
        }

        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player morreu!");
        gameObject.SetActive(false);

    }
   public void RecuperarVida(float boa)
    {
        currentHealth += boa;
        Debug.Log("Player levou dano! Vida atual: " + currentHealth);
         healthBar.UpdateHealthBar(currentHealth, maxHealth);

    }
}
