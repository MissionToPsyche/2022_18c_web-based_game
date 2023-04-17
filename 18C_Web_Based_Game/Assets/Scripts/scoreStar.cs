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
    public TMP_Text currentMass;
    public TMP_Text massScore;
    public TMP_Text makeUpScore;
    public TMP_Text otherParticleFeedback;
    public TMP_Text starCongrats;

    private int numberofStars;
    private int points;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("scoring toomanymetal:" + ScoringController.tooManyNonMetalParticles_static);
        Debug.Log("Mass bool:" + ScoringController.needsAdditionalMass_static);

        double totalScore = ScoringController.totalScore_static;
        bool needsAdditionalMass = ScoringController.needsAdditionalMass_static;
        bool tooManyNonMetalParticles = ScoringController.tooManyNonMetalParticles_static;

        Debug.Log("Recieved score: " + totalScore);
        if (totalScore >= 90)
        {
            numberofStars = 3;
        }
        else if (totalScore < 90 && totalScore >= 50)
        {
            numberofStars = 2;
        }

        else if (totalScore < 50 && totalScore >= 25)
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
            starCongrats.text = "You recieved 3 stars!";
        }
        else
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
            starCongrats.text = "You recieved 0 stars. Try Agian!";
        }
        if (ScoringController.correctMass_static == true)
        {
            massFeedback.text = "Congrats! the Psyche you created falls within the acceptable mass range!";
        }
        else if (needsAdditionalMass == true)
        {
            massFeedback.text = "The Psyche you created doesn't have enough mass. Increasing the mass will award you a higher score.";
        }
        else if (needsAdditionalMass == false)
        {
            massFeedback.text = "The mass of Psyche you created is too large. Try to reduce the mass for a better score.";
        }

        if (tooManyNonMetalParticles == true)
        {
            otherParticleFeedback.text = "Try to remove additional mantle/crust particles to get a better score";
        }
        else if (tooManyNonMetalParticles == false)
        {
            otherParticleFeedback.text = "The Psyche you created is made almost entirely of metal. You get full points!";
        }

        //Display the current mass of the user-created Psyche
        currentMass.text = "Psyche's Mass: " + ScoringController.totalMass_static + " units";

        //Display the mass score
        massScore.text = "Mass Score: " + ScoringController.massScore_static;

        //Display the MakeUp Score
        makeUpScore.text = "Make-Up Score: " + ScoringController.MakeUpScore_static;
    }
}
