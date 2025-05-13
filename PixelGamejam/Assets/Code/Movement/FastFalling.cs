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
        falling = !isGrounded();
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Jumping jumper = GetComponent<Jumping>();

        if(jumper.groundCheck.Grounded == false){
            
            timer += Time.deltaTime;

            rb.gravityScale = Mathf.Max(1, timer * multiplier);
        } else {
            
            timer = 0;

            rb.gravityScale = 1;
        }
        
    }

    public LayerMask groundLayer;
    private bool isGrounded()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return raycastHit.collider != null;
    }
}
