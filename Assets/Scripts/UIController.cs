using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditorInternal;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
    [SerializeField] private RectTransform shootButton;
    [SerializeField] private RectTransform joistick;
    [SerializeField] private RectTransform pauseButton;
    //[SerializeField] private RectTransform lifeBar;
    //private RectTransform[] lives;
    private int points;

    [SerializeField] private RectTransform resMenu;
    ///
    private GameController gController;

    void Awake()
    {
        score = 0;
        //Из DestroyByContact
        //Messenger.AddListener(GameEvent.ASTEROID_HIT, AsteroidHit);
        //Из PlayerController, HeadManager
        Messenger<string>.AddListener(GameEvent.SHOW_MENU, ShowLastMenu);
    }
    void Start()
    {
        scoreText.text = score.ToString() + "/" + points;
        gController = GameObject.FindGameObjectWithTag("GController").transform.GetComponent<GameController>();
        if (gController == null)
            Debug.Log("Отсутствует GameController объект в UIC");
    }
    public void AsteroidHit()
    {
        score++;
        if (score <= points)
        {
            scoreText.text = score.ToString() + "/" + points;
        }
    }
    public void SetPoints(int countPoints)
    {
        points = countPoints;
    }
    public Joistick GetJoistick()
    {
        return joistick.GetComponent<Joistick>();
    }
    public void VisibleButtons(bool active)
    {
        shootButton.gameObject.SetActive(active);
        joistick.gameObject.SetActive(active);
    }
    public ShootButton ShootB()
    {
        return shootButton.GetComponent<ShootButton>();
    }
    //Показ менюшки после потолка баллов на уровне
    public void ShowLastMenu(string strState)
    {
        if (resMenu != null)
        {
            resMenu.gameObject.SetActive(true);
            resMenu.transform.GetChild(1).GetComponent<Text>().text = strState;
            Messenger<string>.RemoveListener(GameEvent.SHOW_MENU, ShowLastMenu);
            //Messenger.RemoveListener(GameEvent.ASTEROID_HIT, AsteroidHit);
        }
        else
            Debug.Log("Отсутствует завершающая менюшка. UIC.");
    }
    public void ShowMenuOnPause(bool state)
    {
        if (resMenu != null)
        {
            resMenu.gameObject.SetActive(state);
            resMenu.transform.GetChild(1).GetComponent<Text>().text = "... PAUSE ... ";
            gController.GameOnPause(state);
        }
    }
    public void SwitchOfPauseButton()
    {
        pauseButton.GetComponent<PauseButton>().EnableToggle();
    }
}
