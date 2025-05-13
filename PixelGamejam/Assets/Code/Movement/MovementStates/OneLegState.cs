using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="OneLeg")]
public class OneLegState : ScriptableMovementState
{
    float mySpeed;
    public override void Move(float speed)
    {
        mySpeed = speed;
        jumper.CheckForJumpingNOINPUT(); // HAS TO BE NOINPUT OR ELSE IT WOULD WAIT FOR SPACE KEY PRESSED
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
}
