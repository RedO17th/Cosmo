using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] hazards;
    [SerializeField] private Vector3 spawnValues;
    [SerializeField] private int hazardCount;
    [SerializeField] private float spawnWait;
    [SerializeField] private float startWait;
    [SerializeField] private float wavetWait;
    private bool gameOver = false;
    private bool isDesctop = false;
    private UIController uiControl;
    //Загрузка главного менеджера
    private HeadManager hManager;
    //будущий индекс уровня
    private int numberOfLvl = 0;
    //колво баллов по настройкам
    private int countPointsSettings = 0;
    public int countPointsAtLvl;
    void Awake()
    {
        countPointsAtLvl = 0;
        //Из DestroyByContact
        //Messenger.AddListener(GameEvent.ASTEROID_HIT, GetPoint);
        //Из HeadManager
        Messenger.AddListener(GameEvent.GAME_END, GameOver);
        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            hManager = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<HeadManager>();
            if (hManager != null)
            {
                numberOfLvl = hManager.CurrentLvl();
                countPointsSettings = hManager.CurrentPoins();
            }
            else
            {
                Debug.Log("Не нашел скрипт HedManager. GC");
            }
        }
    }
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("UIC"))
        {
            uiControl = GameObject.FindGameObjectWithTag("UIC").transform.GetComponent<UIController>();
            if (uiControl == null)
                Debug.Log("Отсутствует uiC in GC");
            else
            {
                uiControl.SetPoints(countPointsSettings);
            }
        }
        else
            Debug.Log("Отсутствует родительский объект UIController");

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            isDesctop = true;
            uiControl.VisibleButtons(false);
        }

        StartCoroutine(SpawnWaves());

    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazards[Random.Range(0, hazards.Length)], spawnPosition, spawnRotation);
                
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(wavetWait);
            if (gameOver)
            {
                break;
            }
        }
    }
    private void GameOver()
    {
        gameOver = true;
    }
    public HeadManager SendHManager()
    {
        return hManager;
    }
    public bool ThisDectop()
    {
        return isDesctop;
    }
    public Joistick ReturnJoistick()
    {
        return uiControl.GetJoistick();
    }
    //Получение очков с попадания по астероидам (из DestroyByContact)
    public void GetPoint()
    {
        countPointsAtLvl++;
        //Debug.Log("Стало и инициализировалось: " + countPointsAtLvl + " " + countPointsSettings);
        if (countPointsAtLvl <= countPointsSettings)
        {
            hManager.GetPointLvl(numberOfLvl, countPointsAtLvl);
            uiControl.AsteroidHit();
            //передать данный балл в HeadManager hManager
        }
        else
            countPointsAtLvl = 0;
    }
    public void GameOnPause(bool state)
    {
        if (hManager != null)
            hManager.Pause(state);
        else
            Debug.Log("Отсутствует HeadManager. GC.");
    }
    public void EndLvl()
    {
        uiControl.SwitchOfPauseButton();
    }
}
