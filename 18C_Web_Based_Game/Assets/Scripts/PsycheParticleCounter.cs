using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsycheParticleCounter : MonoBehaviour
{
    public bool partOfPsyche = false; //Will be updated to true if it touches a particle that is partOfPsyche
    
    // Start is called before the first frame update
    void Start()
    {
        //Determine if it is the central asteroid particle of Psyche
        if(this.name == "MiddleAsteroidParticle")
        {
            partOfPsyche = true;
        }
    }

    void OnEnable()
    {
        //Determine if it is the central asteroid particle of Psyche
        if (this.name == "MiddleAsteroidParticle")
        {
            partOfPsyche = true;
        }
    }

    //This propagates the partOfPsyche boolean throughout the particles touching the center particle
    void OnCollisionStay2D (Collision2D collision)
    {
        //Debug.Log("we hit something");
        PsycheParticleCounter script = collision.collider.GetComponent<PsycheParticleCounter>();
        if (script.partOfPsyche == true || partOfPsyche == true)
        {
            //Debug.Log("We in here");
            script.partOfPsyche = true;
            partOfPsyche = true;
        }
    }
}
