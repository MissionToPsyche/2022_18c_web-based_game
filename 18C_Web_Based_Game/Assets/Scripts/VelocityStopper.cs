using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Script runs constantly.
// Its job is to stop the particles from moving before the player presses launch
public class VelocityStopper : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        UserInputToAsteroid inputScript =
                        GameObject.Find("UserInput").GetComponent<UserInputToAsteroid>();
        bool isClicked = inputScript.ClickedButton;
        while (true)
        {
            ParticleCounter counterObj = GameObject.Find("ParticleCounter").GetComponent<ParticleCounter>();
            List<GameObject> particles = counterObj.particles;

            if (isClicked == true)
            {
                //Don't stop the particles
            }
            else //Stop the particles
            {
                foreach (GameObject particle in particles)
                {
                    Rigidbody2D rb = particle.GetComponent<Rigidbody2D>();
                    rb.velocity = new Vector2(0, 0);
                }
            }
            //wait 5 seconds then run again
            yield return new WaitForSeconds(5);

            isClicked = inputScript.ClickedButton;
            Debug.Log("isClicked: " + isClicked);
        }
    }
}