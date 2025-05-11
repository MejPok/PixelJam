using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] bool grounded;
    public bool Grounded { 
        get { 
            return grounded; 
        } 

        set{
            if(value != grounded) { //Check for change
                grounded = value;
            }
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
