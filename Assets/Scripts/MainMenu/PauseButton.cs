using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private UIController uiControl;
    private bool isOpen = false;
    private SphereCollider collaider;
    private void Awake()
    {
        collaider = GetComponent<SphereCollider>();
    }
    private void Start()
    {
        uiControl = GameObject.FindGameObjectWithTag("UIC").transform.GetComponent<UIController>();
        if (uiControl == null)
            Debug.Log("Отсутствует uiC in PB");
    }
    private void OnMouseDown()
    {
        isOpen = !isOpen;
        uiControl.ShowMenuOnPause(isOpen);
    }
    public void EnableToggle()
    {
        collaider.enabled = false;
    }
}
