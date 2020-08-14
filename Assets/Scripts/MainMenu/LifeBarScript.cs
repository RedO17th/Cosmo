using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarScript : MonoBehaviour
{
    private RectTransform[] lives;
    public int countLife;
    public List<int> numberLives;
    private void Awake()
    {
        countLife = 0;
        int randomValue = transform.childCount + 1;
        numberLives = new List<int>() { randomValue };

        lives = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            lives[i] = (RectTransform)transform.GetChild(i);
            lives[i].GetComponent<Image>().enabled = true;
        }
        //Из PlayerController
        //Messenger<int>.AddListener(GameEvent.LIFE_HIT, MinusLife);
        //Debug.Log("Перезагрузка уровня и countLife = " + countLife);
    }
    public void MinusLife(int indexLive)
    {
        if (indexLive >= 0) //&& CheckNumbers(indexLive))
        { 
            if (lives[indexLive] != null)
            {
                lives[indexLive].GetComponent<Image>().enabled = false;
            }
            else
                Debug.Log("Отсутствует элемент Жизни в массиве lives. LBS.");
        }
    }
    //private bool CheckNumbers(int number)
    //{
    //    bool temp = true;
    //    Debug.Log(" В методе ");
    //    foreach (var numb in numberLives)
    //    {
    //        if (number == numb)
    //        {
    //            temp = false;
    //            break;
    //        }
    //    }
    //    if (temp)
    //        numberLives.Add(number);
    //    Debug.Log(" В методе и число " + temp + " " + number);
    //    return temp; 
    //}
}
