using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    const float G = 1f;

    public static List<Attractor> Attractors;

    public Rigidbody2D rb;

    //In each update run the Attract() for each particle in the system
    void FixedUpdate()
    {
        
        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this)
            {
                Attract(attractor);
            }
        }
    }

    //add a particle to the Attractors List, if the list doesn't exist yet make it.
    void OnEnable()
    {
        if(Attractors == null)
        {
            Attractors = new List<Attractor>();
        }

        Attractors.Add(this);
    }

    //If a paricle is disabled remove it from the Attactors List
    void OnDisable()
    {
        Attractors.Remove(this);
    }

    //This methods is used to calculate the amount of force between the different 
    // particles in the system.
    void Attract(Attractor objToAttract)
    {
        Rigidbody2D rbToAttract = objToAttract.rb; //get the rigidbody of the other Attractor

        Vector3 direction = rb.position - rbToAttract.position; // determines the direction that the paricles are going in
        float distance = direction.magnitude; //The distance is the absolute value between the paricles
        
        if(distance == 0f)
        {
            return;
        }
        

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        float fewSeconds = 5;
        if(collision.gameObject.tag == "OtherAsteroid")
        {
            Disable(fewSeconds);
        }
    }

    void Disable(float time)
    {
        enabled = false;
        //If called mulitle times reset the timer
        CancelInvoke("Enable");
        Invoke("Enable", time);
    }

    void Enable()
    {
        enabled = true;
    }
    */
}
