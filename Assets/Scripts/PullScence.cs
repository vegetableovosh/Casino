using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PullScence : MonoBehaviour, IPointerClickHandler
{
    private string NameObj;

    public void OnPointerClick(PointerEventData eventData)
    {
        NameObj = this.gameObject.name;
        LevelChange.ChangeScence(NameObj);
    }

    void OnMouseDown(){
        NameObj = this.gameObject.name;
        LevelChange.ChangeScence(NameObj);
    }

}
