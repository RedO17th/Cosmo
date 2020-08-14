using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EvasiveManeuver : MonoBehaviour
{
    //[SerializeField] private float dodge;
    //[SerializeField] private float smooth;
    //[SerializeField] private float tilt;
    //[SerializeField] private Vector2 startWait;
    //[SerializeField] private Vector2 maneuverTime;
    //[SerializeField] private Vector2 maneuverWait;
    //[SerializeField] private Boundary boundary;

    //private float currentSpeed;
    //private float targetManeuver;
    //private Rigidbody rb;
    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    currentSpeed = rb.velocity.z;
    //}
    //void Start()
    //{
    //    StartCoroutine(Evade());
    //}
    //IEnumerator Evade()
    //{
    //    yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
    //    while (true)
    //    {

    //        targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
    //        yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
    //        targetManeuver = 0;
    //        yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
    //    }
    //}
    //void FixUpdate()
    //{
    //    float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smooth);
    //    rb.velocity = new Vector3(newManeuver, .0f, currentSpeed);
    //    rb.position = new Vector3
    //    (
    //        Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
    //        0.0f,
    //        Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
    //    );
    //    rb.rotation = Quaternion.Euler(.0f, .0f, rb.velocity.x * -tilt);
    //}

    public Boundary boundary;
    public float tilt;
    public float dodge;
    public float smoothing;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    private float currentSpeed;
    private float targetManeuver;

    void Start()
    {
        currentSpeed = GetComponent<Rigidbody>().velocity.z;
        StartCoroutine(Evade());
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(GetComponent<Rigidbody>().velocity.x, targetManeuver, smoothing * Time.deltaTime);
        GetComponent<Rigidbody>().velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}
