using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMenuButton : MonoBehaviour
{
    [SerializeField] private int numberOfLvl;
    private int numberPointNextLvl;
    private bool noReload = true;
    private HeadManager hManager;
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            hManager = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<HeadManager>();
            numberPointNextLvl = hManager.GetIndexCurrentScoreArray();
        }
    }
    private void OnMouseDown()
    {

        if (transform.CompareTag("RestartButton"))
        {
            //Нужно подумать что здесь подправить
            //int numb = numberOfLvl;
            //hManager.DeletePointAtLvl(--numb);
            hManager.DeletePointAtLvl(numberPointNextLvl);
            noReload = false;
        }
        hManager.Pause(false);
        hManager.ReloadLVL(noReload);
        hManager.LoadLevl(numberOfLvl, numberPointNextLvl);
    }
}
