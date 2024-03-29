using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class UserInputToAsteroid : MonoBehaviour
{
    // following variables will be stored for calculating the score
    //public const int DESIREDMASS = 500;

    //public static float totalMass = 254;

    //public static float metalMass = 90;

    //public static float mantleMass = 124;

    //public static float crustMass = 40;
    // end ----------------------------------^

    public bool ClickedButton = false;

    public int massInput;
    public int velocityInput;
    public int angleInput;
    public string materialInput;
    
    public TextMeshProUGUI scoreValue;

    public TMP_InputField massInputField;
    public TMP_InputField velocityInputField;
    public TMP_InputField angleInputField;

    public GameObject otherAsteroid;
    private TestAsteroidSpeed testAsteroidSpeed;


    //The following variables are used for material picking
    private int materialSelected = 0; //by default it should be zero, is changed when user clicks a different option
    private Renderer otherAsteroidRenderer;
    private Color otherAsteroidColor;

    public void StoreParameters()
    {
        //Only allow the user to click launch once
        if (ClickedButton == false)
        {
            bool preventApplicationofParameter = false;

            massInput = Int32.Parse(massInputField.text);
            if (massInput < 0 || massInput > 30)
            {
                massInputField.text = "Not Valid";
                preventApplicationofParameter = true;
            }

            velocityInput = Int32.Parse(velocityInputField.text);
            if(velocityInputField.text == "" || velocityInput > 90 || velocityInput < -90)
            {
                velocityInputField.text = "Not Valid";
                preventApplicationofParameter = true;
            }

            angleInput = Int32.Parse(angleInputField.text);

            if (preventApplicationofParameter == false)
            {
                Debug.Log("mass: " + massInput + " velocity: " + velocityInput +
                          " angle: " + angleInput + " material: " + materialInput);

                //Now acually set the given parameters to the asteroid
                Rigidbody2D rb = otherAsteroid.GetComponent<Rigidbody2D>();

                //mass
                rb.mass = massInput;

                //velocity and angle
                float velx = velocityInput * Mathf.Cos(angleInput * Mathf.Deg2Rad);
                float vely = velocityInput * Mathf.Sin(angleInput * Mathf.Deg2Rad);
                rb.velocity = new Vector2(velx, vely);

                //Enable the timer contained in the AfterLaunchTimer.cs script
                AfterLaunchTimer script = GameObject.Find("OtherAsteroid").GetComponent<AfterLaunchTimer>();
                script.enabled = true;

                //Delete the ProjectionLine
                if (GameObject.Find("ProjectionLine") != null)
                {
                    Destroy(GameObject.Find("ProjectionLine"));
                    Debug.Log("ProjectionLine Destroyed!");
                }

                //Update Clicked Button to true
                ClickedButton = true;
            }
        }

    }

    // Total Score is updated when 'Evaluate Psyche' button is Clicked
    // total Score calculated by:
    //   MakeUp score (greater other particle count = less score) +
    //   Total Mass of Psyche compared to actual mass score
    public void HandleEvaluateClick()
    {
        //Used to hold the scores for each
        double totalScore = 0;
        double massScore = 0;
        double numberOfNonMetalScore = 0;

        //ParticleCounter contains the list of particles that are part of Psyche
        ParticleCounter counterScript =
                        GameObject.Find("ParticleCounter").GetComponent<ParticleCounter>();

        List<GameObject> particleList = counterScript.particles;

        //Used to store the mass and particle counts
        float massOfMetalParticles = 0;
        int numOfMetalParticles = 0;

        float massOfMantleParticles = 0;
        int numOfMantleParticles = 0;

        float massOfCrustParticles = 0;
        int numOfCrustParticles = 0;

        foreach(GameObject obj in particleList)
        {
            //determine the mass of each particle type
            // also determine the count of each particle
            if(obj.tag == "MetalCore_Particle")
            {
                Rigidbody2D rigidbody = obj.GetComponent<Rigidbody2D>();
                massOfMetalParticles = massOfMetalParticles + rigidbody.mass;
                numOfMetalParticles++;
            }
            if(obj.tag == "Mantle_Particle")
            {
                Rigidbody2D rigidbody = obj.GetComponent<Rigidbody2D>();
                massOfMantleParticles = massOfMantleParticles + rigidbody.mass;
                numOfMantleParticles++;
            }
            if(obj.tag == "Crust_Particle")
            {
                Rigidbody2D rigidbody = obj.GetComponent<Rigidbody2D>();
                massOfCrustParticles = massOfCrustParticles + rigidbody.mass;
                numOfCrustParticles++;
            }
        }

        Debug.Log("Metal: " + massOfMetalParticles + ", Mantle: " + massOfMantleParticles + " Crust: " + massOfCrustParticles);
        Debug.Log("Metal particles: " + numOfMetalParticles + ", Mantle particles: " + numOfMantleParticles + " Crust particles: " + numOfCrustParticles);

        //Calculate score based on overall mass of Psyche
        // Psyche has an total estimated mass of 2.7 x 10^19 kg
        // 90 (metal mass units) + 124 (mantle mass units) + 40 (crust mass units) = 254 mass units by default
        // Since Psyche formed by hit-and-run collisions we need to score based on having mass knocked off
        // Lets make the target 100 mass units.
        // 27000000000000000000 kg / 350 = 7.7 x 10^16 kg per mass unit in game
        float totalMass = massOfMetalParticles + massOfMantleParticles + massOfCrustParticles;
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

        //Calculate score based on how many non-metal particles remain
        int totalNumNonMetal = numOfCrustParticles + numOfMantleParticles;
        Debug.Log("totalNumNonMetal:" + totalNumNonMetal);
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
        scoreValue.text = totalScore.ToString();

        string inputForWinPage;
        inputForWinPage = totalScore.ToString() + "," + needsMoreMass.ToString() + "," + tooManyNonMetal.ToString();

        Debug.Log("Mass score = " + massScore + ", NonMetalScore = " + numberOfNonMetalScore);
        /*
        scoreValue.text = massInput.ToString();


        ParticleCounter pScript =
            GameObject.Find("ParticleCounter").GetComponent<ParticleCounter>();
        pScript.enabled = true;


        totalMass += massInput;

        // See what material was entered. Then add mass of material + initial mass of material DIV BY total mass to find the ratio
        if (materialInput == "Metal")
        {
            metalMass += massInput;
            Debug.Log("METAL MASS: " + metalMass);
        }
        else if (materialInput == "Mantle")
        {
            mantleMass += massInput;
            Debug.Log("MANTLE MASS: " + mantleMass);
        }
        else
        {
            // Crust
            crustMass += massInput;
            Debug.Log("CRUST MASS: " + crustMass);
        }
        // get metal Ratio
        float metalRatio = (float)metalMass / (float)totalMass;

        // get mantle Ratio
        float mantleRatio = (float)mantleMass / (float)totalMass;

        // get crust ratio
        float crustRatio = (float)crustMass / (float)totalMass;


        Debug.Log("MASS INPUT: " + massInput);

        Debug.Log("TOTAL MASS: " + totalMass);

        float metalScore = 0;
        float mantleScore = 0;
        float crustScore = 0;

        // IF metal ratio in this range, full metal ratio points, else 0 points
        if (metalRatio <= 0.60 && metalRatio >= 0.30)
        {
            metalScore = 17;
        }
        else
        {
            metalScore = 0;
        }

        // IF crust ratio in this range, full crust ratio points, else 0 points
        if (crustRatio <= 0.10 && crustRatio >= 0.05)
        {
            crustScore = 17;
        }
        else
        {
            crustScore = 0;
        }
        // IF mantle ratio is in this range, full mantle ratio points, else 0 points
        if (mantleRatio <= 0.30 && mantleRatio >= 0.20)
        {
            mantleScore = 16;
        }
        else
        {
            mantleScore = 0;
        }

        // check total mass and develop the score based on it
        // check which is higher, make that the denominator to develop the score
        float scoreTwo = 0;
        float ratio = 0;
        if (totalMass < DESIREDMASS)
        {
            ratio = (float)totalMass / (float)DESIREDMASS;
            scoreTwo = (ratio / 2) * 100;
        }
        else
        {
            ratio = (float)DESIREDMASS / (float)totalMass;
            scoreTwo = (ratio / 2) * 100;
        }

        Debug.Log("METAL SCORE: " + metalScore);
        Debug.Log("CRUST SCORE: " + crustScore);
        Debug.Log("MANTLE SCORE: " + mantleScore);
        Debug.Log("MASS ACCURACY SCORE: " + scoreTwo);

        float totalScore = metalScore + crustScore + mantleScore + scoreTwo;

        Debug
            .Log("TOTAL SCORE: " +
            (metalScore + crustScore + mantleScore + scoreTwo));
        scoreValue.text = totalScore.ToString();
        */
    }

    public void getResults()
    {
        double totalScore = 0;
        double massScore = 0;
        double numberOfNonMetalScore = 0;

        //ParticleCounter contains the list of particles that are part of Psyche
        ParticleCounter counterScript =
                        GameObject.Find("ParticleCounter").GetComponent<ParticleCounter>();

        List<GameObject> particleList = counterScript.particles;

        //Used to store the mass and particle counts
        float massOfMetalParticles = 0;
        int numOfMetalParticles = 0;

        float massOfMantleParticles = 0;
        int numOfMantleParticles = 0;

        float massOfCrustParticles = 0;
        int numOfCrustParticles = 0;

        foreach (GameObject obj in particleList)
        {
            //determine the mass of each particle type
            // also determine the count of each particle
            if (obj.tag == "MetalCore_Particle")
            {
                Rigidbody2D rigidbody = obj.GetComponent<Rigidbody2D>();
                massOfMetalParticles = massOfMetalParticles + rigidbody.mass;
                numOfMetalParticles++;
            }
            if (obj.tag == "Mantle_Particle")
            {
                Rigidbody2D rigidbody = obj.GetComponent<Rigidbody2D>();
                massOfMantleParticles = massOfMantleParticles + rigidbody.mass;
                numOfMantleParticles++;
            }
            if (obj.tag == "Crust_Particle")
            {
                Rigidbody2D rigidbody = obj.GetComponent<Rigidbody2D>();
                massOfCrustParticles = massOfCrustParticles + rigidbody.mass;
                numOfCrustParticles++;
            }
        }

        Debug.Log("Metal: " + massOfMetalParticles + ", Mantle: " + massOfMantleParticles + " Crust: " + massOfCrustParticles);
        Debug.Log("Metal particles: " + numOfMetalParticles + ", Mantle particles: " + numOfMantleParticles + " Crust particles: " + numOfCrustParticles);

        //Calculate score based on overall mass of Psyche
        // Psyche has an total estimated mass of 2.7 x 10^19 kg
        // 90 (metal mass units) + 124 (mantle mass units) + 40 (crust mass units) = 254 mass units by default
        // The game plays better if players can launch asteroids at Psyche that have high mass (more mass and velocicy = larger impact)
        // Lets make the target 350 mass units.
        // 27000000000000000000 kg / 100 = 2.7 x 10^17 kg per mass unit in game
        float totalMass = massOfMetalParticles + massOfMantleParticles + massOfCrustParticles;
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

        //Calculate score based on how many non-metal particles remain
        int totalNumNonMetal = numOfCrustParticles + numOfMantleParticles;
        Debug.Log("totalNumNonMetal:" + totalNumNonMetal);
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
        scoreValue.text = totalScore.ToString();

        ScoringController.totalMass_static = totalMass;
        ScoringController.totalScore_static = totalScore;
        ScoringController.massScore_static = massScore;
        ScoringController.MakeUpScore_static = numberOfNonMetalScore;
        ScoringController.needsAdditionalMass_static = needsMoreMass;
        ScoringController.tooManyNonMetalParticles_static = tooManyNonMetal;

        Debug.Log("ScoringController.tooManyNonMetal: " + ScoringController.tooManyNonMetalParticles_static);

        SceneManager.LoadScene("Win_Screen");
    }

    private LineRenderer lineRenderer;

    public void HandleAngleInput()
    {
        float distance = 5f;

        GameObject otherAsteroid = GameObject.Find("OtherAsteroid");

        DeleteProjectedLine();

        lineRenderer = new GameObject("ProjectionLine").AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.magenta;
        lineRenderer.endColor = Color.magenta;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        //Calculate the end point of the line
        angleInput = Int32.Parse(angleInputField.text);
        float x = otherAsteroid.transform.position.x + distance * Mathf.Cos(angleInput * Mathf.Deg2Rad);
        float y = otherAsteroid.transform.position.y + distance * Mathf.Sin(angleInput * Mathf.Deg2Rad);

        //Draw the line
        lineRenderer.SetPosition(0, otherAsteroid.transform.position);
        lineRenderer.SetPosition(1, new Vector3(x, y, 1f));

    }

    public void DeleteProjectedLine()
    {
        if (GameObject.Find("ProjectionLine") != null)
        {
            Destroy(GameObject.Find("ProjectionLine"));
            Debug.Log("ProjectionLine Destroyed!");
        }
    }

    public void HandleMaterialInput(int val)
    {
        //0 = Metal
        //1 = Mantle
        //2 = Crust
        if(val >= 0 && val <= 2)
        {
            materialSelected = val;
            MaterialInputApplier(val);
        }
    }

    //This method is called when the play selects a drop-down option for material
    //  It sets the otherAsteroid to the appropriate particle type
    public void MaterialInputApplier(int val)
    {
        //Handle the user material input
        SpriteRenderer renderer = otherAsteroid.GetComponent<SpriteRenderer>();

        if (val == 0)
        {
            materialInput = "Metal";
            otherAsteroid.tag = "OA_MetalCore_Particle";
            renderer.color = new Color(0.8588236f, 0.3686275f, 0f, 1f);
        }
        
        else if (val == 1)
        {
            materialInput = "Mantle";
            otherAsteroid.tag = "OA_Mantle_Particle";
            renderer.color = new Color(0.5566f, 0.2389f, 0f, 1f);
        }
        else if (val == 2)
        {
            materialInput = "Crust";
            otherAsteroid.tag = "OA_Crust_Particle";
            renderer.color = new Color(0.3113f, 0.1344f, 0.0011f, 1f);
        }
        else { materialInput = "Something went wrong (materialInput)"; }
    }

    //This method is called when the player selects a drop-down option for Location.
    //it adjusts the otherAsteroid location according to where the center particle of
    //   Psyche is positioned
    public void HandleLocationInput(int val)
    {
        Vector3 centerParticleLocation = GameObject.Find("MiddleAsteroidParticle").transform.position;
        //GameObject centerParticle = GameObject.Find("MiddleAsteroidParticle"); 
        switch (val)
        {
            //Left
            case 0:
                Vector3 otherAsteroidPosition = new Vector3(-10, 0, 0);
                otherAsteroidPosition = otherAsteroidPosition + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition;
                DeleteProjectedLine();
                HandleAngleInput();
                break;
            
            //Top-Left
            case 1:
                Vector3 otherAsteroidPosition1 = new Vector3(-7, 5, 0);
                otherAsteroidPosition1 = otherAsteroidPosition1 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition1;
                DeleteProjectedLine();
                HandleAngleInput();
                break;

            //Top
            case 2:
                Vector3 otherAsteroidPosition2 = new Vector3(0, 7, 0);
                otherAsteroidPosition2 = otherAsteroidPosition2 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition2;
                DeleteProjectedLine();
                HandleAngleInput();
                break;
            
            //Top-Right
            case 3:
                Vector3 otherAsteroidPosition3 = new Vector3(7, 5, 0);
                otherAsteroidPosition3 = otherAsteroidPosition3 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition3;
                DeleteProjectedLine();
                HandleAngleInput();
                break;

            //Right
            case 4:
                Vector3 otherAsteroidPosition4 = new Vector3(10, 0, 0);
                otherAsteroidPosition4 = otherAsteroidPosition4 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition4;
                DeleteProjectedLine();
                HandleAngleInput();
                break;

            //Bottom-Right
            case 5:
                Vector3 otherAsteroidPosition5 = new Vector3(7, -5, 0);
                otherAsteroidPosition5 = otherAsteroidPosition5 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition5;
                DeleteProjectedLine();
                HandleAngleInput();
                break;

            //Bottom
            case 6:
                Vector3 otherAsteroidPosition6 = new Vector3(0, -5, 0);
                otherAsteroidPosition6 = otherAsteroidPosition6 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition6;
                DeleteProjectedLine();
                HandleAngleInput();
                break;

            //Bottom-Left
            case 7:
                Vector3 otherAsteroidPosition7 = new Vector3(-7, -5, 0);
                otherAsteroidPosition7 = otherAsteroidPosition7 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition7;
                DeleteProjectedLine();
                HandleAngleInput();
                break;

        }
    }

    public void setOtherAsteroid()
    {
        otherAsteroid = GameObject.Find("OtherAsteroid");
    }
    //Runs before any start(), lets us set the "OtherAsteroid" parameters
    private void Awake()
    {
        testAsteroidSpeed = GameObject.Find("OtherAsteroid").GetComponent<TestAsteroidSpeed>();
        
    }
}
