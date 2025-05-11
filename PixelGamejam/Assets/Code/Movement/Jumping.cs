using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public bool InJump;
    public float JumpForce;

    public float coyoteTimeAllowance;
    PlayerMovement pMovement;
    Rigidbody2D rb;

    public GroundCheck groundCheck;

    void Start(){
        pMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        InJump = false;
    }

    void Update(){
        CheckForJumping();
        ResetJumpCooldown();
                

    }

    void CheckForJumping(){

        if (Input.GetKey(KeyCode.Space) && !InJump){
            
            if(groundCheck.Grounded == true){
                Debug.Log("Grounded jump");
                Jump();
                return;
            }

            if(groundCheck.notGroundedTimer <= coyoteTimeAllowance){ // check for coyote
                Debug.Log("Coyote jump");
                Jump();
                return;
            }


        }
    }

    float resetJumpTimer;
    void ResetJumpCooldown(){
        if(InJump){
            resetJumpTimer += Time.deltaTime;

            if(resetJumpTimer >= coyoteTimeAllowance){
                resetJumpTimer = 0;
                InJump = false;
            }
        } 
        
    }
    void Jump(){
        InJump = true;
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
    }
}
