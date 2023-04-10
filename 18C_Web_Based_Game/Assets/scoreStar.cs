using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreStar : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    
    public TMP_Text scorePoints;
    public TMP_Text massFeedback;
    public TMP_Text otherParticleFeedback;
    public TMP_Text starCongrats;

    private int numberofStars;
    private int points;
    // Start is called before the first frame update
    void Start()
    {
        //points = Random.Range(1, 1000);
        //numberofStars = Random.Range(1, 3);
        //star1.SetActive(false);
        //star2.SetActive(false);
        //star3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //scorePoints.text=points.ToString();

        //90+ score = 3 stars
        //50+ score = 2 stars
        //<50 score = 1 star

        double totalScore = ScoringController.totalScore_static;
        bool needsAdditionalMass = ScoringController.needsAdditionalMass_static;
        bool tooManyNonMetalParticles = ScoringController.tooManyNonMetalParticles_static;

        Debug.Log("Recieved score: " + totalScore);
        if(totalScore >= 90)
        {
            numberofStars = 3;
        }

        if(totalScore < 90 && totalScore >= 50)
        {
            numberofStars = 2;
        }

        if(totalScore < 50 && totalScore >= 25)
        {
            numberofStars = 1;
        }

        else
        {
            numberofStars = 0;
        }

        if (numberofStars == 1)
        {
            star2.SetActive(true);
            star1.SetActive(false);
            star3.SetActive(false);
            starCongrats.text = "You recieved 1 star!";
        }
        else if (numberofStars == 2)
        {
            star1.SetActive(true);
            star3.SetActive(true);
            star2.SetActive(false);
            starCongrats.text = "You recieved 2 stars!";
        }
        else if (numberofStars == 3)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            starCongrats.text = "You recieved 3 stars";
        }
        else
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
            starCongrats.text = "You recieved 0 stars. Try Agian!";
        }

        if(needsAdditionalMass == true)
        {
            massFeedback.text = "The Psyche you created doesn't have enough mass. Increasing the mass will award you a higher score.";
        }
        if(massFeedback == false)
        {
            massFeedback.text = "The mass of the Psyche you created is very close to the real world Psyche. You get full points for the mass aspect of the score!";
        }

        if(tooManyNonMetalParticles == true)
        {
            otherParticleFeedback.text = "Try to remove additional mantle/crust particles to get a better score";
        }
        if(massFeedback == false)
        {
            otherParticleFeedback.text = "The Psyche you created is made almost entirely of metal. You get full points!";
        }
    }
}
