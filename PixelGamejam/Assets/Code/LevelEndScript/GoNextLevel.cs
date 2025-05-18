using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


public class GoNextLevel : MonoBehaviour
{
    public void LoadEndScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    float timer;
    public float howTimer;
    public GameObject prefab;
    public Sprite[] boneTypes;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= howTimer)
        {
            timer = 0;
            StartSpawningBones();
        }
        
    }

    public void StartSpawningBones()
    {
        Vector2 pos = new Vector2(Random.Range(transform.position.x, transform.position.x + 17), transform.position.y);

        var bone = Instantiate(prefab, pos, Quaternion.identity);
        bone.transform.localScale = new Vector2(4, 4);
        bone.GetComponent<SpriteRenderer>().sprite = boneTypes[Random.Range(0, boneTypes.Length - 1)];
    }
}
