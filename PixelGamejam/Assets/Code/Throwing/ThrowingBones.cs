using System;
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

    public void PutDownTheBone(string NAME, Sprite sprite)
    {
        GameObject bullet = Instantiate(bonePrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<BoneData>().CreateBone(NAME, sprite);
        LevelManager.instance.AddBoneToRoom(bullet);
    }

    public void ThrowTheBone(string NAME, Sprite sprite)
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bonePrefab, transform.position, Quaternion.identity);
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        bullet.GetComponent<Rigidbody2D>().velocity = direction * 15f;

        bullet.GetComponent<BoneData>().CreateBone(NAME, sprite);

        LevelManager.instance.AddBoneToRoom(bullet);
    }
}
