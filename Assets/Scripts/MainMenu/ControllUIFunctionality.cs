using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllUIFunctionality : MonoBehaviour
{
    [SerializeField] private RectTransform lvlBar;
    private GameObject[] arrayButtons;
    private void Awake()
    {
        arrayButtons = lvlBar.GetComponent<LevelsButtonArray>().ReturnArrayButLvl();
        //foreach (var item in arrayButtons)
        //    Debug.Log(item.transform.name);
    }
    public void OpenLvlButton(int indexButton)
    {
        arrayButtons[indexButton].GetComponent<LevlsButton>().ToggleButtonState();
    }

}
