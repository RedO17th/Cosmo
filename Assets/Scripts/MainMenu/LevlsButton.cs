using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevlsButton : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private int numberPointNextLvl;
    private HeadManager hManager;
    //Открыт уровень или нет.
    private bool state = false;
    private CircleCollider2D coll;
    private Image imgButton;
    private Animation anim;
    [SerializeField] private float alphaImgYEnabled;
    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        imgButton = GetComponent<Image>();
        anim = GetComponent<Animation>();

        coll.enabled = false;
        anim.enabled = false;
    }
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            hManager = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<HeadManager>();
        }
    }
    void OnMouseDown()
    {
        hManager.LoadLevl(levelNumber, numberPointNextLvl);
    }
    public void ToggleButtonState()
    {
        var tempColor = imgButton.color;
            state = true;
            coll.enabled = state;
            anim.enabled = state;

            tempColor.a = alphaImgYEnabled;
            imgButton.color = tempColor;
    }
    public bool ReturnStayButton()
    {
        return state;
    }

}
