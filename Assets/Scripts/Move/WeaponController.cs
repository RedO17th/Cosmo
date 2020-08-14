using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotSpawn;
    [SerializeField] private float fireRate;
    [SerializeField] private float delay;

    private AudioSource clipShot;
    void Awake()
    {
        clipShot = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
    }
    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        clipShot.Play();    
    }
}
