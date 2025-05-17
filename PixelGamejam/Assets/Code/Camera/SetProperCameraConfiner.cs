using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SetProperCameraConfiner : MonoBehaviour
{
    void Update()
    {
        if (LevelManager.instance.activeMap != null)
        {
            GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = LevelManager.instance.activeMap.transform.GetChild(1).GetComponent<PolygonCollider2D>();
        }
    }
}
