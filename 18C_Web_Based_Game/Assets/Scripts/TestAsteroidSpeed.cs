using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAsteroidSpeed : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    private bool hasCollided = false; //used to enable the Attractor script

    // Start is called before the first frame update
    void Start()
    {
        //For demo purpose have the attractor script be turned off initially
        gameObject.GetComponent<Attractor>().enabled = false;

        //Set the velocity to 0 at startup
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Set the velocity of the OtherAsteroid to the speed upon pressing space
        if(Input.GetKeyDown("space"))
        {
            rb.velocity = transform.right * speed;
        }
    }

    //Used to control what happens after a collision occurs for OtherAsteroid
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Used to tell what has collided with the OtherAsteroid
        //Debug.Log(collision.gameObject.name);
        
        //enable the Attractor script, will only run once.
        if(hasCollided == false)
        {
            gameObject.GetComponent<Attractor>().enabled = true;
            hasCollided = true;
        }
        
    }
}
