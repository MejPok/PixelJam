using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Jumping))] 
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    Rigidbody2D rb;

    Vector2 baseScale;

    private void Awake()
    {
        baseScale = transform.localScale;
        //pulls player box collider, essentially the foundation of the character
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //this creates the variable horizontalInput which checks if the player is pressing a button to walk
        float horizontalInput = Input.GetAxis("Horizontal"); //Important!! add new input for controller inputs
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y); // this is a formula stating that the players movement is a Vector equal to the horizontalInput multiplied by speed for the horizontal axis and the rb.velocity.y for the vertical movement.


        //flips player sprite when moving
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector2(baseScale.x, baseScale.y);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-baseScale.x, baseScale.y);

        
            
    }

}
