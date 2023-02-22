using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuarGravity : MonoBehaviour
{
    public float massOfAsteroid;
    public Transform centerOfAsteroid;
    public float G;

    float massOfParticle;
    float distance;
    float forceValue;
    Vector3 forceDirection;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        massOfParticle = rb.mass;
        distance = Vector3.Distance(centerOfAsteroid.position, transform.position);
        forceValue = G * (massOfAsteroid * massOfParticle) / (distance * distance);
    }

    // Update is called once per frame
    void Update()
    {
        forceDirection = (centerOfAsteroid.position - transform.position).normalized;
        rb.AddForce(forceValue * forceDirection); //We need value and direction of the force
    }
}
