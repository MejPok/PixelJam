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
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(rb.velocity.x) < maxRollSpeed || Mathf.Sign(horizontalInput) != Mathf.Sign(rb.velocity.x))
        {
            Vector2 force = new Vector2(horizontalInput * speed, 0);
            rb.AddForce(force);
            PlayMoveSound();
        }
        
    }

    public override void Update(){
        // Pseudo rolling slowdown
        float horizontalInput = Input.GetAxis("Horizontal");

        if (isGrounded && Mathf.Abs(horizontalInput) < 0.01f && Mathf.Abs(rb.velocity.x) > 0.01f)
        {
            float frictionForce = Mathf.Min(rollFriction, Mathf.Abs(rb.velocity.x));
            float newXVelocity = rb.velocity.x - Mathf.Sign(rb.velocity.x) * frictionForce;
            rb.velocity = new Vector2(newXVelocity, rb.velocity.y);
        }
    }
}
