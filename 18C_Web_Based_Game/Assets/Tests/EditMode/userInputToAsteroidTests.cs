using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class userInputToAsteroidTests
{
    //This script will be used to test the UserInputToAsteroid.cs script.
    // code that can be verified via a test will be tested to make sure it is functioning properly.
    // If code can be verified in the Unity editor it will be noted which lines of code 
    // don't need a test written for it.


    //Lines 1 - 77 consist of code that takes values from the user input fields and applies them to 
    // useable values. They are seen in the debug menu and don't need tests written for them.

    [Test]
    public void xVelocityNoAngle()
    {
        int veloInput = 5; //Let the velocity = 5
        int angleInput = 0; //Ley the angle = 0

        float xVelocity = veloInput * Mathf.Cos(angleInput * Mathf.Deg2Rad);

        //The expected value should be 5 units of velocity to the right.
        Assert.AreEqual(5f, xVelocity);
    }

    [Test]
    public void xVelocity45Angle()
    {
        int veloInput = 5;
        int angleInput = 45;


        float xVelocity = veloInput * Mathf.Cos(angleInput * Mathf.Deg2Rad);

        //The expected value should be 3.53553391 units of velocity to the right.
        Assert.AreEqual(3.53553391f, xVelocity);
    }

    [Test]
    public void xVelocityNeg45Angle()
    {
        int veloInput = 5;
        int angleInput = -45;


        float xVelocity = veloInput * Mathf.Cos(angleInput * Mathf.Deg2Rad);

        //The expected value should be 3.53553391 units of velocity to the right.
        Assert.AreEqual(3.53553391f, xVelocity);
    }

    [Test]
    public void yVelocityNoAngle()
    {
        int veloInput = 5; //Let the velocity = 5
        int angleInput = 0; //Ley the angle = 0

        float xVelocity = veloInput * Mathf.Sin(angleInput * Mathf.Deg2Rad);

        //The expected value should be 0 units of velocity to the up/down.
        Assert.AreEqual(0f, xVelocity);
    }

    [Test]
    public void yVelocity45Angle()
    {
        int veloInput = 5;
        int angleInput = 45;


        float xVelocity = veloInput * Mathf.Sin(angleInput * Mathf.Deg2Rad);

        //The expected value should be 3.53553391 units of velocity to the right.
        Assert.AreEqual(3.53553391f, xVelocity);
    }

    [Test]
    public void yVelocityNeg45Angle()
    {
        int veloInput = 5;
        int angleInput = -45;


        float xVelocity = veloInput * Mathf.Sin(angleInput * Mathf.Deg2Rad);

        //The expected value should be -3.53553391 units of velocity to the right.
        Assert.AreEqual(-3.53553391f, xVelocity);
    }

    // Line 82 is where the cVelocity and yVelocity are applied to the otherAsteroid this can be seen in the Unity editor.

    //Lines 84 - 86 enable the timer script attached to the otherAsteroid. This can be seen in the editor.

    //Lines 88 - 93 delete the angle projection line. This is a visual thing and can be seen in the editior.

    //Lines 102 - 127 are declaring variables and getting referenes to gameObjects

    //Lines 129 - 151 count the number of each particle that is a part of the particleList attached to the ParticleCounter GameObject
    // The counts are printed to the debug.log and are can be verified by looking at the counter within the ParticleCounter Script.
    
    //This method will calcuate the score in the following tests
    public double Scoring(float totalMass, int totalNumNonMetal)
    {
        double totalScore = 0;
        double massScore = 0;
        double numberOfNonMetalScore = 0;
        bool needsMoreMass = false; //Used to give feedback on the win/lose page
        bool tooManyNonMetal = false; //Used to give feedback on the win/lose page

        //Best possible mass score
        if (totalMass >= 90 && totalMass <= 110)
        {
            massScore = 50;
            ScoringController.correctMass_static = true;
        }

        //2nd best mass score
        if (totalMass >= 80 && totalMass < 90)
        {
            massScore = 40;
            needsMoreMass = true;
        }
        if (totalMass < 120 && totalMass > 110)
        {
            massScore = 40;
            needsMoreMass = false;
        }

        //3rd best mass score
        if (totalMass >= 70 && totalMass < 80)
        {
            massScore = 30;
            needsMoreMass = true;
        }
        if (totalMass <= 130 && totalMass > 120)
        {
            massScore = 30;
            needsMoreMass = false;
        }

        //4th best mass score
        if (totalMass >= 60 && totalMass < 70)
        {
            massScore = 20;
            needsMoreMass = true;
        }
        if (totalMass <= 140 && totalMass > 130)
        {
            massScore = 20;
            needsMoreMass = false;
        }

        //5th mass score bracket
        if (totalMass >= 50 && totalMass < 60)
        {
            massScore = 10;
            needsMoreMass = true;
        }
        if (totalMass <= 150 && totalMass > 140)
        {
            massScore = 10;
            needsMoreMass = false;
        }

        //final mass score bracket
        if (totalMass < 50)
        {
            massScore = 0;
            needsMoreMass = true;
        }
        if (totalMass > 150)
        {
            massScore = 0;
            needsMoreMass = false;
        }


        //MakeUp Scoring
        if (totalNumNonMetal >= 0 && totalNumNonMetal < 5)
        {
            numberOfNonMetalScore = 50;
            tooManyNonMetal = false;
        }

        if (totalNumNonMetal >= 5 && totalNumNonMetal < 10)
        {
            numberOfNonMetalScore = 40;
            tooManyNonMetal = true;
        }

        if (totalNumNonMetal >= 10 && totalNumNonMetal < 15)
        {
            numberOfNonMetalScore = 30;
            tooManyNonMetal = true;
        }

        if (totalNumNonMetal >= 15 && totalNumNonMetal < 25)
        {
            numberOfNonMetalScore = 20;
            tooManyNonMetal = true;
        }

        if (totalNumNonMetal >= 25 && totalNumNonMetal < 40)
        {
            numberOfNonMetalScore = 10;
            tooManyNonMetal = true;
        }

        if (totalNumNonMetal >= 40)
        {
            numberOfNonMetalScore = 0;
            tooManyNonMetal = true;
        }

        totalScore = massScore + numberOfNonMetalScore;
        return totalScore;
    }

    [Test]
    public void ScoreTest1()
    {
        float totalMass = 80;
        int NumofNonMetal = 10;

        double totalScore = Scoring(totalMass, NumofNonMetal);

        // mass score = 40, MakeUp score = 30
        Assert.AreEqual(70, totalScore);
    }

    [Test]
    public void ScoreTest2()
    {
        float totalMass = 140;
        int NumofNonMetal = 0;

        double totalScore = Scoring(totalMass, NumofNonMetal);

        // mass score = 20, MakeUp score = 50
        Assert.AreEqual(70, totalScore);
    }

    [Test]
    public void ScoreTest3()
    {
        float totalMass = 10;
        int NumofNonMetal = 50;

        double totalScore = Scoring(totalMass, NumofNonMetal);

        // mass score = 0, MakeUp score = 0
        Assert.AreEqual(0, totalScore);
    }

    [Test]
    public void ScoreTest4()
    {
        float totalMass = 101;
        int NumofNonMetal = 2;

        double totalScore = Scoring(totalMass, NumofNonMetal);

        // mass score = 50, MakeUp score = 50
        Assert.AreEqual(100, totalScore);
    }

    [Test]
    public void ScoreTest5()
    {
        float totalMass = 75;
        int NumofNonMetal = 9;

        double totalScore = Scoring(totalMass, NumofNonMetal);

        // mass score = 30, MakeUp score = 40
        Assert.AreEqual(70, totalScore);
    }


    //Lines 383 - 561 Are a repeat of the scoring algorith but this time they save the scores
    // into a script so the win page can use it. No need to verify agian.

    //Lines 565 - 590 draw the projection line from the otherAsteroid. This behavor can be tested in the 
    // Unity editor and don't need unit tests.

    //Lines 592 - 600 delete the projection line. This behavor is observable in the Unity editior

    //Lines 601 - 612 set a value equal to the material the player selected. This does't need a unit test made for it

    //Lines 613 - 640 set the otherAsteroid to the correct material type as well as chnages its color/tag.
    // All of these can be seen within the otherAsteroid GameObject

    //Lines 642 - 724 handle moving the otherAsteroid to different locations while destroying existing 
    // projection lines and creating a new one. This behavior can be seen in the Unity editior so no unit tests are needed.

    //Lines 736 - 735 contain the setOtherAsteroid() and the Awake()
    // The setOtherAsteroid uses a public GameObject variable so we can check in the editior which GameObject is being referenced.
    // The Awake() doesn't have any functioanlty anymore but is still there.

}
