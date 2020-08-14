using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    private Rigidbody rbTumble;
    [SerializeField] private float tumble;
    private void Awake()
    {
        rbTumble = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rbTumble.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
