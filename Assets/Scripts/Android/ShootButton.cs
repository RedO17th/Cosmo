using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool touched = false;
    private bool canFire = false;
    private int pointerID;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            touched = !touched;
            pointerID = eventData.pointerId;
            canFire = !canFire;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            touched = !touched;
            canFire = !canFire;
        }
    }
    public bool CanFire()
    {
        return canFire;
    }
    public bool FireButton()
    {
        return Input.GetButton("Fire1"); 
    }
}
