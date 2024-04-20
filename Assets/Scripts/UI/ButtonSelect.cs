using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("selected " + this.name);
        Actions.LevelSelectButtonPressed(this.gameObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("deselected" + this.name);
    }

}
