using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowingBones : MonoBehaviour
{
    public static ThrowingBones tb {get; set;}

    public BoneChoser bc;

    public GameObject bonePrefab;

    public void Start(){
        tb = this;
    }

    public void PutDownTheBone(){

    }

    public void ThrowTheBone(){
        Vector2 shotPosition = new Vector2(0, 0);
        
        float angle = Mathf.Atan2(shotPosition.y, shotPosition.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bonePrefab, transform.position, Quaternion.identity);

        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        bullet.GetComponent<Rigidbody2D>().velocity = shotPosition * 20;
    }
}
