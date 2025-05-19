using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="OneLeg")]
public class OneLegState : ScriptableMovementState
{
    float mySpeed;
    public float rollFriction;
    public override void Move(float speed)
    {
        mySpeed = speed;
        jumper.CheckForJumpingNOINPUT(); // HAS TO BE NOINPUT OR ELSE IT WOULD WAIT FOR SPACE KEY PRESSED


        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0)
        {
            Vector2 force = new Vector2(horizontalInput * 1.5f, 0.01f);
            rb.AddForce(force);
            PlayMoveSound();
        }
    }

    public override void Jump()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput != 0){
            Vector2 force = new Vector2(horizontalInput * mySpeed, JumpForce * 5);
            if(Mathf.Abs(rb.velocity.x) < maxRollSpeed){
                rb.AddForce(force);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space)){
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            
        }
        
    }
    public override void Update(){
        // Pseudo rolling slowdown
        if (isGrounded && Mathf.Abs(rb.velocity.x) > 0.01f)
        {
            float side = 1f;

            if(rb.velocity.x > 0){
                side *= -1;
            }
            
            rb.velocity = new Vector2(rb.velocity.x + side * Mathf.Min(rollFriction, Mathf.Abs(rb.velocity.x)), rb.velocity.y);
        }
    }
}
