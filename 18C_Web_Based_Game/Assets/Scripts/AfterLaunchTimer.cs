using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterLaunchTimer : MonoBehaviour
{
    //public float timer = 5.0f; //will count down 5 seconds
    //public GameObject PsycheCentralParticle;

    //testing (can be seen in unity editor)
    public List<GameObject> particles;

    //Used to reenable the UI after a launch
    public GameObject parentHUD;

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Wait is over 1");

        //Enable the PsycheParticleCounter script on the MiddleAsteroidParticle
        GameObject middleParticle = GameObject.Find("MiddleAsteroidParticle");
        PsycheParticleCounter script = middleParticle.GetComponent<PsycheParticleCounter>();
        script.enabled = true;

        //wait another 5 seconds to allow the boolean to propagate through the particles
        yield return new WaitForSeconds(5);
        Debug.Log("Wait is over 2");

        //Call the countWhatParticlesArePartOfPsyche() method in ParticleCounter
        GameObject particleCounter = GameObject.Find("ParticleCounter");
        particleCounter.GetComponent<ParticleCounter>().countWhatParticlesArePartOfPsyche();
        Debug.Log("After counting");

        /*
        //Remove particles from system that aren't part of Psyche
        GameObject ParticleCount = GameObject.Find("ParticleCounter");
        ParticleCounter script3 = ParticleCount.GetComponent<ParticleCounter>();
        particles = script3.particles;

        foreach (GameObject obj in particles)
        {
            ParticleCount.deleteParticle(obj);
        }
        */

        //Set all the particles velocities to zero
        GameObject counter = GameObject.Find("ParticleCounter");
        ParticleCounter script2 = counter.GetComponent<ParticleCounter>();
        particles = script2.particles;

        foreach (GameObject obj in particles)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, 0);
        }

        //Move Psyche back to a default location in the scene
        Vector3 centerParticleLocation = GameObject.Find("MiddleAsteroidParticle").transform.position;
        foreach (GameObject obj in particles)
        {
            Vector3 currentPosition = obj.transform.position;
            Vector3 defaultPosition = new Vector3(0.65f, 1.11f, 0.02680064f);
            obj.transform.position = defaultPosition;
            obj.transform.position = defaultPosition + (centerParticleLocation - currentPosition);
        }

        //set all velovities to zero agian
        foreach (GameObject obj in particles)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, 0);
        }

        //Disable the PsycheParticleCounter script on the MiddleAsteroidParticle
        script.enabled = false;

        //Set all the partOfPsyche booleans to false
        foreach (GameObject obj in particles)
        {
            PsycheParticleCounter script3 = obj.GetComponent<PsycheParticleCounter>();
            script3.partOfPsyche = false;
        }

        //Change the tags of the previous OtherAsteroid
        GameObject oldOtherAsteroid;
        oldOtherAsteroid = GameObject.Find("/Other Asteroid/OtherAsteroid");
        oldOtherAsteroid.name = "oldOtherAsteroid";
        
        //
        string oldTag = oldOtherAsteroid.tag;

        if (oldOtherAsteroid.tag == "OA_MetalCore_Particle")
        {
            oldOtherAsteroid.tag = "MetalCore_Particle";
        }
        else if (oldOtherAsteroid.tag == "OA_Mantle_Particle")
        {
            oldOtherAsteroid.tag = "Mantle_Particle";
        }
        else if (oldOtherAsteroid.tag == "OA_Crust_Particle")
        {
            oldOtherAsteroid.tag = "Crust_Particle";
        }

        //wait a second for the asteroid to move to the default location
        yield return new WaitForSeconds(1); 

        //Generate a new OtherAsteroid left of Psyche
        // Vector3 centerParticleLocation = GameObject.Find("MiddleAsteroidParticle").transform.position;
        Vector3 newPosition = new Vector3(-5.25f, 0.19f, 0.0208f);
        //newPosition = newPosition + centerParticleLocation;
        GameObject OtherAsteroid = Instantiate(oldOtherAsteroid, newPosition, oldOtherAsteroid.transform.rotation);
        OtherAsteroid.name = "OtherAsteroid";
        
        //
        OtherAsteroid.tag = oldTag;
        
        OtherAsteroid.transform.position = newPosition;

        AfterLaunchTimer timerScript = OtherAsteroid.GetComponent<AfterLaunchTimer>();
        timerScript.enabled = false;

        OtherAsteroid.transform.position = newPosition;

        //Move the newly generated OtherAsteroid so its parent object is "Other Asteroid"
        GameObject parent = GameObject.Find("Other Asteroid");
        OtherAsteroid.transform.SetParent(parent.transform, true);

        //Update what the UserInputToAsteroid script sees as the OtherAsteroid
        GameObject inputObject = GameObject.Find("UserInput");
        UserInputToAsteroid inputScript = inputObject.GetComponent<UserInputToAsteroid>();
        inputScript.setOtherAsteroid();
        inputScript.ClickedButton = false;

        //Reenable the UI "HUD Parent"
        parentHUD.SetActive(true);
    }  
    
}
