using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="FullState")]
public class FullBodyScriptable : ScriptableMovementState
{
    public override void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, JumpForce * JumpMultiplier);
    }

    public override void Move(float speed)
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //Important!
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        if (Mathf.Abs(horizontalInput) > 0)
        {
            PlayMoveSound();
        }
        
    }
}
