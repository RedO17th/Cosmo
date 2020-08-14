using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{
    private Rigidbody rbShip;
    [SerializeField] private float speedShip = 1.0f;
    [SerializeField] Boundary boundaryShip;
    [SerializeField] private float tilt;
    [SerializeField] GameObject shot;
    [SerializeField] Transform shotSpawn;
    [SerializeField] private float fireRate;
    //Мое
    private float nextFire;
    private Joistick joistick;
    private GameController gController;
    private UIController uiControl;
    //Загрузка главного менеджера
    private HeadManager hManager;
    private bool isHit = false;
    [SerializeField] private GameObject engine;
    private MeshCollider playersColl;
    //Ожидание мелькания игрока в момент попадания
    [SerializeField] private float timeWaitHit;
    private float cashTimeWaitHit;
    //Жизни
    private int countLife;

    private LifeBarScript lifeBar;
    void Awake()
    {
        countLife = 3;
        rbShip = GetComponent<Rigidbody>();
        playersColl = GetComponent<MeshCollider>();
        cashTimeWaitHit = timeWaitHit;
        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            hManager = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<HeadManager>();
            if (hManager == null)
            {
                Debug.Log("Отсутствует скрипт HeadManager. PController");
            }
            else
                hManager.GetPlayer(this);
        }
    }
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GController"))
        {
            gController = GameObject.FindGameObjectWithTag("GController").transform.GetComponent<GameController>();
            if (gController == null)
                Debug.Log("Отсутствует GameController объект в PC");
        }
        else
            Debug.Log("Отсутствует GameController в сцене");

        if (GameObject.FindGameObjectWithTag("UIC"))
        {
            uiControl = GameObject.FindGameObjectWithTag("UIC").transform.GetComponent<UIController>();
            if (uiControl == null)
                Debug.Log("Отсутствует uiC in PC");
        }
        else
            Debug.Log("Отсутствует родительский объект UIController");

        //joistick = GameObject.FindGameObjectWithTag("Joistick").GetComponent<Joistick>();
        joistick = uiControl.GetJoistick();
        if (joistick == null)
            Debug.Log("Отсутствует скрипт управления Джойстиком");

        lifeBar = GameObject.Find("LifeBar").transform.GetComponent<LifeBarScript>();
        if (lifeBar == null)
            Debug.Log("Отсутствует скрипт объекта LifeBarScript. PC.");
           
    }
    void Update()
    {
        if (hManager.SendPause())
        { 
            //Поменять кнопки
            if (gController.ThisDectop())
            {
                if (Input.GetButton("Fire1") && Time.time > nextFire)
                {
                    InstShoot();
                }
            }
            else
            {
                if (uiControl.ShootB().CanFire() && Time.time > nextFire)
                {
                    InstShoot();
                }
            }        
        }

    }
    private void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = joistick.Horizontal();
        float moveVertical = joistick.Vertical();
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rbShip.velocity = movement * speedShip;

        rbShip.position = new Vector3
        (
            Mathf.Clamp(rbShip.position.x, boundaryShip.xMin, boundaryShip.xMax),
            0.0f,
            Mathf.Clamp(rbShip.position.z, boundaryShip.zMin, boundaryShip.zMax)
        );
        rbShip.rotation = Quaternion.Euler(.0f, .0f, rbShip.velocity.x * -tilt);

        if (isHit)
        {
            timeWaitHit -= Time.fixedDeltaTime;

            if (timeWaitHit <= 0f)
            {
                isHit = false;
                timeWaitHit = cashTimeWaitHit;
            }
        }
    }
    private void InstShoot()
    {
        nextFire = Time.time + fireRate;
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }
    public void HitOn()
    {
        if (!isHit)
        {
            isHit = true;
            StartCoroutine(Hit());
        }
    }
    IEnumerator Hit()
    {
        UnTouch(false);
        while (true)
        {
            GetComponent<MeshRenderer>().enabled = false;
            engine.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            GetComponent<MeshRenderer>().enabled = true;
            engine.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            if (!isHit)
                break;
        }
        UnTouch(true);
    }
    public bool Damage()
    {
        countLife--;
        //Messenger<int>.Broadcast(GameEvent.LIFE_HIT, countLife);
        lifeBar.MinusLife(countLife);
        if (!Convert.ToBoolean(countLife))
        {
            hManager.PlayerIsDEad();
            hManager.DeletePointAtLvl(hManager.GetIndexCurrentScoreArray());
            Messenger.Broadcast(GameEvent.GAME_END);
            Messenger<string>.Broadcast(GameEvent.SHOW_MENU, hManager.LString());

            gController.EndLvl();
            return true;
        }
        else
            return false;
    }
    public void UnTouch(bool flag)
    {
        playersColl.enabled = flag;
    }
}
