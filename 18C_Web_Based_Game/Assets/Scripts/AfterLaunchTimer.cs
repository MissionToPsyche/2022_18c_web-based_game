using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterLaunchTimer : MonoBehaviour
{
    //public float timer = 5.0f; //will count down 5 seconds
    public GameObject PsycheCentralParticle;

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Wait is over");
        
        //Enable the PsycheParticleCounter script on the MiddleAsteroidParticle
        GameObject middleParticle = GameObject.Find("MiddleAsteroidParticle");
        PsycheParticleCounter script = middleParticle.GetComponent<PsycheParticleCounter>();
        script.enabled = true;

        //wait another 5 seconds to allow the boolean to propagate through the particles
        yield return new WaitForSeconds(5);
        Debug.Log("Wait is over");

        //Call the countWhatParticlesArePartOfPsyche() method in ParticleCounter
        GameObject particleCounter = GameObject.Find("ParticleCounter");
        particleCounter.GetComponent<ParticleCounter>().countWhatParticlesArePartOfPsyche();
    }
}
