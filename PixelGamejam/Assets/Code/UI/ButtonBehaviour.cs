using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
    BoneChoser bc;
    public void OnPointerClick(PointerEventData eventData)
    {

        bc = transform.parent.GetComponent<BoneChoser>();
        bc.GetThisBone(name, this.gameObject);

    }
}
