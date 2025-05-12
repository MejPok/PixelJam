using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] bool grounded;
    public float notGroundedTimer;
    public Jumping jumper;
    public bool Grounded { 
        get { 
            return grounded; 
        } 

        set{
            if(value != grounded) { //Check for change
                grounded = value;
                
                if(value == false){ // that means player has just left a platform, check for coyote time
                    notGroundedTimer = 0;
                } else {
                    jumper.UseBufferedJump();
                }
            }
            
        }

        }

    void Update()
    {
        if(!grounded){
            notGroundedTimer += Time.deltaTime;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Floor")){
            Grounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Floor")){
            Grounded = false;
        }
    }
}
