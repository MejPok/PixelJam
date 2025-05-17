using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="room")]
public class ScriptableRoom : ScriptableObject
{
    public string[] bonesRequiredRemoved;

    public Transform positionWhereToSpawnPlayer;

    public GameObject activeMap;
    public GameObject map;

    public void StartGame(GameObject player)
    {
        activeMap = Instantiate(map, new Vector2(0, 0), Quaternion.identity);

        positionWhereToSpawnPlayer = activeMap.transform.GetChild(0).GetComponent<Transform>();
        player.transform.position = positionWhereToSpawnPlayer.position;
    }

}
