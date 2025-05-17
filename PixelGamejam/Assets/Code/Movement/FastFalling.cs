using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FastFalling : MonoBehaviour
{
    public float multiplier;
    public float timer;
    public bool falling;
    void Update()
    {
        falling = !IsGrounded();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Jumping jumper = GetComponent<Jumping>();

        if (!falling || !jumper.groundCheck.Grounded){
            
            timer = 0;

            rb.gravityScale = 1;
        } else{
            timer += Time.deltaTime;

            rb.gravityScale = Mathf.Max(1, timer * multiplier);

            timer = 0;

            rb.gravityScale = 1;
        }
        
    }

    public LayerMask groundLayer;
    private bool IsGrounded()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        float rayLength = 0.1f;

        // Cast straight down from the center of the bottom of the collider
        Vector2 origin = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);

        // Debug: draw the ray
        Debug.DrawRay(origin, Vector2.down * rayLength, hit.collider != null ? Color.green : Color.red);

        return hit.collider != null;
    }
}
