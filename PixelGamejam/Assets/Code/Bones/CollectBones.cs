using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBones : MonoBehaviour
{
    private bool isInTrigger = false;
    bool fakePickup;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || fakePickup)
        {
            Debug.Log("Pressed F inside trigger");
            for (int i = 0; i < bonesInRange.Count; i++)
            {
                GameObject currentBone = bonesInRange[i];
                if (currentBone == null)
                {
                    bonesInRange.RemoveAt(i);
                    continue;
                }

                if (fakePickup)
                {
                    fakePickup = false;
                }

                List<string> names = new List<string>();
                foreach (var bone in BoneChoser.instance.bonesDisabled)
                {
                    names.Add(bone.name);
                }

                if (names.Contains(currentBone.name))
                {
                    BoneChoser.instance.GetBoneBack(currentBone.name, currentBone);
                }
            }
            

        }
    

}

    List<GameObject> bonesInRange = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Bone"))
    {
        
            bonesInRange.Add(other.gameObject);
        
    }
}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bone"))
        {
            if (bonesInRange.Contains(other.gameObject))
            {
                bonesInRange.Remove(other.gameObject);
            }
            else
            {
                Debug.Log("Weird Bone");
            }
            
        }
    }

}
