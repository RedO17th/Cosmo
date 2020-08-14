using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadManager : MonoBehaviour
{
    private static bool created = false;
    //Заменить назад на private-----------------------------------------------------
    public int[] scoreArray;
    private int[] arrayOfPoints;
    public bool[] openLevels;
    //Заменить назад на private-----------------------------------------------------
    public bool[] scoreAtLevels;
    private int numberOfLvl = 0;
    private int indexPoints;

    private string stringWin;
    private string stringLose;

    public ControllUIFunctionality controlUI;

    private PlayerController player;
    private GameController gController;

    private bool isPause = true;
    private bool isReload;// = false;
    private bool isDead = false;

    [Range(0.1f, 0.5f)]
    [SerializeField] private float timeWaitReload;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //Init
        InitScoreArrays();
    }
    void Start()
    {
        //Ссылка на скрипт управляющий UI. После получения скрипта инициализируем кнопки
        InitUIControllerAfterTime();
    }
    private void Update()
    {
        if (isReload && !isDead)
        {
            isReload = false;
            GoToMenu();
            InitUIControllerAfterTime();
            Debug.Log("REloadNotDEad");
        }
        if (isReload && isDead)
        {
            
            isReload = false;
            isDead = false;
            Debug.Log("DEAD");
            //Мы не нажали, а уже пролетел таймер
            InitUIControllerAfterTime();
            //StartCoroutine(DeadReload());
        }

    }
    private void InitUIControllerAfterTime()
    {
        StartCoroutine(InitUI());
    }
    IEnumerator InitUI()
    {
        yield return new WaitForSeconds(timeWaitReload);
        //Debug.Log("Before");
        controlUI = GameObject.FindGameObjectWithTag("CanvController").GetComponent<ControllUIFunctionality>();
        //Debug.Log("After");
        if (controlUI == null)
            Debug.Log("ОТсутствует управляющий скрипт UI. HM.");
        InitButtons();
    }
    private void InitButtons()
    {
        for (int i = 0; i < openLevels.Length; i++)
        {
            if (openLevels[i])
            {
                controlUI.OpenLvlButton(i);
            }
        }
    }
    void InitScoreArrays()
    {
        arrayOfPoints = new int[] { 5, 6, 7 };
        scoreArray = new int[] { 0, 0, 0 };
        openLevels = new bool[] { true, false, false };
        scoreAtLevels = new bool[] { false, false, false };
        stringWin = "YOU ARE WIN";
        stringLose = " ... ";
    }
    public void LoadLevl(int indexScene, int countPointsAtLvl = 0)
    {
        indexPoints = countPointsAtLvl;
        numberOfLvl = indexScene;
        SceneManager.LoadScene(indexScene);
    }
    public int CurrentLvl()
    {
        return numberOfLvl;
    }
    public int CurrentPoins()
    {
        //return arrayOfPoints[--numberOfLvl];
        //Debug.Log("indexPoints " + indexPoints);
        return arrayOfPoints[indexPoints];
    }
    public string WinString()
    {
        return stringWin;
    }
    public string LString()
    {
        return stringLose;
    }
    public void GetPointLvl(int indxLvl, int point)
    {
        //indxLvl--;
        //if (!scoreAtLevels[indxLvl])
        //{
        //    if (scoreArray[indxLvl] < arrayOfPoints[indxLvl])
        //    {
        //        scoreArray[indxLvl]++;
        //    }
        //    if (scoreArray[indxLvl] == arrayOfPoints[indxLvl])
        //    {
        //        Messenger.Broadcast(GameEvent.GAME_END);
        //        Messenger<string>.Broadcast(GameEvent.SHOW_MENU, WinString());
        //        player.UnTouch(false);
        //        if (gController == null)
        //        {
        //            GetGameController();
        //            gController.EndLvl();
        //        }
        //        else
        //            gController.EndLvl();
        //        scoreAtLevels[indxLvl] = true;
        //        OpenNextLvl();
        //    }
        //}
        if (!scoreAtLevels[indexPoints])
        {
            if (scoreArray[indexPoints] < arrayOfPoints[indexPoints])
            {
                scoreArray[indexPoints]++;
            }
            if (scoreArray[indexPoints] == arrayOfPoints[indexPoints])
            {
                Messenger.Broadcast(GameEvent.GAME_END);
                Messenger<string>.Broadcast(GameEvent.SHOW_MENU, WinString());
                player.UnTouch(false);
                if (gController == null)
                {
                    GetGameController();
                    gController.EndLvl();
                }
                else
                    gController.EndLvl();
                //scoreAtLevels[indexPoints] = true;
                //OpenNextLvl();
            }
        }
    }
    public void GetPlayer(PlayerController pl)
    {
        player = pl;
    }
    public void ReloadLVL(bool reload)
    {
        //isReload = true;
        isReload = reload;
    }
    public void Pause(bool state)
    {
        if (state)
        {
            TimeScaleState(0f);
            isPause = false;
        }
        else
        {
            TimeScaleState(1f);
            isPause = true;
        }
    }
    public bool SendPause()
    {
        return isPause;
    }
    public void TimeScaleState(float state)
    {
        Time.timeScale = state;
    }
    public void DeletePointAtLvl(int numberLvl)
    {
        scoreArray[numberLvl] = 0;
        scoreAtLevels[numberLvl] = false;
    }
    private void GetGameController()
    {
        gController = GameObject.FindGameObjectWithTag("GController").transform.GetComponent<GameController>();
    }
    public int GetIndexCurrentScoreArray()
    {
        return indexPoints;
    }
    private void GoToMenu()
    {
        scoreAtLevels[indexPoints] = true;
        OpenNextLvl();
    }
    private void OpenNextLvl()
    {
        for (int i = 0; i < openLevels.Length; i++)
        {
            if (!openLevels[i])
            {
                openLevels[i] = true;
                break;
            }
        }
    }
    public void PlayerIsDEad()
    {
        isDead = true;
    }
    //IEnumerator DeadReload()
    //{
    //    if (isDead)
    //    {
    //        yield return new WaitForSeconds(10f);
    //        isDead = false;
    //        Debug.Log("ReloadAfterDead");
    //    }    
    //}
}
