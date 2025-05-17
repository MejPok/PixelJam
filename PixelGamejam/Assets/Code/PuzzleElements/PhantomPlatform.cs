using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomPlatform : MonoBehaviour
{
    BoxCollider2D bc;

    public float dif;
    void Update()
    {
        bc = GetComponent<BoxCollider2D>();

        var pm = PlayerMovement.pm;

        if (bc.enabled)
        {
            if (pm.positionStates[0].position.y < transform.position.y) bc.enabled = false;
        }
        else if (!bc.enabled)
        {
            if (pm.positionStates[0].position.y > transform.position.y + dif) bc.enabled = true;
        }
    }
    
    
}
