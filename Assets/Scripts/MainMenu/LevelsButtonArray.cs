using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsButtonArray : MonoBehaviour
{
    [SerializeField] private GameObject[] arrayButtons;
    public GameObject[] ReturnArrayButLvl()
    {
        return arrayButtons;
    }

}
