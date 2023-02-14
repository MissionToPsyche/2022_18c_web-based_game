using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UserInputToAsteroid : MonoBehaviour
{
    public bool ClickedButton = false;

    public int massInput;
    public int velocityInput;
    public int angleInput;
    public string materialInput;
    
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

                //Update Clicked Button to true
                ClickedButton = true;
            }
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
                break;
            
            //Top-Left
            case 1:
                Vector3 otherAsteroidPosition1 = new Vector3(-7, 5, 0);
                otherAsteroidPosition1 = otherAsteroidPosition1 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition1;
                break;

            //Top
            case 2:
                Vector3 otherAsteroidPosition2 = new Vector3(0, 7, 0);
                otherAsteroidPosition2 = otherAsteroidPosition2 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition2;
                break;
            
            //Top-Right
            case 3:
                Vector3 otherAsteroidPosition3 = new Vector3(7, 5, 0);
                otherAsteroidPosition3 = otherAsteroidPosition3 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition3;
                break;

            //Right
            case 4:
                Vector3 otherAsteroidPosition4 = new Vector3(10, 0, 0);
                otherAsteroidPosition4 = otherAsteroidPosition4 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition4;
                break;

            //Bottom-Right
            case 5:
                Vector3 otherAsteroidPosition5 = new Vector3(7, -5, 0);
                otherAsteroidPosition5 = otherAsteroidPosition5 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition5;
                break;

            //Bottom
            case 6:
                Vector3 otherAsteroidPosition6 = new Vector3(0, -5, 0);
                otherAsteroidPosition6 = otherAsteroidPosition6 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition6;
                break;

            //Bottom-Left
            case 7:
                Vector3 otherAsteroidPosition7 = new Vector3(-7, -5, 0);
                otherAsteroidPosition7 = otherAsteroidPosition7 + centerParticleLocation;
                otherAsteroid.transform.position = otherAsteroidPosition7;
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
