using UnityEngine;
using System.Collections.Generic;

public class EnemyWaypointMovement : MonoBehaviour
{
    [Header("Waypoints")]
public List<Transform> waypoints;

[Header("Movement Settings")]
public float moveSpeed = 3f;
public float waypointReachedDistance = 0.1f;
public bool loop = true;

[Header("Combat Settings")]
public float damage = 10f;
public float attackCooldown = 1f;
public float knockbackForce = 15f;

private Rigidbody2D rb;
private int currentWaypointIndex = 0;
private Vector2 movementDirection;
private float lastAttackTime;

// Método chamado quando há colisão com o personagem
void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        TryAttackPlayer(collision.gameObject);
    }
}

// Método chamado enquanto o inimigo está colidindo com o personagem
void OnCollisionStay2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        TryAttackPlayer(collision.gameObject);
    }
}

void TryAttackPlayer(GameObject player)
{
    // Verifica se pode atacar (cooldown)
    if (Time.time >= lastAttackTime + attackCooldown)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // Calcula direção do knockback (do inimigo para o player)
            Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

            playerHealth.TakeDamage(damage, knockbackDirection, knockbackForce);
            lastAttackTime = Time.time;
        }
    }
}

    }

