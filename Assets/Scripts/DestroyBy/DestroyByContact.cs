using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject playerExplosion;
    private GameController gameController;
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Не найден контроллер. GameController.");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("EnemyShip"))
        {
            return;
        }
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (transform.CompareTag("Enemy"))
            {
                //В UIController по Астероидам 
                //Messenger.Broadcast(GameEvent.ASTEROID_HIT);
                gameController.GetPoint();
            }
        }
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.HitOn();
            if (player.Damage())
            {
                Instantiate(playerExplosion, player.transform.position, player.transform.rotation);
                Destroy(player.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
