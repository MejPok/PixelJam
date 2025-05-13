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

        ResetBufferSpace();

        
    }

    

    public bool CheckForJumping(){
        
        if(!InJump){
            if (Input.GetKey(KeyCode.Space)){
                
                if(groundCheck.Grounded == true){
                    Debug.Log("Grounded jump");
                    Jump();
                    return true;
                } 

                if(groundCheck.notGroundedTimer < coyoteTimeAllowance){ // check for coyote
                    Debug.Log("Coyote jump");
                    MakeSound();
                    Jump();

                    
                    return true;
                }

            }
        }
        
        
        BufferTime();

        return false;
    }

    float bufferTimer;
    bool spacePressed;
    public float BufferAllow;
    void BufferTime(){
        if(Input.GetKeyDown(KeyCode.Space) && groundCheck.Grounded == false){
            spacePressed = true;
            bufferTimer = 0;
        }
    }

    void ResetBufferSpace(){
        if(spacePressed){
            bufferTimer += Time.deltaTime;
            if(bufferTimer > BufferAllow){
                bufferTimer = 0;
                spacePressed = false;
            }
        }
    }

    public void UseBufferedJump(){
        if(spacePressed){
            Debug.Log("ss");
            Jump();
            spacePressed = false;
        }
        
    }

    public bool CheckForJumpingNOINPUT(){

        if (!InJump){
            
            if(groundCheck.Grounded == true){
                Debug.Log("Grounded jump");
                JumpNOINPUT();
                return true;
            }

            if(groundCheck.notGroundedTimer < coyoteTimeAllowance){ // check for coyote
                Debug.Log("Coyote jump");
                JumpNOINPUT();
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
        ScriptableMovementState state = pMovement.movementState;
        state.rb = GetComponent<Rigidbody2D>();
        state.JumpForce = JumpForce;

        state.audioClip = GetComponent<FXchoser>().audioClips[0];
        state.transform = transform;
        state.Jump();
        

        InJump = true;
        spacePressed = false;
        
    }

    void MakeSound(){
        ScriptableMovementState state = pMovement.movementState;
        state.rb = GetComponent<Rigidbody2D>();
        state.JumpForce = JumpForce;

        state.audioClip = GetComponent<FXchoser>().audioClips[0];
        state.transform = transform;
        state.PlayJumpSound();
    }
    void JumpNOINPUT(){
        ScriptableMovementState state = pMovement.movementState;
        state.rb = GetComponent<Rigidbody2D>();
        state.JumpForce = JumpForce;

        state.Jump();

        InJump = true;
        
    }

}
