using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rbBolt;
    private void Awake()
    {
        rbBolt = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rbBolt.velocity = transform.forward * speed;
    }
}
