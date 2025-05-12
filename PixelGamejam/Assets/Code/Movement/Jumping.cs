using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public bool CheckForJumping(){

        if (Input.GetKey(KeyCode.Space) && !InJump){
            
            if(groundCheck.Grounded == true){
                Debug.Log("Grounded jump");
                Jump();
                return true;
            }

            if(groundCheck.notGroundedTimer < coyoteTimeAllowance){ // check for coyote
                Debug.Log("Coyote jump");
                Jump();
                return true;
            }


        }
        return false;
    }

    public bool CheckForJumpingNOINPUT(){

        if (!InJump){
            
            if(groundCheck.Grounded == true){
                Debug.Log("Grounded jump");
                Jump();
                return true;
            }

            if(groundCheck.notGroundedTimer < coyoteTimeAllowance){ // check for coyote
                Debug.Log("Coyote jump");
                Jump();
                return true;
            }


        }
        return false;
    }

    float resetJumpTimer;
    void ResetJumpCooldown(){
        if(groundCheck.Grounded){
            InJump = false;
        }

        if(InJump){
            resetJumpTimer += Time.deltaTime;

            if(resetJumpTimer > coyoteTimeAllowance){ // Makes sure the script knows it isnt the jump state
                resetJumpTimer = 0;
                InJump = false;
            }
        } 
        
    }
    void Jump(){
        ScriptableMovementState state = pMovement.movementStates[0];
        state.rb = GetComponent<Rigidbody2D>();
        state.JumpForce = JumpForce;

        state.Jump();

        InJump = true;
        
    }
}
