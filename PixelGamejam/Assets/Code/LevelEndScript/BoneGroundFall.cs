using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneGroundFall : MonoBehaviour
{
    float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            Destroy(gameObject);
        }
    }
}
