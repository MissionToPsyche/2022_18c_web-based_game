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
            massInput = Int32.Parse(massInputField.text);
            velocityInput = Int32.Parse(velocityInputField.text);
            angleInput = Int32.Parse(angleInputField.text);
            
            
            //Handle the user material input
            otherAsteroidRenderer = otherAsteroid.GetComponent<Renderer>();

            if(materialSelected == 0)
            {
                materialInput = "Metal";
                otherAsteroid.tag = "MetalCore_Particle";
                otherAsteroidColor = new Color(0.8588f, 0.3686f, 0f, 1f);
                otherAsteroidRenderer.material.SetColor("_Color", otherAsteroidColor);
            }
            else if(materialSelected == 1)
            {
                materialInput = "Mantle";
                otherAsteroid.tag = "Mantle_Particle";
                otherAsteroidColor = new Color(0.5566f, 0.2389f, 0f, 1f);
                otherAsteroidRenderer.material.SetColor("_Color", otherAsteroidColor);
            }
            else if(materialSelected == 2)
            {
                materialInput = "Crust";
                otherAsteroid.tag = "Crust_Particle";
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

            //Update Clicked Button to true
            ClickedButton = true;
        }

    }

    public void HandleMaterialInput(int val)
    {
        if(val >= 0 && val <= 3)
        {
            materialSelected = val;
        }
    }

    //Runs before any start(), lets us set the "OtherAsteroid" parameters
    private void Awake()
    {
        testAsteroidSpeed = GameObject.Find("OtherAsteroid").GetComponent<TestAsteroidSpeed>();
    }
}
