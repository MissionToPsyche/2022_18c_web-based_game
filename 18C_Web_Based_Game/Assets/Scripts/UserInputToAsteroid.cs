using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UserInputToAsteroid : MonoBehaviour
{


    // public const int initialMass = 99;
    // public const int initialMantleMass = 33;
    // public const int initialCrustMass = 33;
    // public const int initialMetalMass = 33;

    // public GameObject particleCounter;

    public const int DESIREDMASS = 500;

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
            if(velocityInputField.text == "")
            {
                velocityInputField.text = "Not Valid";
                preventApplicationofParameter = true;
            }

            angleInput = Int32.Parse(angleInputField.text);

            if (preventApplicationofParameter == false)
            {
                //Handle the user material input
                otherAsteroidRenderer = otherAsteroid.GetComponent<Renderer>();

                if (materialSelected == 0)
                {
                    materialInput = "Metal";
                    otherAsteroid.tag = "OA_MetalCore_Particle";
                    otherAsteroidColor = new Color(0.8588f, 0.3686f, 0f, 1f);
                    otherAsteroidRenderer.material.SetColor("_Color", otherAsteroidColor);
                }
                else if (materialSelected == 1)
                {
                    materialInput = "Mantle";
                    otherAsteroid.tag = "OA_Mantle_Particle";
                    otherAsteroidColor = new Color(0.5566f, 0.2389f, 0f, 1f);
                    otherAsteroidRenderer.material.SetColor("_Color", otherAsteroidColor);
                }
                else if (materialSelected == 2)
                {
                    materialInput = "Crust";
                    otherAsteroid.tag = "OA_Crust_Particle";
                    otherAsteroidColor = new Color(0.3113f, 0.1344f, 0.0011f, 1f);
                    otherAsteroidRenderer.material.SetColor("_Color", otherAsteroidColor);
                }
                else { materialInput = "Something went wrong (materialInput)"; }

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
    //   Metal to Mantle to Crust Ratio +
    //   Total Mass of Psyche compared to actual mass
    public void HandleEvaluateClick()
    {
      Debug.Log("Evaluate Clicked");
      scoreValue = GameObject.Find("Canvas - HUD/HUD Parent/CurScore").GetComponent<TMPro.TextMeshProUGUI>();
      scoreValue.text = massInput.ToString();
      Debug.Log("Score Set");

      Debug.Log("MATERIAL CHOSEN WAS: " + materialInput);

      //Enable the timer contained in the AfterLaunchTimer.cs script
      ParticleCounter pScript = GameObject.Find("ParticleCounter").GetComponent<ParticleCounter>();
      pScript.enabled = true;

      Debug.Log("PARTICLE COUNT (crust, metal, mantle): " + pScript.getCrust_Particle_Count() + " " + pScript.getMetal_Particle_Count() + " " + pScript.getMantle_Particle_Count());

      // get total num of Particles.
      // multiple metal by 3 and mantle by 2 because that is their masses
      int adjustedMetalCount = pScript.getMetal_Particle_Count()*3;
      int adjustedMantleCount = pScript.getMantle_Particle_Count()*2;
      int crustCount = pScript.getCrust_Particle_Count();

      int totalParticleCount = pScript.getCrust_Particle_Count() + adjustedMetalCount + adjustedMantleCount;
      Debug.Log("TOTAL PARTICLE COUNT: " + totalParticleCount);


      // get metal Ratio
      float metalRatio = (float)adjustedMetalCount/(float)totalParticleCount;
      Debug.Log("METAL RATIO " + metalRatio);
      // get mantle Ratio
      float mantleRatio = (float)adjustedMantleCount/(float)totalParticleCount;
      Debug.Log("MANTLE RATIO: " + mantleRatio);
      // get crust ratio
      float crustRatio = (float)crustCount/(float)totalParticleCount;
      Debug.Log("CRUST RATIO: " + crustRatio);

      Debug.Log("TOTAL MASS: " + totalParticleCount);


      // check metal to mantle to crust ratio
      // TODO: determine ratio to compare it to?
      // we know metal is between 30-60 %
      float scoreOne = 0;
      if(metalRatio <= 0.60 && metalRatio >= 0.30)
      {
        scoreOne = 50;
      }
      else
      {
        scoreOne = 0;
      }

      // check total mass and develop the score based on it
      float ratio = (float)totalParticleCount/(float)DESIREDMASS;
      Debug.Log("SCORE: " + ratio/2);
      float scoreTwo = (ratio/2) * 100;

      float totalScore = scoreOne + scoreTwo;

      Debug.Log("TOTAL SCORE: " + (scoreOne + scoreTwo));
      scoreValue.text = totalScore.ToString();
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
        }
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
