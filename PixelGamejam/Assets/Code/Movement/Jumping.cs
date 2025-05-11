using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public bool InJump;
    public float JumpForce;
    PlayerMovement pMovement;
    Rigidbody2D rb;

    GroundCheck groundCheck;

    void Start(){
        pMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();

        InJump = false;
    }

    void Update(){
            if (Input.GetKey(KeyCode.Space) && groundCheck.Grounded){
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            }
                

    }
}
