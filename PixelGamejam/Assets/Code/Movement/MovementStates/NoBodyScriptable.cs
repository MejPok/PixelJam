using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NoBody")]
public class NoBodyScriptable : ScriptableMovementState
{
    public float rollFriction;
    public override void Jump(){
        return;
    }

    public override void Move(float speed){
        float horizontalInput = Input.GetAxis("Horizontal"); //Important!! add new input for controller inputs
        Vector2 force = new Vector2(horizontalInput * speed, 0);
        if(Math.Abs(rb.velocity.x) < maxRollSpeed){
            rb.AddForce(force);
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
